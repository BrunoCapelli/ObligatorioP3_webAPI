using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Servicios.IServicios;
using Servicios.Servicios;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    public class EspecieController : Controller
    {
        protected IServicioEspecie _servicioEspecie;
        protected IServicioAmenaza _servicioAmenaza;
        protected IServicioEspecieAmenaza _servicioEspecieAmenaza;
        protected IServicioEstadoConservacion _servicioEstadoConservacion;
        protected IServicioEcosistemaMarino _servicioEcosistemaMarino;
        protected IServicioAudit _servicioAudit;
        protected IConfiguration _configuration;

        protected IServicioEcosistemaMarinoEspecie _servicioEcosistemaMarinoEspecie;

        IWebHostEnvironment _webHostEnvironment { get; set; }

        public EspecieController(IServicioEspecie servicioEspecie,
            IServicioEstadoConservacion estadoConservacion, 
            IServicioEcosistemaMarino servicioEcosistemaMarino,
            IServicioEspecieAmenaza servicioEspecieAmenaza,
            IServicioAmenaza servicioAmenaza,
            IServicioAudit servicioAudit,
            IWebHostEnvironment webHostEnvironment, 
            IConfiguration configuration,
            IServicioEcosistemaMarinoEspecie servicioEcosistemaMarinoEspecie) 
        {
            _servicioEspecie = servicioEspecie;
            _servicioAmenaza = servicioAmenaza;
            _servicioAudit = servicioAudit;
            _servicioEstadoConservacion = estadoConservacion;
            _servicioEspecieAmenaza = servicioEspecieAmenaza;
            _servicioEcosistemaMarino = servicioEcosistemaMarino;
            _servicioEcosistemaMarinoEspecie = servicioEcosistemaMarinoEspecie;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();
            ViewBag.Estados = _servicioEstadoConservacion.GetAll();
            IEnumerable<EspecieDTO> especies = _servicioEspecie.GetAll();
            foreach (EspecieDTO e in especies) {
                e.ImagenURL = ObtenerNombreImagen(e.EspecieId);
            }

            ViewBag.Especies = especies;
            return View();
        }


        [HttpPost]
        public IActionResult Delete(int id) {
            if (HttpContext.Session.Get("email") != null) {
                try {

                    _servicioEspecie.Remove(id);
                    IEnumerable<EspecieDTO> especies = _servicioEspecie.GetAll();
                    ViewBag.Especies = especies;
                    foreach (EspecieDTO e in especies) {
                        e.ImagenURL = ObtenerNombreImagen(e.EspecieId);
                    }

                    _servicioAudit.Log(HttpContext.Session.GetString("email") ?? "NULL", id, "Especie (Delete)");
                    ViewBag.Msg = "La especie ha sido eliminada con exito";
                    BorrarImagen(id);
                }
                catch (Exception ex) {
                    IEnumerable<EspecieDTO> especies = _servicioEspecie.GetAll();
                    ViewBag.Especies = especies;
                    ViewBag.Msg = ex.Message;

                }


                    ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();
                return View("Index");
            }else {
                TempData["msg"] = "Debe iniciar sesion para realizar esa accion";
                return RedirectToAction("Login", "Usuario");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.Get("email") != null) {
                IEnumerable<EstadoConservacionDTO> estados = _servicioEstadoConservacion.GetAll();
                ViewBag.estados = estados;
                return View();
            }
            else {
                TempData["msg"] = "Debe iniciar sesion para realizar esa accion";
                return RedirectToAction("Login", "Usuario");
            }
            
        }

        [HttpPost]
        public IActionResult Create(string NombreCientifico, string NombreVulgar, string Descripcion, string PesoMin, string PesoMax, int EstadoConservacion, IFormFile Imagen)
        {
            ViewBag.Especies = _servicioEspecie.GetAll();
            ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();
            try
            {
                Double.TryParse(PesoMin, out double pesoMinParsed);
                Double.TryParse(PesoMax, out double pesoMaxParsed);

                EstadoConservacionDTO EstadoC = _servicioEstadoConservacion.GetEstado(EstadoConservacion);

                EspecieDTO newEspecie = new EspecieDTO(NombreCientifico, NombreVulgar, Descripcion, pesoMinParsed, pesoMaxParsed, EstadoC);
                newEspecie.NombreMin = extraerValor("ParametersTopes:NombreMin");
                newEspecie.NombreMax = extraerValor("ParametersTopes:NombreMax");
                newEspecie.DescripcionMin = extraerValor("ParametersTopes:DescripcionMin");
                newEspecie.DescripcionMax = extraerValor("ParametersTopes:DescripcionMax");

                
                EspecieDTO especieCreada = _servicioEspecie.Add(newEspecie);

                string ArchivoName = Path.GetFileName(Imagen.FileName);
                //string fileName = Path.GetFileNameWithoutExtension(Imagen.FileName);
                string extension = Path.GetExtension(ArchivoName);

                if(Imagen != null)
                {
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        ViewBag.Msg = "Formatos de imagen admitidos: jpeg,jpg o png";
                        throw new Exception("La imagen ingresada no es valida");
                    }

                    string rutaRaiz = _webHostEnvironment.WebRootPath;
                    rutaRaiz = Path.Combine(rutaRaiz, "img", "especies");
                    string nuevoNombre = especieCreada.EspecieId.ToString() + "_001" + extension;
                    string ruta = Path.Combine(rutaRaiz, nuevoNombre);
                    using (FileStream stream = new FileStream(ruta, FileMode.Create))
                    {
                        Imagen.CopyTo(stream);
                    }
                    _servicioAudit.Log(HttpContext.Session.GetString("email") ?? "NULL", especieCreada.EspecieId, "Especie (Add)");
                    ViewBag.Msg = "Especie creada!";
                }
                else
                {
                    throw new Exception("La imagen ingresada no es valida");
                }
                
                

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Msg = ex.Message;
                IEnumerable<EstadoConservacionDTO> estados = _servicioEstadoConservacion.GetAll();
                ViewBag.estados = estados;

                return View();
            }

            

        }

        public string ObtenerNombreImagen(int id) {

            // Construye el nombre del archivo de imagen en función del ID.
            string nombreArchivo = id + "_001";

            // Comprueba las extensiones posibles (jpg, jpeg, png) y obtén la ruta si existe.
            string[] extensiones = { "jpg", "jpeg", "png" };

            foreach (string extension in extensiones) {
                //string rutaImagen = Path.Combine(carpetaImagenes, nombreArchivo + "." + extension);
                //string rutaImagen = carpetaImagenes + "/" + nombreArchivo + "." + extension;
                string rutaImagen = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "img", "especies", nombreArchivo + "." + extension);

                if (System.IO.File.Exists(rutaImagen)) {
                    return nombreArchivo + "." + extension;
                }
            }

            // Devuelve una cadena vacía si la imagen no se encuentra.
            return string.Empty;
        }

        private int extraerValor(string clave) {
            int valor = 0;
            string strValor = _configuration[clave];
            Int32.TryParse(strValor, out valor);
            return valor;
        }

        public void BorrarImagen(int id) {
            // Construye el nombre del archivo de imagen en función del ID.
            string nombreArchivo = id + "_001";

            // Comprueba las extensiones posibles (jpg, jpeg, png) y obtén la ruta si existe.
            string[] extensiones = { "jpg", "jpeg", "png" };

            foreach (string extension in extensiones) {
                //string rutaImagen = Path.Combine(carpetaImagenes, nombreArchivo + "." + extension);
                //string rutaImagen = carpetaImagenes + "/" + nombreArchivo + "." + extension;
                string rutaImagen = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "img", "especies", nombreArchivo + "." + extension);

                if (System.IO.File.Exists(rutaImagen)) {
                    System.IO.File.Delete(rutaImagen);
                }
            }


        }

        [HttpGet]
        public IActionResult AsignarEcosistema() {
            if (HttpContext.Session.Get("email") != null) {
                ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();
                ViewBag.Especies = _servicioEspecie.GetAll();


                return View();

            } else {
                TempData["msg"] = "Debe iniciar sesion para realizar esa accion";
                return RedirectToAction("Login", "Usuario");
            }
        }
        [HttpPost]
        public IActionResult AsignarEcosistema(int EspecieId, int EcosistemaId)
        {
            try
            {
                if (EcosistemaId != 0 && EspecieId != 0)
                {

                    EcosistemaMarinoEspecieDTO emeDTO =  _servicioEcosistemaMarinoEspecie.Add(EcosistemaId, EspecieId);
                    _servicioAudit.Log(HttpContext.Session.GetString("email") ?? "NULL", emeDTO.EcosistemaMarinoId, "Especie (Asig. Eco)");
                }
                
                return RedirectToAction("Index");

            }catch(Exception ec)
            {
                
                TempData["msg"] = ec.Message;
                return RedirectToAction("AsignarEcosistema");
            }

        }

        [HttpPost]
        public IActionResult FiltrarPorNombreCientifico(string fNombreCientifico)
        {
            if (!String.IsNullOrEmpty(fNombreCientifico))
            {
                IEnumerable<EspecieDTO> Especies = _servicioEspecie.FiltrarPorNombreCientifico(fNombreCientifico);
                foreach (EspecieDTO e in Especies)
                {
                    e.ImagenURL = ObtenerNombreImagen(e.EspecieId);
                }

                ViewBag.Especies = Especies;    
                return View("Index");

            }
            else
            {
                ViewBag.Especies = _servicioEspecie.GetAll();
            }
            ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();

            return View("Index");
        }
        [HttpPost]
        public IActionResult FiltrarPorGradoDeConservacion(int RangosExtincion)
        {
            if (RangosExtincion > 0)
            {
                IEnumerable<EspecieDTO> Especies = _servicioEspecie.FiltrarPorGradoDeConservacion(RangosExtincion);

                foreach (EspecieDTO e in Especies)
                {
                    e.ImagenURL = ObtenerNombreImagen(e.EspecieId);
                }
                ViewBag.Especies = Especies;
                
            }
            else
            {
                ViewBag.Especies = _servicioEspecie.GetAll();
            }
            ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();
            ViewBag.Estados = _servicioEstadoConservacion.GetAll();

            return View("Index");
        }

        
        [HttpPost]
        public IActionResult FiltrarPorPeso(int pesoDesde, int pesoHasta)
        {
            if (pesoDesde > 0 && pesoHasta >0)
            {
                IEnumerable<EspecieDTO> Especies = _servicioEspecie.FiltrarPorPeso(pesoDesde, pesoHasta);
                foreach (EspecieDTO e in Especies)
                {
                    e.ImagenURL = ObtenerNombreImagen(e.EspecieId);
                }
                ViewBag.Especies = Especies;
            }
            else
            {
                ViewBag.Especies = _servicioEspecie.GetAll();
            }
            ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();
            ViewBag.Estados = _servicioEstadoConservacion.GetAll();

            return View("Index");
        }

        [HttpPost]
        public IActionResult FiltrarPorEcosistema(int EcosistemaID)
        {
            if(EcosistemaID > 0)
            {
                IEnumerable<EspecieDTO> Especies = _servicioEspecie.FiltrarPorEcosistema(EcosistemaID);
                foreach (EspecieDTO e in Especies)
                {
                    e.ImagenURL = ObtenerNombreImagen(e.EspecieId);
                }
                ViewBag.Especies = Especies;
            }
            ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();
            ViewBag.Estados = _servicioEstadoConservacion.GetAll();

            return View("Index");
        }

        // FiltrarPorEspecieQueNoHabita
        [HttpPost]
        public IActionResult FiltrarPorEspecieQueNoHabita(int EspecieId)
        {
            ViewBag.Especies = _servicioEspecie.GetAll();
            if (EspecieId > 0)
            {
                ViewBag.Ecosistemas = _servicioEspecie.FiltrarPorEspecieQueNoHabita(EspecieId);
            }
            else
            {
                ViewBag.Ecosistemas = _servicioEcosistemaMarino.GetAll();
            }

            return View();
           
        }

        [HttpGet]
        public IActionResult AsignarAmenaza()
        {
            if (HttpContext.Session.Get("email") != null) {
                ViewBag.Especies = _servicioEspecie.GetAll();
                ViewBag.Amenazas = _servicioAmenaza.GetAll();


                return View();

            }
            else {
                TempData["msg"] = "Debe iniciar sesion para realizar esa accion";
                return RedirectToAction("Login", "Usuario");
            }
        }

        [HttpPost]
        public IActionResult AsignarAmenaza(int AmenazaId, int EspecieId)
        {
            try
            {
                if (EspecieId > 0 && AmenazaId > 0)
                {
                    _servicioEspecieAmenaza.Add(AmenazaId, EspecieId);
                    _servicioAudit.Log(HttpContext.Session.GetString("email") ?? "NULL", EspecieId, "Especie (Asig. Ame)");
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
