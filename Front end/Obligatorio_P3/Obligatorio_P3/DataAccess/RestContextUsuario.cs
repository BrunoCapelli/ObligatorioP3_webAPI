using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public class RestContextUsuario<T>: IRestContextUsuario<T>
    {
        private readonly HttpClient httpClient;
        private readonly string apiUrl = "https://localhost:7002/api/Usuario";

        public RestContextUsuario(string apiUrl)
        {
            // Constructor de la clase que acepta la URL base de la API como argumento.

            this.apiUrl = apiUrl;
            // Inicializa la URL base de la API con el valor proporcionado.

            httpClient = new HttpClient();
            // Inicializa una instancia de HttpClient, que se utilizará para realizar solicitudes HTTP a la API.

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Configura los encabezados HTTP para indicar que se espera una respuesta en formato JSON.
        }

        public async Task<Usuario> Login(string alias, string password)
        {

            string request = "/Login?Alias="+alias+"&Password="+password;
        
            string entityJson = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(request, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl + request, content);
            string errorMessage = await response.Content.ReadAsStringAsync();
            HttpErrorHandler.throwExceptionFromHttpStatusCodeAsync(response, errorMessage);
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                WriteIndented = true                                // write pretty json
            };
            // pass options to deserializer
            var createdEntity = JsonSerializer.Deserialize<Usuario>(responseBody, options);
            return createdEntity;
        }

        public async Task<T> Add(T entity, string token)
        {
            string url = "/Register";
            string entityJson = JsonSerializer.Serialize(entity);
            StringContent content = new StringContent(entityJson, System.Text.Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl+url, content);
            string errorMessage = await response.Content.ReadAsStringAsync();
            HttpErrorHandler.throwExceptionFromHttpStatusCodeAsync(response, errorMessage);
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var createdEntity = JsonSerializer.Deserialize<T>(responseBody, options);
            return createdEntity;
        }
        public async Task<IEnumerable<T>> GetAll(string filters)
        {
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl + filters);
            string errorMessage = await response.Content.ReadAsStringAsync();
            HttpErrorHandler.throwExceptionFromHttpStatusCodeAsync(response, errorMessage);
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var entities = new List<T>();
            if(responseBody != "")
            {
                entities = JsonSerializer.Deserialize<List<T>>(responseBody, options);
            }
            
            return entities;
        }
    }
}
