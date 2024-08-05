using Dominio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UIWeb.Controllers
{
    public class PeonController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "PEON")
            {
                Peon p = Sistema.ObtenerInstancia().DevolerPeonPorMail(HttpContext.Session.GetString("emailLogueado"));
                return View(p);
            }
            return Redirect("/Error/SinAutorizacion");
        }
        public IActionResult DatosPersonales()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "PEON")
            {
                Peon p = Sistema.ObtenerInstancia().DevolerPeonPorMail(HttpContext.Session.GetString("emailLogueado"));
                return View(p);
            }
            return Redirect("/Error/SinAutorizacion");
        }
        public IActionResult RegistrarVacunacion()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "PEON")
            {
                List<Ganado> g = Sistema.ObtenerInstancia().ListadoGanadaMayor3Meses();
                return View(g);
            }
            return Redirect("/Error/SinAutorizacion");
        }

        public IActionResult RealizarVacunacion(string codigo)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "PEON" && codigo != null)
            {
                Ganado g = Sistema.ObtenerInstancia().ObtenerGanadoPorCodigo(codigo);
                return View(g);
            }
            return Redirect("/Error/SinAutorizacion");
        }

        [HttpPost]

        public IActionResult RealizarVacunacion(string Codigo, string Nombre, DateTime FechaVacunacion)
        {
            if(HttpContext.Session.GetString("rolLogueado") == "PEON")
            {
                try
                {
                    Ganado g = Sistema.ObtenerInstancia().ObtenerGanadoPorCodigo(Codigo);
                    Vacuna v = Sistema.ObtenerInstancia().ObtenerVacunaPorNombre(Nombre);
                    Sistema.ObtenerInstancia().AsignarVacunaAGanado(g, v, FechaVacunacion);
                    TempData["error"] = false;
                }
                catch (Exception ex)
                {
                    TempData["error"] = true;
                    TempData["MensajeError"] = ex.Message;
                }
                return RedirectToAction("RegistrarVacunacion");
            }
            return Redirect("/Error/SinAutorizacion");
        }
        public IActionResult Tareas()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "PEON")
            {
                List<Tarea> auxiliar = Sistema.ObtenerInstancia().TareasNoCompletadasPorPeon(HttpContext.Session.GetString("emailLogueado"));
                return View(auxiliar);
            }
            return Redirect("/Error/SinAutorizacion");
        }
        public IActionResult CompletarTarea(int id)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "PEON" && id != 0)
            {
                Tarea t = Sistema.ObtenerInstancia().ObtenerTareaPorId(id, HttpContext.Session.GetString("emailLogueado"));
                return View(t);
            }
            return Redirect("/Error/SinAutorizacion");
        }
        [HttpPost]
        public IActionResult CompletarTarea(int id, string ComentarioPeon)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "PEON")
            {
                try
                {
                    string email = HttpContext.Session.GetString("emailLogueado");
                    Peon p = Sistema.ObtenerInstancia().DevolerPeonPorMail(email);
                    Tarea t = Sistema.ObtenerInstancia().ObtenerTareaPorId(id, email);
                    Sistema.ObtenerInstancia().ValidarTarea(t,ComentarioPeon);
                    TempData["error"] = false;
                } catch (Exception ex)
                {
                    TempData["error"] = true;
                    TempData["MensajeError"] = ex.Message;
                }
                return RedirectToAction("Tareas");
            }
            return Redirect("/Error/SinAutorizacion");
        }
    }
}
