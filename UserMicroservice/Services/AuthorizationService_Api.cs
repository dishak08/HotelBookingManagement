using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using UserMicroservice.Exceptions;
using UserMicroservice.Models;

namespace UserMicroservice.Services
{
    public class AuthorizationService_Api : IAuthorizationService_Api
    {
        private readonly HttpClient _httpClient;

        public AuthorizationService_Api(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["ConnectedServices:Authorization"]);
        }
        public async Task<AuthTokenPayload> GetAuthTokenAsync(AppUser user)
        {
            // Preparing User Data to be passed through the API
            var myContent = JsonSerializer.SerializeToUtf8Bytes(user);
            ByteArrayContent content = new ByteArrayContent(myContent);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // Preparation Complete. Sending object "Content"

            HttpResponseMessage response = await _httpClient.PostAsync("api/auth/login", content);

            var responseReader = await response.Content.ReadAsStreamAsync();
            AuthTokenPayload payload = await JsonSerializer.DeserializeAsync<AuthTokenPayload>(responseReader);

            if (payload == null) throw new ConnectedServiceException();
            
            return payload;
        }
    }
}
