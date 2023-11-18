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
    public class EcosistemaEspecieController : ControllerBase
    {
        private IServicioEcosistemaMarinoEspecie _servicioEcosistemaMarinoEspecie;

        public EcosistemaEspecieController(IServicioEcosistemaMarinoEspecie servicioEcosistemaMarinoEspecie)
        {
            _servicioEcosistemaMarinoEspecie = servicioEcosistemaMarinoEspecie;
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
                return Ok(res);
            }
            catch (ElementoNoValidoException ex) {
                return BadRequest(ex.ToString());
            }
            catch (ElementoYaExisteException ex) {
                return Conflict(ex.Message);
            }
        }

    }
}
    