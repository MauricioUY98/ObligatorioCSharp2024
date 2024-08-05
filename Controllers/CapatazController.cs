using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace UIWeb.Controllers
{
    public class CapatazController : Controller
    {
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                Capataz c = Sistema.ObtenerInstancia().DevolerCapatazPorMail(HttpContext.Session.GetString("emailLogueado"));
                return View(c);
            }
            return Redirect("/Error/SinAutorizacion");
        }

        public IActionResult AltaBovino()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {

                return View();
            }
            return Redirect("/Error/SinAutorizacion");
        }

        [HttpPost]

        public IActionResult AltaBovino(Bovino bovino)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                try
                {
                    Sistema.ObtenerInstancia().AltaBovino(bovino);
                    TempData["error"] = false;

                }
                catch (Exception ex)
                {
                    TempData["error"] = true;
                    TempData["MensajeError"] = ex.Message;
                }
                return RedirectToAction("AltaBovino");
            }
            return Redirect("/Error/SinAutorizacion");
        }

        public IActionResult ListadoGanadoLibre()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                List<Ganado> auxiliar = Sistema.ObtenerInstancia().ListadoGanadoSinPotrero();
                return View(auxiliar);
            }
            return Redirect("/Error/SinAutorizacion");
        }

        public IActionResult AsignacionGanadoAPotrero(string codigo)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ" && codigo != null)
            {
                Ganado g = Sistema.ObtenerInstancia().ObtenerGanadoPorCodigo(codigo);
                return View(g);
            }
            return Redirect("/Error/SinAutorizacion");
        }

        [HttpPost]

        public IActionResult AsignacionGanadoAPotrero(int id, string Codigo)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                try
                {
                    Ganado g = Sistema.ObtenerInstancia().ObtenerGanadoPorCodigo(Codigo);
                    Potrero p = Sistema.ObtenerInstancia().ObtenerPotreroPorCodigo(id);
                    Sistema.ObtenerInstancia().AgregarGanadoAPotrero(g, p);
                    TempData["error"] = false;
                }
                catch (Exception ex)
                {
                    TempData["error"] = true;
                    TempData["MensajeError"] = ex.Message;
                }
                return RedirectToAction("ListadoGanadoLibre");
            }
            return Redirect("/Error/SinAutorizacion");
        }

        public IActionResult ListadoPeones()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                Capataz c = Sistema.ObtenerInstancia().DevolerCapatazPorMail(HttpContext.Session.GetString("emailLogueado"));
                return View(c.CantPersonas);
            }
            return Redirect("/Error/SinAutorizacion");
        }

        public IActionResult MostrarTareaPeon(string Mail)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                Peon peon = Sistema.ObtenerInstancia().DevolerPeonPorMail(Mail);
                return View(peon);
            }
            return Redirect("/Error/SinAutorizacion");
        }

        public IActionResult AsignacionDeTareasAPeones()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                List<Peon> auxiliar = Sistema.ObtenerInstancia().ListadoPeones();
                return View(auxiliar);
            }
            return Redirect("/Error/SinAutorizacion");
        }

        [HttpPost]
        public IActionResult AsignacionDeTareasAPeones(Tarea t, string Mail)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                try
                {
                    Peon p = Sistema.ObtenerInstancia().DevolerPeonPorMail(Mail);
                    Capataz c = Sistema.ObtenerInstancia().DevolerCapatazPorMail(HttpContext.Session.GetString("emailLogueado"));
                    t.TareaCompletada = false;
                    t.FechaRealizacion = DateTime.Now;
                    c.AsignarTarea(t, p);

                    TempData["error"] = false;

                }
                catch (Exception ex)
                {
                    TempData["error"] = true;
                    TempData["MensajeError"] = ex.Message;
                }
                return RedirectToAction("AsignacionDeTareasAPeones");
            }
            return Redirect("/Error/SinAutorizacion");
        }

        public IActionResult ListadoPotreros()
        {
            List<Potrero> potreros = Sistema.ObtenerInstancia().ObtenerListaPotreroOrdenado();
            return View(potreros);
        }

        public IActionResult ListadoGanados()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                List<Ganado> auxiliar = Sistema.ObtenerInstancia().ObtenerListaGanado();
                return View(auxiliar);
            }
            return Redirect("/Error/SinAutorizacion");
        }

        [HttpPost]

        public IActionResult ListadoGanados(string Tipo, int Peso)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "CAPATAZ")
            {
                List<Ganado> auxiliar = Sistema.ObtenerInstancia().ObtenerGanadosPorTipoYPeso(Tipo, Peso);
                return View(auxiliar);
            }
            return Redirect("/Error/SinAutorizacion");
        }
    }
}
