using Oryza.Infrastructure.Capture;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.Capture
{
    public class WebCaptureAsyncBlockTest : Test
    {
        [Fact]
        public async void CaptureOryzaWebPage_HasOryzaString()
        {
            // arrange
            var webCaptureAsyncBlock = _serviceProvider.GetService<WebCaptureAsyncBlock>();
            var configuration = _serviceProvider.GetService<IConfiguration>();

            // act
            var html = await webCaptureAsyncBlock.Handle(configuration.OryzaCaptureAddress);

            // assert
            Assert.Contains("oryza.com", html);
            Assert.Contains("USD per ton", html);
        }
    }
}