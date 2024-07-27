using Examen2Poo.API.Dtos.common;
using Examen2Poo.Dto.Client;
using Examen2Poo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Examen2Poo.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController :ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientsService)
        {
            _clientService = clientsService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ClientDto>>> Create(ClientCreateDto dto)
        {
            var response = await _clientService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ClientDto>>> GetPostById(Guid id)
        {
            var response = await _clientService.GetByIdAsync(id);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<ResponseDto<ClientDto>>> EditPostById(ClientEditDto dto, Guid id)
        //{
        //    var response = await _clientService.EditByIdAsync(dto, id);

        //    return StatusCode(response.StatusCode, new
        //    {
        //        response.Status,
        //        response.Message
        //    });
        //}
    }

}
