using System;
using System.Threading.Tasks;

namespace SharedResources.Logic
{
    public interface ISharedResourceLogic
    {
        bool TryExit(Guid syncKey);

        Task<(bool, Guid)> TryEnterAsync(string data);
    }
}
