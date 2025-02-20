using Microsoft.AspNetCore.Mvc;
using MultiSearchAPI.Models;
using MultiSearchAPI.Services;

namespace MultiSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentsController : ControllerBase
    {
        private readonly EquipmentService _equipmentService;

        public EquipmentsController()
        {
            _equipmentService = new EquipmentService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> Get()
        {
            var equipments = _equipmentService.GetEquipments();
            return Ok(equipments);
        }
    }
}
