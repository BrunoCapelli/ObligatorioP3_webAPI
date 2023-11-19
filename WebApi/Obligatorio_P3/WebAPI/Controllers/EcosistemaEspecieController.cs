using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.Servicios;
using System;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcosistemaEspecieController : ControllerBase
    {
        private IServicioEcosistemaMarinoEspecie _servicioEcosistemaMarinoEspecie;
        private IServicioEspecie _servicioEspecie;
        private IServicioAudit _servicioAudit;

        public EcosistemaEspecieController(IServicioEcosistemaMarinoEspecie servicioEcosistemaMarinoEspecie, IServicioEspecie servicioEspecie, IServicioAudit servicioAudit)
        {
            _servicioEcosistemaMarinoEspecie = servicioEcosistemaMarinoEspecie;
            _servicioEspecie = servicioEspecie;
            _servicioAudit = servicioAudit;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            IEnumerable<EcosistemaMarinoEspecieDTO> ecosEsp = _servicioEcosistemaMarinoEspecie.GetAll();
            return Ok(ecosEsp);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post(int ecosistemaId , int especieId) {
            try {
                EcosistemaMarinoEspecieDTO res = _servicioEcosistemaMarinoEspecie.Add(ecosistemaId, especieId);
                _servicioAudit.Log(HttpContext.Session.GetString("email"), res.EcosistemaMarinoId, "Especie (Asig. Eco)");
                return Ok(res);
            }
            catch (ElementoNoValidoException ex) {
                return BadRequest(ex.ToString());
            }
            catch (ElementoYaExisteException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetById(int id)
        {

            IEnumerable<EspecieDTO> ecosEsp = _servicioEspecie.FiltrarPorEcosistema(id);
            if(ecosEsp.Count() != 0)
            {
                return Ok(ecosEsp);
            }
            else
            {
                return NoContent();
            }
        }

    }
}
    