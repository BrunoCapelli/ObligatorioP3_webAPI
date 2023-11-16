using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Servicios.Servicios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class AmenazaController : ControllerBase {
        private IServicioAmenaza _servicioAmenaza;

        public AmenazaController(IServicioAmenaza servicioAmenaza) {
            _servicioAmenaza = servicioAmenaza;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Estos chirimbolos son para que Swagger interprete los posibles codigos de error que devuelve y los documente automaticamente
        public IActionResult GetAll() {
            IEnumerable<AmenazaDTO> amenazas = _servicioAmenaza.GetAll();
            return Ok(amenazas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id) {

            try {
                AmenazaDTO amenaza = _servicioAmenaza.GetAmenaza(id);
                return Ok(amenaza);

            }
            catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }

       
    }
}
