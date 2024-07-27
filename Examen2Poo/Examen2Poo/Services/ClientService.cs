using AutoMapper;
using Examen2Poo.API.Dtos.common;
using Examen2Poo.API.Services.Interfaces;
using Examen2Poo.Database;
using Examen2Poo.Database.Entities;
using Examen2Poo.Dto.Amortitation;
using Examen2Poo.Dto.Client;
using Examen2Poo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Transactions;
using static System.Net.WebRequestMethods;

namespace Examen2Poo.Services
{
    public class ClientService : IClientService
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
        public async Task<ResponseDto<dtoprueba>> GetByIdSolutionsAsync(Guid id)
        {
            // Obtener el cliente con las amortizaciones
            var clientEntity = await _context.Clients
                .Include(t => t.Amortitations)
                .ThenInclude(x => x.Amortization)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (clientEntity == null)
            {
                return new ResponseDto<dtoprueba>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El registro {id} no fue encontrado"
                };
            }

            //https://csharp.net-tutorials.com/es/427/linq-/agrupacion-de-datos-el-metodo-groupby-/
            //var usersGroupedByFirstLetters = users.GroupBy(user => user.Name.Substring(0, 2));
            //foreach (var group in usersGroupedByFirstLetters)
            //https://youtu.be/5SdifLe2Iho?t=482
            var grupoAmortizaciones = clientEntity.Amortitations
                .GroupBy(amortization => amortization.Amortization.NombreClave)
                .Select(g => g.Key).ToList();

            var clientDto = _mapper.Map<dtoprueba>(clientEntity);
            clientDto.AmortizationIds = grupoAmortizaciones;
            var clienteConAmortizaciones = new dtoprueba
            {
                Name = clientDto.Name,
                IdentytyNumber = clientDto.IdentytyNumber,
                LoadAmount = clientDto.LoadAmount,
                ComisionRate = clientDto.ComisionRate,
                InteresRest = clientDto.InteresRest,
                Ter = clientDto.Ter,
                DateTimeDesembolso = clientDto.DateTimeDesembolso,
                DeteTimePrimerPago = clientDto.DeteTimePrimerPago,
                AmortizationIds = grupoAmortizaciones
            };
            return new ResponseDto<dtoprueba>
            {
                StatusCode = 200,
                Status = true,
                Message = "exito al encontrar el Registro del usuario",
                Data = clienteConAmortizaciones,
            };
        }
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

                    var existingClient = await _context.Clients
                        .Include(c => c.Amortitations)
                        .FirstOrDefaultAsync(c => c.IdentytyNumber == dto.IdentytyNumber);

                    if (existingClient is null)
                    {
                        _context.Clients.Add(ClientEntity);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ClientEntity = existingClient;
                    }
                    
                    var existingAmortization = await _context.Amortitation
                       .Where(amortization => dto.AmortitationsList.Contains(amortization.NombreClave)).ToListAsync();


                    //Identificar las tags que no existen
                    var newAmortizationsName = dto.AmortitationsList.Except(existingAmortization.Select(t => t.NombreClave)).ToList();
                    // para cada nueva amortrizaciuon 
                    var ids = new List<Guid>();
                    foreach (var amortizacionNombreClave in newAmortizationsName)
                    {
                        var amortizationIds = await ProcessNewAmortization(amortizacionNombreClave, dto);
                        ids.AddRange(amortizationIds);
                    }
                    var amortizationsENtity = await _context.Amortitation
                        .Where(amortization => ids.Contains(amortization.Id))
                        .ToListAsync();
                    var allAmortizations = existingAmortization.Concat(amortizationsENtity).ToList();
                    //hacinedo la propagacion
                    var ClientAmortizationEntity = allAmortizations.Select(t => new ClientAmortitationEntity
                    {
                        ClientId = ClientEntity.Id,
                        AmortizationId = t.Id,
                    }).ToList();

                    // Agregar las relaciones al contexto y guardar
                    _context.ClientAmortitation.AddRange(ClientAmortizationEntity);
                    await _context.SaveChangesAsync();
                    //throw new Exception("Error al crear la Publicacion en el roollBack");


                    await transaction.CommitAsync();

                    return new ResponseDto<ClientDto>
                    {
                        StatusCode = 200,
                        Status = true,
                        Message = "Registro creado correctamente."
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

        private async Task<List<Guid>> ProcessNewAmortization(string AmortizationNameClave, ClientCreateDto dto)
        {
            var amortizationPlan = CalculateAmortizationPlan(AmortizationNameClave, dto.LoadAmount, dto.ComisionRate, dto.Ter, dto.DateTimeDesembolso);

            _context.Amortitation.AddRange(amortizationPlan);

            await _context.SaveChangesAsync();
            var amortizationIds = amortizationPlan.Select(a => a.Id).ToList();

            return amortizationIds;

        }
        public List<AmortizationEntity> CalculateAmortizationPlan(string clave, double montoSolicitado, double InteresAnual, int totalDePagos, DateTime FechadePrimerPago)
        {
            var amortizationPlan = new List<AmortizationEntity>();
            double saldoPrincipal = montoSolicitado;
            DateTime fechaPago = FechadePrimerPago;
            int diasEntrePagos = 0;
            // para cada pago 
            for (int i = 0; i <= totalDePagos; i++)
            {
                if (i == 0)
                {
                    amortizationPlan.Add(new AmortizationEntity
                    {
                        NombreClave = clave,
                        NCuota = 0,
                        Interest = 0,
                        Svcd = 0,
                        cuotaSinSvcd = 0,
                        AbonoExtraordinario = 0,
                        CuotaConSvcd = 0,
                        SaldoPrincipal = saldoPrincipal
                    });
                }
                else
                {
                    // inetres entre los 12 meses del a;o
                    //Intereses (de un mes específico) = (Saldo principal del mes anterior) x (i%/360) x (días mes).
                    double interes = Math.Round(saldoPrincipal * ((InteresAnual /360)*diasEntrePagos), 2);
                    // cuota mensual 
                    // TODO funcion o algo de la cuota 
                    double cuotaMensual = Math.Round(montodeCadaMes(montoSolicitado, InteresAnual, totalDePagos), 2);

                    if (cuotaMensual >=saldoPrincipal)

                    {
                        cuotaMensual=saldoPrincipal+interes;
                    }
                    double AbonoCapital = cuotaMensual - interes;
                    // double cuotaSinSvcd = montoSolicitado - interes - 100;
                    // seguro saldo por 0.015 pero siempre tiene que ser mayor a 2
                    double svsd = Math.Round(calcularSvcd(saldoPrincipal), 2);
                    double cuotaTotal = Math.Round(cuotaMensual+ svsd, 2);

                    saldoPrincipal = Math.Round( saldoPrincipal - AbonoCapital);


                    // propagacion 
                    // Ver si se hace un dto
                    amortizationPlan.Add(new AmortizationEntity
                    {
                        NombreClave = clave,
                        NCuota = i,
                        Interest = interes,
                        Svcd = svsd,
                        Amount = AbonoCapital,
                        Fecha = fechaPago,
                        DiasMes = diasEntrePagos,
                        cuotaSinSvcd = cuotaMensual,
                        AbonoExtraordinario = 0,
                        CuotaConSvcd = cuotaTotal,
                        SaldoPrincipal = saldoPrincipal
                    });
                }
                //https://estradawebgroup.com/Post/Gestiona-fechas-y-horas-como-un-experto-la-magia-de-DateTime-en-C/20718
                // https://learn.microsoft.com/es-es/dotnet/api/system.datetime.addmonths?view=net-8.0
                //https://stackoverflow.com/questions/4181942/convert-double-to-int

                DateTime proximoPago = fechaPago.AddMonths(1);
               // https://www.codecademy.com/resources/docs/c-sharp/math-functions/min
                //https://barcelonageeks.com/metodo-datetime-daysinmonth-en-c/
                int ultimoDiaDelMes = DateTime.DaysInMonth(proximoPago.Year, proximoPago.Month);
                int diaDePago = Math.Min(fechaPago.Day, ultimoDiaDelMes);

                proximoPago = new DateTime(proximoPago.Year, proximoPago.Month, diaDePago);
                diasEntrePagos = (int)((proximoPago - fechaPago).TotalDays);
                fechaPago = proximoPago;
            }

            return amortizationPlan;
            }

            private double montodeCadaMes(double MontoSolicitado, double interesAnual, int totalPagos)
            {
            //montoSolicitado * (interesMensual / (1 - Math.Pow(1 + interesMensual, -totalDePagos)))
            // Imensual  =  ianual/ (360/12/365) 
            double interesMensual = interesAnual / 11.83;
                //  cuota=   Im/ (1-(1+im)^-n)/i
                double cuota = MontoSolicitado / ( (1   -  Math.Pow((   1+interesMensual), -totalPagos  )   )     /    interesMensual  );
                return cuota;
            }
            private double calcularSvcd(double saldoPrincipal)
            {
                var resultado = saldoPrincipal * 0.0015;
                if (resultado < 2)
                {
                    resultado = 2;
                }
                return resultado;
            }
        }
    }

