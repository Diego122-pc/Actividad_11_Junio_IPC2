namespace ControlAcademicMvc.Models;

// Entidad simple para transportar datos del estudiante.
public class Estudiante
{
    // Numero de carne del estudiante.
    public int Carne { get; set; }

    // Nombre completo registrado.
    public string Nombre { get; set; } = string.Empty;

    // Promedio academico acumulado.
    public double Promedio { get; set; }
}
