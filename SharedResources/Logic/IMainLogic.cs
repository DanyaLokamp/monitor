using System;
using System.Threading.Tasks;
using SharedResources.Models;

namespace SharedResources.Logic
{
    public interface IMainLogic
    {
        Task<(bool isException, Guid, string)> LockResource(Info info);

        (bool success, bool isException, string message) ReleaseResource(SynchronizationSettings settings);
    }
}
