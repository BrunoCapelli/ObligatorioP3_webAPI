using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.Servicios;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcosistemaController : ControllerBase
    {
        private IServicioEcosistemaMarino _servicioEcoMarino;
        private IServicioEstadoConservacion _servicioEstadoConservacion;
        private IServicioEcosistemaAmenaza _servicioEcosistemaAmenaza;
        private IServicioPais _servicioPais;
        private IConfiguration _configuration;

        public EcosistemaController(IServicioEcosistemaMarino servicioEcosistemaMarino,
            IServicioEcosistemaAmenaza servicioEcosistemaAmenaza, 
            IServicioEstadoConservacion servicioEstadoConservacion,
            IConfiguration configuration,
            IServicioPais servicioPais)
        {
            _servicioEcoMarino = servicioEcosistemaMarino;
            _servicioEcosistemaAmenaza = servicioEcosistemaAmenaza;
            _servicioEstadoConservacion = servicioEstadoConservacion;
            _configuration = configuration;
            _servicioPais = servicioPais;
        }

        [HttpGet("Ecosistemas")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public IActionResult GetAll()
        {
            IEnumerable<EcosistemaMarinoDTO> especies = _servicioEcoMarino.GetAll();
            return Ok(especies);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                EcosistemaMarinoDTO ecoDTO = _servicioEcoMarino.GetById(id);
                return Ok(ecoDTO);
            }
            catch (Exception ex)
            {
                 return NotFound(ex.Message);
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
                _servicioEcoMarino.Remove(id);
                return Ok("Eliminado con exito");
            }
            catch (ElementoNoValidoException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        //public IActionResult Post(string Nombre, int Area, string Latitud, string Longitud, int GradoPeligro, [Required] int Pais, [Required] int EstadoConservacion/*, IFormFile Imagen*/) 
        public IActionResult Post(EcosistemaMarinoDTO eco) 
        {
            try
            {
                //UbiGeografica ubi = new UbiGeografica();

                Regex regex = new Regex(@"^[0-9,-]+$");
                if (regex.IsMatch(eco.UbicacionGeografica.Latitud.ToString()) && regex.IsMatch(eco.UbicacionGeografica.Longitud.ToString()))
                {
                    Double.TryParse(eco.UbicacionGeografica.Latitud.ToString(), out double latitudParsed);
                    Double.TryParse(eco.UbicacionGeografica.Longitud.ToString(), out double longitudParsed);

                    UbiGeografica ubi = new UbiGeografica(latitudParsed, longitudParsed, eco.UbicacionGeografica.GradoPeligro);
                    ubi.Validate();
                }
                else
                {
                    throw new StringException("La longitud y latitud solo pueden llevar numeros y coma");
                }



                EstadoConservacionDTO EstadoC = _servicioEstadoConservacion.GetEstado(eco.EstadoConservacion.EstadoConservacionId);


                EcosistemaMarinoDTO ecoDTO = eco;
                ecoDTO.NombreMin = extraerValor("ParametersTopes:NombreMin");
                ecoDTO.NombreMax = extraerValor("ParametersTopes:NombreMax");

                EcosistemaMarinoDTO nuevoEco = _servicioEcoMarino.Add(ecoDTO);
                /*

                string ArchivoName = Path.GetFileName(Imagen.FileName);
                string extension = Path.GetExtension(ArchivoName);

                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                {
                    ViewBag.Msg = "Formatos de imagen admitidos: jpeg,jpg o png";
                    return View();
                }

                string rutaRaiz = _webHostEnvironment.WebRootPath;
                rutaRaiz = Path.Combine(rutaRaiz, "img", "ecosistemas");
                string nuevoNombre = nuevoEco.EcosistemaMarinoId.ToString() + "_001" + extension;
                string ruta = Path.Combine(rutaRaiz, nuevoNombre);
                using (FileStream stream = new FileStream(ruta, FileMode.Create))
                {
                    Imagen.CopyTo(stream);
                }*/
                
               return Ok(nuevoEco);
            }

            catch (ElementoNoValidoException ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Authorize]
        [HttpPost("AsociarAmenazaAEcosistema/{amenazaId}/{ecosistemaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AsociarAmenazaAEcosistema(int amenazaId, int ecosistemaId)
        {
            try
            {
                if (ecosistemaId > 0 && amenazaId > 0)
                {
                    _servicioEcosistemaAmenaza.Add(amenazaId, ecosistemaId);
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

        private int extraerValor(string clave)
        {
            int valor = 0;
            string strValor = _configuration[clave];
            Int32.TryParse(strValor, out valor);
            return valor;
        }

    }
}
