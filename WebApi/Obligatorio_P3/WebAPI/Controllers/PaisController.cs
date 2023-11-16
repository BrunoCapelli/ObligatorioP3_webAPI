using Domain.DTO;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.Servicios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private IServicioPais _servicioPais;

        public PaisController(IServicioPais servicioPais)
        {
            _servicioPais = servicioPais;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public IActionResult GetAll()
        {
            IEnumerable<PaisDTO> paises = _servicioPais.GetAll();

            return Ok(paises);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            PaisDTO pais = _servicioPais.GetPais(id);

            return Ok(pais);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id) {
            try {
                PaisDTO pais = _servicioPais.GetPais(id);
                return Ok(pais);

            }
            catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoadPaises()
        {
            try {
                IEnumerable<PaisDTO> paises = await _servicioPais.LoadPaisesAsync();
                return Ok(paises);
            }
            catch (ElementoNoValidoException ex) {
                return BadRequest(ex.ToString());
            }
        }


    }
}
