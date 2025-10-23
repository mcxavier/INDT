using System.Text.Json;

namespace ContratacaoService.Core.DTO
{
    public class BaseResponseDTO<T>
    {
        public JsonElement? Data { get; set; } = null;
        public string ErrorMessage { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        public int totalCount { get; set; }
        public string JsonText { get; set; }
        public int StatusCode { get; set; }

        public BaseResponseDTO()
        {
        }


        public BaseResponseDTO(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
