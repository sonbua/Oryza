using Oryza.Infrastructure.Capture;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.Capture
{
    public class WebCaptureBlockTest : Test
    {
        [Fact(Skip = "Temporarily wait until an account is registered at oryza.com")]
        public void CaptureOryzaWebPage_HasOryzaString()
        {
            // arrange
            var webCaptureBlock = _serviceProvider.GetService<WebCaptureBlock>();
            var configuration = _serviceProvider.GetService<IConfiguration>();

            // act
            var html = webCaptureBlock.Handle(configuration.OryzaCaptureAddress);

            // assert
            Assert.Contains("oryza.com", html);
            Assert.Contains("USD per ton", html);
        }
    }
}