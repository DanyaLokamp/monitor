using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResources.Core
{
    public class FifoSemaphore
    {
        private SemaphoreSlim semaphore;
        private ConcurrentQueue<TaskCompletionSource<bool>> queue =
            new ConcurrentQueue<TaskCompletionSource<bool>>();

        public FifoSemaphore(int initialCount, int maxCount)
        {
            semaphore = new SemaphoreSlim(initialCount, maxCount);
        }

        public async Task<bool> WaitAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            queue.Enqueue(tcs);
            var res = await semaphore.WaitAsync(TimeSpan.FromSeconds(5));

            if (res)
            {
                TaskCompletionSource<bool> popped;

                if (queue.TryDequeue(out popped))
                    popped.SetResult(true);

                return await tcs.Task;
            };

            return await tcs.Task;
        }

        public void Release()
        {
            semaphore.Release();
        }
    }
}
