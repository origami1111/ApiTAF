using ApiTAF.Utils;
using RestSharp;

namespace ApiTAF.Services
{
    public class ServiceCalls
    {
        private readonly RestClient client;

        public ServiceCalls()
        {
            client = new RestClient(Config.BaseUrl);
        }

        public async Task<RestResponse> GetUsersAsync()
        {
            RestRequest request = new RestRequest("users");

            Logger.Log.Info($"Send {request.Method}/{request.Resource} request");

            var response = await client.GetAsync(request);

            Logger.Log.Info($"Status code: {response.StatusCode}");
            Logger.Log.Info($"Response body: {response.Content}");

            return response;
        }

        public async Task<RestResponse> PostUserAsync(string name, string username)
        {
            RestRequest request = new RestRequest("users") { RequestFormat = DataFormat.Json };

            Logger.Log.Info($"Send {request.Method}/{request.Resource} request");

            request.AddJsonBody(new
            {
                name,
                username
            });

            Logger.Log.Info($"Request body: {request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)}");

            var response = await client.PostAsync(request);

            Logger.Log.Info($"Status code: {response.StatusCode}");
            Logger.Log.Info($"Response body: {response.Content}");

            return response;
        }

        public async Task<RestResponse> GetInvalidResourceAsync()
        {
            RestRequest request = new RestRequest("invalidendpoint");

            Logger.Log.Info($"Send {request.Method}/{request.Resource} request");

            var response = await client.GetAsync(request);

            Logger.Log.Info($"Status code: {response.StatusCode}");
            Logger.Log.Info($"Response body: {response.Content}");

            return response;
        }
    }
}
