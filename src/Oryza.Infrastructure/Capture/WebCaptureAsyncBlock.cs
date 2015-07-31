using System;
using System.Threading.Tasks;
using LegoBuildingBlock;
using RestSharp;

namespace Oryza.Infrastructure.Capture
{
    public class WebCaptureAsyncBlock : IBlock<Uri, Task<string>>
    {
        public WebCaptureAsyncBlock(IRestClient restClient)
        {
            Handle = async uri =>
                           {
                               restClient.BaseUrl = uri;

                               var restResponse = await restClient.ExecuteGetTaskAsync(new RestRequest());

                               return restResponse.Content;
                           };
        }

        public Func<Uri, Task<string>> Handle { get; private set; }
    }
}