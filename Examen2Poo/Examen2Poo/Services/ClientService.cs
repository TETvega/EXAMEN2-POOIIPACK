using AutoMapper;
using Examen2Poo.API.Dtos.common;
using Examen2Poo.API.Services.Interfaces;
using Examen2Poo.Database;
using Examen2Poo.Database.Entities;
using Examen2Poo.Dto.Client;
using Examen2Poo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examen2Poo.Services
{
    public class ClientService: IClientService
    {
        private readonly Examen2PooContext _context;
        private readonly IAuthService _authService;
        private readonly ILogger<ClientService> _logger;
        private readonly IMapper _mapper;
        public ClientService(
            Examen2PooContext context,
            IAuthService authService,
            ILogger<ClientService> logger,
            IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _logger = logger;
            _mapper = mapper;
        }


        // Metodos 

        public async Task<ResponseDto<ClientDto>> GetByIdAsync(Guid Id)
        {
            var clientEntity = await _context.Clients

                .Include(t => t.Amortitations)
                .ThenInclude(x => x.Amortization)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (clientEntity == null)
            {
                return new ResponseDto<ClientDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El registro {Id} no fue encontrado"
                };
            }

            var postDto = _mapper.Map<ClientDto>(clientEntity);
            return new ResponseDto<ClientDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Exito al encontrar el Registro",
                Data = postDto
            };
        }
        public async Task<ResponseDto<ClientDto>> CreateAsync(ClientCreateDto dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var ClientEntity = _mapper.Map<ClientEntity>(dto);

                    _context.Clients.Add(ClientEntity);
                    await _context.SaveChangesAsync();

                    //buscar las tablas del Dto en l tabla de tags
                    var existingAmortizations = await _context.Amortitation
                        .Where(client => dto.AmortitationsList.Contains(client.NombreClave)).ToListAsync();

                    //Identificar las tags que no existen
                    var newAmortizationsClave = dto.AmortitationsList.Except(existingAmortizations.Select(t => t.NombreClave)).ToList();

                    var newAmortizations = newAmortizationsClave.Select(name => new AmortizationEntity
                    {
                        NombreClave = name,
                    }).ToList();

                    _context.Amortitation.AddRange(newAmortizations);

                    await _context.SaveChangesAsync();


                    //combinar tags existentes y nuevos

                    var allAmortizations = existingAmortizations.Concat(newAmortizations).ToList();
                    var postTagsEntity = allAmortizations.Select(t => new ClientAmortitationEntity
                    {
                        //el id se genera el solo
                        ClientId = ClientEntity.Id,
                        AmortizationId = t.Id,
                    }).ToList();


                    _context.ClientAmortitation.AddRange(postTagsEntity);
                    await _context.SaveChangesAsync();
                    //throw new Exception("Error al crear la Publicacion en el roollBack");

                    await transaction.CommitAsync();
                    //TODO RESPUESTA AL CLIENTE

                    return new ResponseDto<ClientDto>
                    {
                        StatusCode = 200,
                        Status = true,
                        Message = "Registro creado correctamente Cliente"

                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, "Error al Crear el Registro en Cliente Create Dto");
                    return new ResponseDto<ClientDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = "Se produjo un error al crear el CLIENTE"
                    };

                }
            }
        }



    }
}
