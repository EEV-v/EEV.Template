using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EEV.ServiceDiscovery.API.Controllers
{
    public class ServicesController : BaseApiCrudController<string>
    {
        private static ConcurrentDictionary<string, string> _dictionary
            = new ConcurrentDictionary<string, string>();

        public override Task<IActionResult> Create(string model)
        {
            return Task.FromResult<IActionResult>(Ok());
        }

        public override Task<IActionResult> Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IActionResult> Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IActionResult> List()
        {
            throw new System.NotImplementedException();
        }

        public override Task<IActionResult> Update(string model)
        {
            throw new System.NotImplementedException();
        }
    }
}
