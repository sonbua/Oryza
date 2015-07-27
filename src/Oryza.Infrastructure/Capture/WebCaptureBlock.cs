using System;
using LegoBuildingBlock;
using RestSharp;

namespace Oryza.Infrastructure.Capture
{
    public class WebCaptureBlock : IBlock<Uri, string>
    {
        private readonly IRestClient _restClient;

        public WebCaptureBlock(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Uri Input { get; set; }

        public Func<Uri, string> Work
        {
            get
            {
                return uri =>
                       {
                           _restClient.BaseUrl = uri;

                           return _restClient.ExecuteAsGet(new RestRequest(), "GET").Content;
                       };
            }
        }
    }
}