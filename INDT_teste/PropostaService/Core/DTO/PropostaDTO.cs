namespace PropostaService.Core.DTO
{
    public class PropostaDTO
    {
        public int id { get; set; }
        public string descrciao { get; set; }
        public int statusId { get; set; }
        public string status { get; set; }
        public DateTime? dataContratacao { get; set; }
    }
}
