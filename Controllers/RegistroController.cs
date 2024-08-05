using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace UIWeb.Controllers
{
    public class RegistroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RealizarRegistro(Peon peon)
        {
            try
            {
                Sistema.ObtenerInstancia().AltaPeon(peon);
                TempData["error"] = false;
                return Redirect("/Login/Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = true;
                TempData["MensajeError"] = ex.Message;
                return Redirect("/Registro/Index");
            }
        }
    }
}
