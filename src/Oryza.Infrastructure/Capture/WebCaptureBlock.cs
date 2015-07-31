using System;
using LegoBuildingBlock;
using RestSharp;

namespace Oryza.Infrastructure.Capture
{
    public class WebCaptureBlock : IBlock<Uri, string>
    {
        public WebCaptureBlock(IRestClient restClient)
        {
            Handle = uri =>
                     {
                         restClient.BaseUrl = uri;

                         return restClient.ExecuteAsGet(new RestRequest(), "GET").Content;
                     };
        }

        public Func<Uri, string> Handle { get; private set; }
    }
}