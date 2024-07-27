using Examen2Poo.API.Dtos.common;
using Examen2Poo.Dto.Amortitation;
using Examen2Poo.Dto.Client;

namespace Examen2Poo.Services.Interfaces
{
    public interface IClientService
    {
        Task<ResponseDto<ClientDto>> CreateAsync(ClientCreateDto dto);
        Task<ResponseDto<ClientDto>> GetByIdAsync(Guid Id);
        Task<ResponseDto<dtoprueba>> GetByIdSolutionsAsync(Guid Id);
    }
}
