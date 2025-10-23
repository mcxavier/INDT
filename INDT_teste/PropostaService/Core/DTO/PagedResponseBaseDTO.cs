namespace PropostaService.Core.DTO
{
    public class PagedResponseBaseDTO<T> where T : class
    {
        public string? ErrorMessage { get; set; }
        public IEnumerable<T>? Data { get; set; }
        public int TotalCount { get; set; }
        public int statusCode { get; set; }
        public string? InformationMessage { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
    }
}
