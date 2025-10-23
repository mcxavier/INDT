using PropostaService.Core.DTO;

namespace PropostaService.Core.Interfaces
{
    public interface IPropostaUseCase
    {
        Task<PagedResponseBaseDTO<PropostaDTO>> GetPropostaById(int id);
        Task<PagedResponseBaseDTO<PropostaDTO>> UpsertProposta(PropostaDTO propostaRequest);
        Task<PagedResponseBaseDTO<PropostaDTO>> ListPropostaByParams(Dictionary<string, string?> queryStringDictionary);
        Task<PagedResponseBaseDTO<PropostaDTO>> ContrataProposta(int id);
    }
}
