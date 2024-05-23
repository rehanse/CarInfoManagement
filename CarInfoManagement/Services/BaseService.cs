using CarInfoManagement.Models.API;
using CarInfoManagement.Models.Exception;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using static CarInfoManagement.Utility.SD;

namespace CarInfoBFF.Services
{
    public class BaseService:IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<string?> SendAsync(RequestDTO requestDTO)
        {
            if(requestDTO == null) throw new ArgumentNullException(nameof(requestDTO));
            using HttpClient client = _httpClientFactory.CreateClient("CarInfoDetailsAPI");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",requestDTO.AccessToken);

            var message = CreateRequestMessage(requestDTO);

            var apiResponse = await client.SendAsync(message).ConfigureAwait(false);

            var apiContent = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            if(apiResponse.IsSuccessStatusCode && !string.IsNullOrEmpty(apiContent))
            {
                return apiContent;
            }

            HandleErrorResponse(apiContent);

            throw new BadHttpRequestException(apiContent);
        }
        private HttpRequestMessage CreateRequestMessage(RequestDTO requestDTO)
        {
            var message = new HttpRequestMessage
            {
                Method = GetHttpMethod(requestDTO.ApiType),
                RequestUri = new Uri(requestDTO.Url)
             };
             SetRequestHeaders(message);
             setRequestBody(requestDTO, message);
             return message;
          }
    private HttpMethod GetHttpMethod(ApiType apiType)
    {
            return apiType switch
            {
                ApiType.POST => HttpMethod.Post,
                ApiType.PUT => HttpMethod.Put,
                ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };
    }
        private void SetRequestHeaders(HttpRequestMessage message)
        {
            message.Headers.Add("Accept", "application/json");
        }
        private void setRequestBody(RequestDTO requestDTO , HttpRequestMessage message)
        {
            if(requestDTO.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data),Encoding.UTF8,"application/json");
            }
        }
        private void HandleErrorResponse(string apiContent)
        {
            var exceptionDTO = JsonConvert.DeserializeObject<ExceptionDTO>(apiContent);
            if(exceptionDTO?.api_id != null)
            {
               // throw new ErrorResponseException(apiContent);
            }
        }
   }
    
}

