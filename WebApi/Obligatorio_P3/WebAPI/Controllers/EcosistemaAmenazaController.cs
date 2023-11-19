using Domain.DTO;
using Domain.Entities;
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
        private IServicioAudit _servicioAudit;

        public EcosistemaAmenazaController(IServicioEcosistemaAmenaza servicioEcostemaAmenaza, IServicioAudit servicioAudit) {
            _servicioEcosistemaAmenaza = servicioEcostemaAmenaza;
            _servicioAudit = servicioAudit;
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post(EcosistemaAmenaza entity)  
        { 
            try {
                _servicioEcosistemaAmenaza.Add(entity.AmenazaId, entity.EcosistemaMarinoId);
                _servicioAudit.Log("Usuario",entity.EcosistemaMarinoId, "EcosistemaMarino (Asig. Amenaza)");
                return Ok(entity);
            }
            catch (ElementoNoValidoException ex) {
                return BadRequest(ex.ToString());
            
            }
        
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<EcosistemaAmenaza> especies = _servicioEcosistemaAmenaza.GetAll();
            return Ok(especies);

        }
    }
}
