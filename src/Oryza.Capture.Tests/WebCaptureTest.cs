using System;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Xunit;

namespace Oryza.Capture.Tests
{
    public class WebCaptureTest
    {
        private readonly IServiceProvider _serviceProvider;

        public WebCaptureTest()
        {
            _serviceProvider = new TestDoublesContainerBuilder().Build();
        }

        [Fact]
        public async void CaptureAsync_CaptureOryzaWebPage_HasOryzaString()
        {
            // arrange
            var webCapture = _serviceProvider.GetService<IWebCapture>();
            var configuration = _serviceProvider.GetService<IConfiguration>();

            // act
            var html = await webCapture.CaptureAsync(configuration.OryzaCaptureAddress);

            // assert
            Assert.Contains("oryza.com", html);
            Assert.Contains("USD per ton", html);
        }
    }
}