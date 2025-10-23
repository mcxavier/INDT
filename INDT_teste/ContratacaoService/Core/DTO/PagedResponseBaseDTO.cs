using System.Text.Json;

namespace ContratacaoService.Core.DTO
{
    public class PagedResponseBaseDTO<T> where T : class
    {
        public string? ErrorMessage { get; set; }
        public string? JsonText { get; set; }
        public int TotalCount { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        public int StatusCode { get; set; }


        public JsonElement? Data { get; set; } = null;

    }
}
