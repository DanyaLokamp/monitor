using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResources.Logic
{
    public class SomeResourceInterface : ISomeResourceInterface
    {
        public async Task DoSomeWorkAsync(string data, CancellationToken token)
        {
            while (true)
            {
                await Task.Delay(1000);
                Debug.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");

                token.ThrowIfCancellationRequested();
            }
        }
    }
}
