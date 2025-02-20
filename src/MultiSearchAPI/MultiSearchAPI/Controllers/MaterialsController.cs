using Microsoft.AspNetCore.Mvc;
using MultiSearchAPI.Models;
using MultiSearchAPI.Services;

namespace MultiSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly MaterialService _materialService;

        public MaterialsController()
        {
            _materialService = new MaterialService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Material>> Get()
        {
            var materials = _materialService.GetMaterials();
            return Ok(materials);
        }
    }
}
