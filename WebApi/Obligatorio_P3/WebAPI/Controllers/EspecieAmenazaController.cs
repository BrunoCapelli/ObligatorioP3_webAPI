using Domain.DTO;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.Servicios;

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EspecieAmenazaController : ControllerBase {
        private IServicioEspecieAmenaza _servicioEspecieAmenaza;

        public EspecieAmenazaController(IServicioEspecieAmenaza servicioEspecieAmenaza) {
            _servicioEspecieAmenaza = servicioEspecieAmenaza;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post(EspecieAmenazaDTO especieAmenaza) {
            try {
                _servicioEspecieAmenaza.Add(especieAmenaza.AmenazaId, especieAmenaza.EspecieId);
                return Ok();
            }
            catch (ElementoNoValidoException ex) {
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<EspecieAmenazaDTO> ea = _servicioEspecieAmenaza.GetAll();
            return Ok(ea);

        }
    }
}
