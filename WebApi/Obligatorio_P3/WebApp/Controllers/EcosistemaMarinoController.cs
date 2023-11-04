using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.Servicios;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebApp.Controllers {
    public class EcosistemaMarinoController : Controller {

        protected IServicioEcosistemaMarino _servicioEcosistemaMarino;
        protected IServicioPais _servicioPais;
        protected IServicioEstadoConservacion _servicioEstadoConservacion;
        protected IServicioEcosistemaAmenaza _servicioEcosistemaAmenaza;
        protected IServicioAmenaza _servicioAmenaza;
        protected IServicioAudit _servicioAudit;
        protected IConfiguration _configuration;
        protected IWebHostEnvironment _webHostEnvironment { get; set; }




        public EcosistemaMarinoController(IServicioEcosistemaMarino servicioEcosistemaMarino,
            IServicioPais servicioPais,
            IServicioAmenaza servicioAmenaza,
            IServicioAudit servicioAudit,
            IServicioEcosistemaAmenaza servicioEcosistemaAmenaza,
            IServicioEstadoConservacion servicioEstadoConservacion,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment) 
        {
            _servicioEcosistemaMarino = servicioEcosistemaMarino;
            _servicioPais = servicioPais;
            _servicioAmenaza = servicioAmenaza;
            _servicioAudit = servicioAudit;
            _servicioEcosistemaAmenaza = servicioEcosistemaAmenaza;
            _servicioEstadoConservacion = servicioEstadoConservacion;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        
        public ActionResult Index() {
            IEnumerable<EcosistemaMarinoDTO> ecos = _servicioEcosistemaMarino.GetAll();
            foreach(EcosistemaMarinoDTO e in ecos) {
                e.ImagenURL = ObtenerNombreImagen(e.EcosistemaMarinoId);
            }
            ViewBag.Ecosistema = ecos;

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id) {
            if (HttpContext.Session.Get("email") != null) {
                try {
                    _servicioEcosistemaMarino.Remove(id);
                    IEnumerable<EcosistemaMarinoDTO> ecos = _servicioEcosistemaMarino.GetAll();
                    foreach (EcosistemaMarinoDTO e in ecos) {
                        e.ImagenURL = ObtenerNombreImagen(e.EcosistemaMarinoId);
                    }
                    ViewBag.Ecosistema = ecos;
                    ViewBag.Msg = "El ecosistema ha sido eliminado con exito";
                    _servicioAudit.Log(HttpContext.Session.GetString("email") ?? "NULL", id, "Ecosistema (Delete)");
                    BorrarImagen(id);

                }
                catch (Exception ex) {
                    TempData["msg"] = ex.Message;

                }

                return RedirectToAction("Index");
            }
            else {
                TempData["msg"] = "Debe iniciar sesion para realizar esa accion";
                return RedirectToAction("Login", "Usuario");
            }
        }

        // GET: EcosistemaMarinoController/Create
        public ActionResult Create() {
            if(HttpContext.Session.Get("email") != null) {
                IEnumerable<EstadoConservacionDTO> estados = _servicioEstadoConservacion.GetAll();
                IEnumerable<PaisDTO> paises = _servicioPais.GetAll();

                ViewBag.estados = estados;
                ViewBag.paises = paises;
                return View();
            }
            else {
                TempData["msg"] = "Debe iniciar sesion para realizar esa accion";
                return RedirectToAction("Login", "Usuario");
            }
            
            
        }

        
        [HttpPost]
        public ActionResult Create(string Nombre, int Area, string Latitud, string Longitud ,int GradoPeligro,int Pais, int EstadoConservacion,IFormFile Imagen) {
            try 
            {
                UbiGeografica ubi = new UbiGeografica();

                Regex regex = new Regex(@"^[0-9,-]+$");
                if(regex.IsMatch(Latitud) && regex.IsMatch(Longitud))
                {
                    Double.TryParse(Latitud, out double latitudParsed);
                    Double.TryParse(Longitud, out double longitudParsed);

                    ubi = new UbiGeografica(latitudParsed, longitudParsed, GradoPeligro);
                    ubi.Validate();
                }
                else
                {
                    throw new StringException("La longitud y latitud solo pueden llevar numeros y coma");
                }



                EstadoConservacionDTO EstadoC = _servicioEstadoConservacion.GetEstado(EstadoConservacion);
                

                EcosistemaMarinoDTO ecoDTO = new EcosistemaMarinoDTO(Nombre, ubi, Area, EstadoC, Pais);
                ecoDTO.NombreMin = extraerValor("ParametersTopes:NombreMin");
                ecoDTO.NombreMax = extraerValor("ParametersTopes:NombreMax");

                EcosistemaMarinoDTO nuevoEco = _servicioEcosistemaMarino.Add(ecoDTO);


                string ArchivoName = Path.GetFileName(Imagen.FileName);
                string extension = Path.GetExtension(ArchivoName);

                if (extension != ".jpg" && extension !=".jpeg" && extension!= ".png") {
                    ViewBag.Msg = "Formatos de imagen admitidos: jpeg,jpg o png";
                    return View();
                }

                string rutaRaiz = _webHostEnvironment.WebRootPath;
                rutaRaiz = Path.Combine(rutaRaiz, "img","ecosistemas");
                string nuevoNombre = nuevoEco.EcosistemaMarinoId.ToString() + "_001" + extension;
                string ruta = Path.Combine(rutaRaiz, nuevoNombre);
                using (FileStream stream = new FileStream(ruta, FileMode.Create)) {
                    Imagen.CopyTo(stream);
                }

                _servicioAudit.Log(HttpContext.Session.GetString("email") ?? "NULL", nuevoEco.EcosistemaMarinoId, "Ecosistema (Add)");
                ViewBag.Msg = "Ecosistema creado!";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) {
                IEnumerable<EstadoConservacionDTO> estados = _servicioEstadoConservacion.GetAll();
                IEnumerable<PaisDTO> paises = _servicioPais.GetAll();
                ViewBag.estados = estados;
                ViewBag.paises = paises;
                ViewBag.Msg = ex.Message;
                return View();
            }
        }

        private int extraerValor(string clave) {
            int valor = 0;
            string strValor = _configuration[clave];
            Int32.TryParse(strValor, out valor);
            return valor;
        }

        public string ObtenerNombreImagen(int id) {
           
            // Construye el nombre del archivo de imagen en función del ID.
            string nombreArchivo = id + "_001";

            // Comprueba las extensiones posibles (jpg, jpeg, png) y obtén la ruta si existe.
            string[] extensiones = { "jpg", "jpeg", "png" };

            foreach (string extension in extensiones) {
                //string rutaImagen = Path.Combine(carpetaImagenes, nombreArchivo + "." + extension);
                //string rutaImagen = carpetaImagenes + "/" + nombreArchivo + "." + extension;
                string rutaImagen = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "img", "ecosistemas", nombreArchivo + "." + extension);

                if (System.IO.File.Exists(rutaImagen)) {
                    return nombreArchivo + "." + extension;
                }
            }

            // Devuelve una cadena vacía si la imagen no se encuentra.
            return string.Empty;
        }

        public void BorrarImagen(int id) {
            // Construye el nombre del archivo de imagen en función del ID.
            string nombreArchivo = id + "_001";

            // Comprueba las extensiones posibles (jpg, jpeg, png) y obtén la ruta si existe.
            string[] extensiones = { "jpg", "jpeg", "png" };

            foreach (string extension in extensiones) {
                //string rutaImagen = Path.Combine(carpetaImagenes, nombreArchivo + "." + extension);
                //string rutaImagen = carpetaImagenes + "/" + nombreArchivo + "." + extension;
                string rutaImagen = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "img", "ecosistemas", nombreArchivo + "." + extension);

                if (System.IO.File.Exists(rutaImagen)) {
                    System.IO.File.Delete(rutaImagen);
                }
            }


        }

        [HttpGet]
        public IActionResult AsignarAmenaza()
        {
            if (HttpContext.Session.Get("email") != null) {
                ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();
                ViewBag.Amenazas = _servicioAmenaza.GetAll();

                return View();
            }
            else {
                TempData["msg"] = "Debe iniciar sesion para realizar esa accion";
                return RedirectToAction("Login", "Usuario");
            }
            
        }

        [HttpPost]
        public IActionResult AsignarAmenaza(int AmenazaId, int EcosistemaId)
        {
            try
            {
                if (EcosistemaId > 0 && AmenazaId > 0 )
                {
                    _servicioEcosistemaAmenaza.Add(AmenazaId, EcosistemaId);
                    _servicioAudit.Log(HttpContext.Session.GetString("email") ?? "NULL", EcosistemaId, "Ecosistema (Asig. Ame)");
                }

                TempData["msg"] = "La asociacion ha sido realizada";
                return RedirectToAction("AsignarAmenaza");

            }
            catch (Exception ec)
            {

                TempData["msg"] = ec.Message;
                return RedirectToAction("AsignarAmenaza");
            }

        }
    }
}
