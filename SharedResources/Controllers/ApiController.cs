using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Core;
using SharedResources.Logic;
using SharedResources.Models;

namespace SharedResources.Controllers
{
    [ApiController]
    [Route("mutex")]
    public class ApiController : ControllerBase
    {
        private readonly IMainLogic _logic;

        public ApiController(IMainLogic logic)
        {
            _logic = logic;
        }

        [HttpPost("enter")]
        public async Task<OperationResult<Guid?>> Enter([FromBody] Info info)
        {
            try
            {
                (bool isException, Guid syncKey, string message) = await _logic.LockResource(info);

                return new OperationResult<Guid?>(
                        true,
                        false,
                        message,
                        syncKey);
            }
            catch(Exception ex)
            {
                return new OperationResult<Guid?>(
                    false,
                    true,
                    ex.Message,
                    null);
            };


        }

        [HttpPost("exit")]
        public OperationResult Exit([FromBody] SynchronizationSettings settings)
        {
            try
            {
                (bool isSuccess, bool isException, string message) = _logic.ReleaseResource(settings);

                return new OperationResult(
                        true,
                        false,
                        message);
            }
            catch(Exception ex)
            {
                return new OperationResult(
                    false,
                    true,
                    ex.Message);
            }

        }
    }
}
