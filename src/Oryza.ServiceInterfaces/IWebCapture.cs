using System;
using System.Threading.Tasks;

namespace Oryza.ServiceInterfaces
{
    public interface IWebCapture
    {
        Task<string> CaptureAsync(Uri uri);
    }
}