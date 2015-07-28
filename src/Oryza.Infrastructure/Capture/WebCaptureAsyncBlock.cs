using System;
using System.Threading.Tasks;
using LegoBuildingBlock;
using RestSharp;

namespace Oryza.Infrastructure.Capture
{
    public class WebCaptureAsyncBlock : IBlock<Uri, Task<string>>
    {
        private readonly IRestClient _restClient;

        public WebCaptureAsyncBlock(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Func<Uri, Task<string>> Handle
        {
            get
            {
                return async uri =>
                             {
                                 _restClient.BaseUrl = uri;

                                 var restResponse = await _restClient.ExecuteGetTaskAsync(new RestRequest());

                                 return restResponse.Content;
                             };
            }
        }
    }
}