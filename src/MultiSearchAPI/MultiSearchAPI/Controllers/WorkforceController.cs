using Microsoft.AspNetCore.Mvc;
using MultiSearchAPI.Models;
using MultiSearchAPI.Services;

namespace MultiSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkforceController : ControllerBase
    {
        private readonly WorkforceService _workforceService;

        public WorkforceController()
        {
            _workforceService = new WorkforceService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Workforce>> Get()
        {
            var workforce = _workforceService.GetWorkforce();
            return Ok(workforce);
        }
    }
}
