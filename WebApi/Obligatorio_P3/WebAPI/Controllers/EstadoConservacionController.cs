using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoConservacionController : ControllerBase
    {
        private IServicioEstadoConservacion _servicioEstadoConservacion;

        public EstadoConservacionController(IServicioEstadoConservacion servicioEstadoConservacion)
        {
            _servicioEstadoConservacion = servicioEstadoConservacion;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            IEnumerable<EstadoConservacionDTO> estados = _servicioEstadoConservacion.GetAll();

            return Ok(estados);
        }

    }
}
