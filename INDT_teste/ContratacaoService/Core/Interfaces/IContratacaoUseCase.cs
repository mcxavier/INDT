using ContratacaoService.Core.DTO;

namespace ContratacaoService.Core.Interfaces
{
    public interface IContratacaoUseCase
    {
        Task<BaseResponseDTO<dynamic>> Contrata(int id);
        Task<BaseResponseDTO<dynamic>> List();
    }
}
