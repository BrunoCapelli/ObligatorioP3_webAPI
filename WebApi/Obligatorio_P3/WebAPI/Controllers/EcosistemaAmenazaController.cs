using Domain.DTO;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EcosistemaAmenazaController : ControllerBase {
        private IServicioEcosistemaAmenaza _servicioEcosistemaAmenaza;

        public EcosistemaAmenazaController(IServicioEcosistemaAmenaza servicioEcostemaAmenaza) {
            _servicioEcosistemaAmenaza = servicioEcostemaAmenaza;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post([FromBody] int amenazaId, int ecosistemaId)  
        { 
            try {
                _servicioEcosistemaAmenaza.Add(amenazaId, ecosistemaId);
                return Ok();
            }
            catch (ElementoNoValidoException ex) {
                return BadRequest(ex.ToString());
            
            }
        
        }
    }
}
