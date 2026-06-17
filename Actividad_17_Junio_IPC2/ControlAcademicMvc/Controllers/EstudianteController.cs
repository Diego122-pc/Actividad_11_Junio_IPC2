using ControlAcademicMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControlAcademicMvc.Controllers;

public class EstudianteController : Controller
{
    // Lista estatica para simular la capa de datos.
    private static readonly List<Estudiante> BaseDatosMemoria =
    [
        new() { Carne = 2026012, Nombre = "Fernando Velasquez", Promedio = 91.5 },
        new() { Carne = 2026045, Nombre = "Maria Mercedes", Promedio = 84.0 }
    ];

    [HttpGet]
    public IActionResult Listar()
    {
        return View(BaseDatosMemoria);
    }

    [HttpPost]
    public IActionResult Registrar([FromBody] Estudiante nuevoEstudiante)
    {
        if (nuevoEstudiante is null)
        {
            return BadRequest(new { mensaje = "El estudiante es requerido." });
        }

        if (nuevoEstudiante.Carne <= 0 || string.IsNullOrWhiteSpace(nuevoEstudiante.Nombre))
        {
            return BadRequest(new { mensaje = "Carne y nombre son obligatorios." });
        }

        BaseDatosMemoria.Add(nuevoEstudiante);
        return Created($"/Estudiante/Historial/{nuevoEstudiante.Carne}", nuevoEstudiante);
    }
}
