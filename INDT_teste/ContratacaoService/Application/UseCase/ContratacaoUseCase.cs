using ContratacaoService.Core.DTO;
using ContratacaoService.Core.Interfaces;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace ContratacaoService.Application.UseCase
{
    public class ContratacaoUseCase : IContratacaoUseCase
    {
        private HttpContent content;

        public async Task<BaseResponseDTO<dynamic>> Contrata(int id)
        {
            var client = new HttpClient();
            var response = await client.PostAsync("http://localhost:5122/api/Proposta/Contrata/0" + id.ToString(), content);

            PagedResponseBaseDTO<object> PageResponse = new PagedResponseBaseDTO<object>();
            PageResponse.TotalCount = 1;
            PageResponse.StatusCode = ((int)response.StatusCode);
            var res = ConvertBaseResponseJson(PageResponse);
            return res;
        }

        public async Task<BaseResponseDTO<dynamic>> List()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5122/api/Proposta/List");

            PagedResponseBaseDTO<object> PageResponse = new PagedResponseBaseDTO<object>();
            PageResponse.TotalCount = 1;
            PageResponse.StatusCode = ((int)response.StatusCode);
            var res = ConvertBaseResponseJson(PageResponse);
            return res;
        }

        public BaseResponseDTO<dynamic> ConvertBaseResponseJson(PagedResponseBaseDTO<object> response)
        {
            var res = new BaseResponseDTO<dynamic>();
            res.StatusCode = response.StatusCode;
            res.JsonText = response.JsonText;
            if (string.IsNullOrEmpty(response.JsonText))
            {
                return res;
            }

            var deserializedObject = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(response.JsonText);


            if (deserializedObject.TryGetProperty("data", out var data))
                res.Data = data;

            if (deserializedObject.TryGetProperty("totalCount", out var totalCount))
                if (!string.IsNullOrEmpty(totalCount.ToString()))
                    res.totalCount = totalCount.GetInt32();


            if (deserializedObject.TryGetProperty("errorMessage", out var errorMessage))
                if (!string.IsNullOrEmpty(errorMessage.ToString()))
                    res.ErrorMessage = errorMessage.ToString();

            if (deserializedObject.TryGetProperty("detail", out var detail))
                if (!string.IsNullOrEmpty(detail.ToString()) && response.StatusCode != 200)
                    res.ErrorMessage = detail.ToString();

            if (deserializedObject.TryGetProperty("fields", out var fields))
                if (!string.IsNullOrEmpty(fields.ToString()) && response.StatusCode != 200)
                    res.ErrorMessage = $"{fields} já cadastrado.";

            res.Data ??= deserializedObject;

            return res;
        }
    }
}
