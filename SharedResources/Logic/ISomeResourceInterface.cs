using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResources.Logic
{
    public interface ISomeResourceInterface
    {
        Task DoSomeWorkAsync(string data, CancellationToken token);
    }
}
