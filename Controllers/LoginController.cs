using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace UIWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RealizarLogin(Empleado e)
        {
            try
            {
                Empleado empleadoLogueado = Sistema.ObtenerInstancia().ObtenerUsuarioPorEmailYContrasena(e);
                HttpContext.Session.SetString("emailLogueado", empleadoLogueado.Mail);
                HttpContext.Session.SetString("rolLogueado", empleadoLogueado.Rol);
                HttpContext.Session.SetString("nombreLogueado", empleadoLogueado.Nombre);
                if (empleadoLogueado.Rol == "CAPATAZ")
                {
                    return Redirect("/Capataz/Index");
                }
                return Redirect("/Peon/Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = true;
                TempData["MensajeError"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("emailLogueado");
            HttpContext.Session.Remove("rolLogueado");
            HttpContext.Session.Remove("nombreLogueado");
            return RedirectToAction("Index");
        }
    }
}
