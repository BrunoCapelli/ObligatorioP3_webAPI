using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcosistemaEspecieController : ControllerBase
    {
        private IServicioEcosistemaMarinoEspecie _servicioEcosistemaMarinoEspecie;

        public EcosistemaEspecieController(IServicioEcosistemaMarinoEspecie servicioEcosistemaMarinoEspecie)
        {
            _servicioEcosistemaMarinoEspecie = servicioEcosistemaMarinoEspecie;
        }

        
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<EcosistemaMarinoEspecieDTO> ecosEsp = _servicioEcosistemaMarinoEspecie.GetAll();
            return Ok(ecosEsp);
        }

        
    }
}
