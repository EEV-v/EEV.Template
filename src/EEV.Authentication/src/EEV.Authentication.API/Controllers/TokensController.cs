using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EEV.Authentication.API.Controllers
{
    [Route("api/v1/auth/[controller]/[action]")]
    [ApiController]
    public class TokensController : Controller
    {
        private static ConcurrentDictionary<Models.LoginInfo, string> _dictionary
            = new ConcurrentDictionary<Models.LoginInfo, string>();

        private static List<Models.LoginInfo> _knownUsers = new List<Models.LoginInfo>() { new Models.LoginInfo("1", "2") };

        [HttpPost]
        public Task<IActionResult> Create(Models.LoginInfo model)
        {
            if (!_knownUsers.Contains(model))
            {
                return Task.FromResult<IActionResult>(Forbid());
            }
            _dictionary.TryAdd(model, Guid.NewGuid().ToString());
            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> Check(string id)
        {
#if DEBUG
            if (id == "Bearer 123")
            {
                return Task.FromResult<IActionResult>(Ok());
            }
#endif
            return _dictionary.Values.Contains(id)
                ? Task.FromResult<IActionResult>(Ok())
                : Task.FromResult<IActionResult>(Forbid());
        }
    }
}
