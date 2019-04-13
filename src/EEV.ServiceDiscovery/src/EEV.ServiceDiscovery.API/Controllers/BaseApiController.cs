using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EEV.ServiceDiscovery.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }

    [Route("api/[controller]")]
    public abstract class BaseApiCrudController<T> : BaseApiController
    {
        [HttpPost]
        public abstract Task<IActionResult> Create(T model);

        [Route("{id}")]
        [HttpPost]
        public abstract Task<IActionResult> Update(T model);

        [Route("{id}")]
        [HttpGet]
        public abstract Task<IActionResult> Get(string id);

        [Route("list")]
        [HttpGet]
        public abstract Task<IActionResult> List();

        [Route("{id}")]
        [HttpDelete]
        public abstract Task<IActionResult> Delete(string id);
    }
}
