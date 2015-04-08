using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.Capture
{
    public class WebCaptureTest : Test
    {
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