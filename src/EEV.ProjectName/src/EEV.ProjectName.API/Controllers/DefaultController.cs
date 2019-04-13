using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EEV.ProjectName.API.Controllers
{
    [Route("api/v1/[Controller]/[Action]")]
    public class DefaultController : Controller
    {
        [HttpGet]
        public Task<IActionResult> Do()
        {
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
