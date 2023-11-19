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
    public class EspecieController : ControllerBase
    {

        private IServicioEspecie _servicioEspecie;
        private IServicioEspecieAmenaza _servicioEspecieAmenaza;
        private IServicioAudit _servicioAudit;
        private IServicioEcosistemaMarinoEspecie _servicioEcosistemaMarinoEspecie;

        public EspecieController(IServicioEspecie servicioEspecie,
            IServicioEcosistemaMarinoEspecie servicioEcosistemaMarinoEspecie,
            IServicioEspecieAmenaza servicioEspecieAmenaza,
            IServicioAudit servicioAudit
            )
        {
            _servicioEspecie = servicioEspecie;
            _servicioEspecieAmenaza = servicioEspecieAmenaza;
            _servicioEcosistemaMarinoEspecie = servicioEcosistemaMarinoEspecie;
            _servicioAudit = servicioAudit;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Estos chirimbolos son para que Swagger interprete los posibles codigos de error que devuelve y los documente automaticamente
        public IActionResult GetAll()
        {
            IEnumerable<EspecieDTO> especies = _servicioEspecie.GetAll();
            return Ok(especies);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                EspecieDTO especie = _servicioEspecie.GetById(id);
                return Ok(especie);

            }catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult Post([FromBody] EspecieDTO especieDTO)
        {
            try
            {
                EspecieDTO eAdd = _servicioEspecie.Add(especieDTO);
                _servicioAudit.Log("Usuario", eAdd.EspecieId, "Especie (Add)");
                return Ok(eAdd);
            }
           
            catch(ElementoNoValidoException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                _servicioEspecie.Remove(id);
                _servicioAudit.Log("Usuario", id, "Especie (Remove)");
                return Ok("Eliminado con exito");
            }
            catch (ElementoNoValidoException exception)
            {
                return NotFound(exception.Message);
            }
        }


        [HttpGet("nombre/{nombreCientifico}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPorNombreCientifico(string nombreCientifico)
        {
            try
            {
                IEnumerable<EspecieDTO> especies = _servicioEspecie.FiltrarPorNombreCientifico(nombreCientifico);
                return Ok(especies);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Grado/{gradoConservacion}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGradoDeConservacion(int gradoConservacion)
        {
            try
            {
                IEnumerable<EspecieDTO> especies = _servicioEspecie.FiltrarPorGradoDeConservacion(gradoConservacion);
                return Ok(especies);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Peso/{pesoDesde}/{pesoHasta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPeso(int pesoDesde, int pesoHasta)
        {
            try
            {
                IEnumerable<EspecieDTO> especies = _servicioEspecie.FiltrarPorPeso(pesoDesde, pesoHasta);
                return Ok(especies);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ecosistema/{ecosistemaID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPorEcosistema(int ecosistemaID)
        {
            try
            {
                IEnumerable<EspecieDTO> especies = _servicioEspecie.FiltrarPorEcosistema(ecosistemaID);
                return Ok(especies);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("AsociarEspecieAEcosistema")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AsociarEspecieAEcosistema(int especieId, int ecosistemaID)
        {
            try
            {
                if (ecosistemaID >= 0 && especieId >= 0)
                {
                    EcosistemaMarinoEspecieDTO emeDTO = _servicioEcosistemaMarinoEspecie.Add(ecosistemaID, especieId);
                    return Ok(emeDTO);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("AsociarAmenazaAEspecie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AsociarAmenazaAEspecie(int AmenazaId, int EspecieId)
        {
            try
            {
                if (EspecieId > 0 && AmenazaId > 0)
                {
                    _servicioEspecieAmenaza.Add(AmenazaId, EspecieId);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
        


    }
}
