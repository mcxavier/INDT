using PropostaService.Core.DTO;


namespace PropostaService.Core.Interfaces
{
    public interface IPropostaRepository
    {
        Task<BaseResponseDTO<PropostaDTO>> GetPropostaById(int id);
        Task<BaseResponseDTO<PropostaDTO>> UpdateProposta(PropostaDTO record);
        Task<BaseResponseDTO<PropostaDTO>> InsertProposta(PropostaDTO record);
        Task<Tuple<List<PropostaDTO>, int>> ListPropostasByParams(Dictionary<string, string?> queryStringDictionary);
    }
}
