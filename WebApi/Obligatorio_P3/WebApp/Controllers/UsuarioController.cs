using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        protected IServicioUsuario _servicioUsuario;
        protected IServicioAudit _servicioAudit;
        public UsuarioController(IServicioUsuario servicioUsuario, IServicioAudit servicioAudit)
        {
            _servicioUsuario = servicioUsuario;
            _servicioAudit = servicioAudit;
        }
       
        public ActionResult Index()
        {
            return View();
        }

       // Register y Login al sistema
        [HttpGet]
        public IActionResult LogIn()
        {
            if (HttpContext.Session.Get("email") == null) {
                return View();
            } else { 
                return RedirectToAction("Index","Home"); 
            }

        }

        [HttpPost]
        public IActionResult Login(string Alias, string password)
        {
            if (!String.IsNullOrEmpty(Alias) && !String.IsNullOrEmpty(password))
            {
                UsuarioDTO usuario = new UsuarioDTO { Alias = Alias, Password = password };

                try
                {
                    UsuarioDTO userLogged = _servicioUsuario.FindUser(usuario);
                    if (userLogged != null)
                    {
                        HttpContext.Session.SetString("email", userLogged.Alias);
                        return RedirectToAction("Index", "Home");

                    }
                    
                }catch(Exception ex)
                {
                    ViewBag.DatosErroneos = ex.Message;
                }
                
                return View();

            }
            else
            {
                ViewBag.DatosErroneos = "Debe completar los campos";
                return View();
            }
        }


        [HttpGet]
        public IActionResult Logout()
        {
            if (HttpContext.Session.Get("email") != null) {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Usuario");
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            //if (HttpContext.Session.GetString("email") == "admin1")
            //{
                
                return View();

            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");

            //}
        }

        [HttpPost]
        public IActionResult Register(string Alias, string Password, string PassConfirm)
        {
            try
            {
                if (!String.IsNullOrEmpty(Alias) && !String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(PassConfirm))
                {
                    if (Password.ToLower() == PassConfirm.ToLower())
                    {
                        UsuarioDTO usuario = new UsuarioDTO { Alias = Alias, Password = Password };
                        try
                        {
                            usuario = _servicioUsuario.Add(usuario);
                            _servicioAudit.Log(HttpContext.Session.GetString("email") ?? "NULL", usuario.UsuarioDTOId, "Usuario (Add)");
                            ViewBag.Msg = "El usuario se creó correctamente!";

                        }
                        catch (Exception ex)
                        {
                            ViewBag.Msg = ex.Message;
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "Las contraseñas deben coincidir";
                    }
                }
                else
                {
                    ViewBag.Msg = "Debe completar todos los campos";
                }

                
                    
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Error: " + ex.Message;
            }

            return View();
        }
    }
}
