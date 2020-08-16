using System;
using System.Threading.Tasks;
using SharedResources.Models;

namespace SharedResources.Logic
{
    public class MainLogic : IMainLogic
    {
        private readonly ISharedResourceLogic _sharedResourceLogic;

        public MainLogic(
            ISharedResourceLogic sharedResourceLogic)
        {
            _sharedResourceLogic = sharedResourceLogic;
        }

        public async Task<(bool isException, Guid, string)> LockResource(Info user)
        {
            try
            {
                var res = await _sharedResourceLogic.TryEnterAsync(user.SomeData);

                if (res.Item1)
                    return (false, res.Item2, "Successfully locked!");

                return (true, Guid.Empty, "Already locked!");
            }
            catch (Exception ex)
            {
                throw ex;
                //return (true, Guid.Empty, ex.Message);
            }
        }

        public (bool success, bool isException, string message) ReleaseResource(SynchronizationSettings settings)
        {
            try
            {
                if (_sharedResourceLogic.TryExit(settings.SyncKey))
                    return (true, false, "Successfully unlocked!");

                return (false, false, "Nothing to unlock!");
            }
            catch (Exception ex)
            {
                throw ex;
                //return (false, true, ex.Message);
            }
        }
    }
}
