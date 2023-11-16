using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;

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



        [HttpPost]
        public async Task<IActionResult> LoadPaises()
        {
            IEnumerable<PaisDTO> paises = await _servicioPais.LoadPaisesAsync();

            return Ok(paises);
        }


    }
}
