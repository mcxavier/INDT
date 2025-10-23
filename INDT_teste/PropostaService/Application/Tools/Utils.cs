using System.Web;

namespace PropostaService.Application.Tools
{
    public static class Utils
    {
        public static Dictionary<string, string?> GetQueryStringDictionary(string queryString)
        {
            var nvc = HttpUtility.ParseQueryString(queryString);
            return nvc.AllKeys.ToDictionary(k => k!, k => nvc[k]);
        }
    }
}
