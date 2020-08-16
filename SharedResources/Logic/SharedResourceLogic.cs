using System;
using System.Threading;
using System.Threading.Tasks;
using SharedResources.Core;

namespace SharedResources.Logic
{
    public class SharedResourceLogic : ISharedResourceLogic
    {
        private CancellationTokenSource cts;
        private Guid SynchronizationKey;
        private bool lockTaken = false;
        private static readonly FifoSemaphore _sem = new FifoSemaphore(1, 1);
        private readonly ISomeResourceInterface _someResourceInterface;

        public SharedResourceLogic(ISomeResourceInterface someResourceInterface)
        {
            _someResourceInterface = someResourceInterface;
        }

        public bool TryExit(Guid syncKey)
        {
            if (lockTaken)
            {
                if (syncKey == SynchronizationKey)
                {
                    try
                    {
                        cts.Cancel();

                        _sem.Release();

                        lockTaken = false;

                        return true;
                    }
                    catch (SynchronizationLockException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    throw new Exception("Wrong SynchronizationKey");
                }
            }

            return false;
        }

        private Guid SetSynchronizationKey()
        {
            SynchronizationKey = Guid.NewGuid();

            return SynchronizationKey;
        }

        public async Task<(bool, Guid)> TryEnterAsync(string data)
        {
            (bool, Guid) result = (false, Guid.Empty);

            if (await _sem.WaitAsync())
            {
                lockTaken = true;

                var key = SetSynchronizationKey();

                cts = new CancellationTokenSource();

                Task.Run(async () =>
                {
                    try
                    {
                        await _someResourceInterface.DoSomeWorkAsync(data, cts.Token);
                    }
                    catch (SynchronizationLockException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }, cts.Token);

                result = (true, key);
            };

            return result;
        }
    }
}
