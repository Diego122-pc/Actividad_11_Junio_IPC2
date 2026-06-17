using Microsoft.AspNetCore.Mvc;

namespace ControlAcademicMvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Listar", "Estudiante");
    }
}
