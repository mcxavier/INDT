using PropostaService.Core.DTO;
using PropostaService.Core.Interfaces;

namespace PropostaService.Application.UseCase
{
    public class PropostaUseCase :IPropostaUseCase
    {
        private readonly IPropostaRepository _propostaRepository;

        public PropostaUseCase(IPropostaRepository propostaRepository)
        {
            _propostaRepository = propostaRepository;
        }

        public async Task<PagedResponseBaseDTO<PropostaDTO>> GetPropostaById(int id)
        {
            var response = await _propostaRepository.GetPropostaById(id);

            if (response.Data == null)
            {
                return new PagedResponseBaseDTO<PropostaDTO>
                {
                    Data = null,
                    TotalCount = 0,
                    ErrorMessage = "Registro não encontrado",
                    statusCode = 404
                };
            }

            return new PagedResponseBaseDTO<PropostaDTO>
            {
                Data = new List<PropostaDTO> { response.Data },
                TotalCount = 1,
                ErrorMessage = response.ErrorMessage,
                statusCode = 200
            };
        }

        public async Task<PagedResponseBaseDTO<PropostaDTO>> ListPropostaByParams(Dictionary<string, string?> queryStringDictionary)
        {
            var lista = await _propostaRepository.ListPropostasByParams(queryStringDictionary);

            return new PagedResponseBaseDTO<PropostaDTO>
            {
                Data = lista.Item1,
                TotalCount = lista.Item2,
                ErrorMessage = "",
                statusCode = 200
            };
        }

        public async Task<PagedResponseBaseDTO<PropostaDTO>> UpsertProposta(PropostaDTO propostaRequest)
        {
            if (propostaRequest.id == 0)
            {
                var insertResult = await _propostaRepository.InsertProposta(propostaRequest);

                return new PagedResponseBaseDTO<PropostaDTO>
                {
                    Data = new List<PropostaDTO> { insertResult.Data },
                    TotalCount = 1,
                    ErrorMessage = insertResult.ErrorMessage,
                    statusCode = insertResult.statusCode
                };
            }
            else
            {
                var updateResult = await _propostaRepository.UpdateProposta(propostaRequest);

                return new PagedResponseBaseDTO<PropostaDTO>
                {
                    Data = new List<PropostaDTO> { updateResult.Data },
                    TotalCount = 1,
                    ErrorMessage = updateResult.ErrorMessage,
                    statusCode = updateResult.statusCode
                };
            }
        }



        public async Task<PagedResponseBaseDTO<PropostaDTO>> ContrataProposta(int id)
        {
            var response = await _propostaRepository.GetPropostaById(id);

            if (response.Data == null)
            {
                return new PagedResponseBaseDTO<PropostaDTO>
                {
                    Data = null,
                    TotalCount = 0,
                    ErrorMessage = "Proposta não encontrada.",
                    statusCode = 404
                };
            }

            response.Data.statusId = 4; // contrata

            response = await _propostaRepository.UpdateProposta(response.Data);

            return new PagedResponseBaseDTO<PropostaDTO>
            {
                Data = new List<PropostaDTO> { response.Data },
                TotalCount = 1,
                ErrorMessage = response.ErrorMessage,
                statusCode = response.statusCode > 0 ? response.statusCode : 200,
            };
        }
    }
}
