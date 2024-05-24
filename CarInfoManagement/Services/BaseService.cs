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
        // constructor for initialize the depedencies
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        // Asynchronous the method for sending HTTP requests
        public async Task<string?> SendAsync(RequestDTO requestDTO)
        {
            if(requestDTO == null) throw new ArgumentNullException(nameof(requestDTO));

            //Create an HttpClient using the provided factory
            using HttpClient client = _httpClientFactory.CreateClient("CarInfoDetailsAPI");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",requestDTO.AccessToken);

            //Create an Http request nessage based on the requestDTO
            var message = CreateRequestMessage(requestDTO);

            //Send the HTTP request and await the reponse
            var apiResponse = await client.SendAsync(message).ConfigureAwait(false);

            // Read the content of the reponse
            var apiContent = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            if(apiResponse.IsSuccessStatusCode && !string.IsNullOrEmpty(apiContent))
            {
                return apiContent;
            }

            //handle the errors in the reponse
            HandleErrorResponse(apiContent);

            throw new BadHttpRequestException(apiContent);
        }

        //Create the HttpRequestMessage based on the RequestDTO
        private HttpRequestMessage CreateRequestMessage(RequestDTO requestDTO)
        {
            var message = new HttpRequestMessage
            {
                Method = GetHttpMethod(requestDTO.ApiType),
                RequestUri = new Uri(requestDTO.Url)
             };
            //set the headers for the request
             SetRequestHeaders(message);

            //Set the request body if it exists in the RequestDTO
             setRequestBody(requestDTO, message);
             return message;
          }

     //Map APiType enum to HttpMethod
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
        //Set the request body for the HTTP request if it exists in the RequestDTO
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
        //handle the errors in the API response content
        private void HandleErrorResponse(string apiContent)
        {
            //Deserialize the API content to ExceptionDTO
            var exceptionDTO = JsonConvert.DeserializeObject<ExceptionDTO>(apiContent);
            if(exceptionDTO?.api_id != null)
            {
               throw new BadHttpRequestException(apiContent);
            }
        }
   }
    
}

