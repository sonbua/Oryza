using System;
using System.Threading.Tasks;
using Oryza.ServiceInterfaces;
using RestSharp;

namespace Oryza.Infrastructure.Capture
{
    public class WebCapture : IWebCapture
    {
        private readonly IRestClient _restClient;

        public WebCapture(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<string> CaptureAsync(Uri uri)
        {
            _restClient.BaseUrl = uri;

            var restResponse = await _restClient.ExecuteGetTaskAsync(new RestRequest());

            return restResponse.Content;
        }
    }
}