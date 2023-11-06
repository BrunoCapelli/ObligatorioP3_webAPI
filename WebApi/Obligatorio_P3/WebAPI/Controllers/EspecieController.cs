﻿using Domain.DTO;
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
        private IServicioEcosistemaMarinoEspecie _servicioEcosistemaMarinoEspecie;

        public EspecieController(IServicioEspecie servicioEspecie,
            IServicioEcosistemaMarinoEspecie servicioEcosistemaMarinoEspecie,
            IServicioEspecieAmenaza servicioEspecieAmenaza
            )
        {
            _servicioEspecie = servicioEspecie;
            _servicioEspecieAmenaza = servicioEspecieAmenaza;
            _servicioEcosistemaMarinoEspecie = servicioEcosistemaMarinoEspecie;
        }
        [Authorize]
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

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post([FromBody] EspecieDTO especieDTO)  // WIP: tengo que ver que hago con el tema de la imagen al crear un nuevo objeto
        {
            try
            {
                EspecieDTO eAdd = _servicioEspecie.Add(especieDTO);
                return Ok(eAdd);
            }
            catch(ElementoYaExisteException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
            catch(ElementoNoValidoException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
       
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                _servicioEspecie.Remove(id);
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

        [HttpGet("grado/{gradoConservacion}")]
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

        [HttpGet("peso/{pesoDesde}/{pesoHasta}")]
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
