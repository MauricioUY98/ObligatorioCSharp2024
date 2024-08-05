using Microsoft.AspNetCore.Mvc;

namespace UIWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult SinAutorizacion() //Realizamos un Controller especifico con un Index para el caso de que un Usuario no tenga autorizacion para entrar a una View, los redirecciones hacia aca. 
        {
            return View();
        }
    }
}
