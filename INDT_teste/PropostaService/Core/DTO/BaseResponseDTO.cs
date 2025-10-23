using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PropostaService.Core.DTO
{
    public class BaseResponseDTO<T>
    {
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        public string? InformationMessage { get; set; }
        public int totalCount { get; set; }
        public int statusCode { get; set; }

        public BaseResponseDTO()
        {
        }

        public BaseResponseDTO(T data)
        {
            Data = data;
        }

        public BaseResponseDTO(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
