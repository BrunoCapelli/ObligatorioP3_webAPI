using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecieController : ControllerBase
    {

        private IServicioEspecie _servicioEspecie;

        public EspecieController(IServicioEspecie servicioEspecie)
        {
            _servicioEspecie = servicioEspecie;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<EspecieDTO> especies = _servicioEspecie.GetAll();
            return Ok(especies);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
