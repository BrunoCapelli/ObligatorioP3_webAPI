using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AmenazaController : ControllerBase {
        // GET: api/<AmenazaController>
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AmenazaController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

       
    }
}
