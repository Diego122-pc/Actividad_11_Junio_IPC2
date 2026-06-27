# Integracion de Datos

## Parte 1

### Tabla comparativa

| Formato | Ventajas | Desventajas |
| --- | --- | --- |
| CSV | Es simple de leer y escribir, ocupa poco espacio y funciona muy bien para cargas masivas con muchos registros | No guarda estructura compleja, no describe tipos de datos y puede fallar si una coma aparece dentro de un valor |
| XML | Permite datos estructurados, usa etiquetas descriptivas y es util cuando se necesita validar o representar jerarquias | Es mas pesado que CSV, ocupa mas espacio y su lectura puede ser mas lenta en archivos grandes |

### Serializacion y deserializacion

La serializacion convierte un objeto de C# en texto JSON para poder enviarlo, guardarlo o compartirlo con otro sistema

La deserializacion hace el proceso contrario, toma un texto JSON y lo convierte en un objeto de C# para poder usar sus datos dentro del programa

Con `System.Text.Json`, la serializacion se puede hacer con `JsonSerializer.Serialize(objeto)` y la deserializacion con `JsonSerializer.Deserialize<Tipo>(json)`

### Problema N mas 1

El problema N mas 1 aparece cuando se procesa un archivo masivo y por cada fila se hace una consulta o una insercion individual en la base de datos

Esto vuelve lento el proceso porque si el archivo tiene mil filas, el sistema puede terminar haciendo mil operaciones separadas

La solucion es trabajar por lotes

Primero se leen y preparan los registros en memoria de forma controlada y luego se guardan juntos con `AddRange()` y una sola llamada a `SaveChangesAsync()`

## Parte 2

### Desafio 1

```csharp
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class Alumno
{
    public int Id { get; set; }
    public string Carnet { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
}

public class ServicioAlumnos
{
    private readonly HttpClient _httpClient;

    public ServicioAlumnos(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Alumno?> ObtenerAlumnoAsync()
    {
        try
        {
            using HttpResponseMessage respuesta = await _httpClient.GetAsync("https://api.usac.edu/v1/alumnos");

            respuesta.EnsureSuccessStatusCode();

            string contenidoJson = await respuesta.Content.ReadAsStringAsync();

            JsonSerializerOptions opcionesJson = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Alumno? alumno = JsonSerializer.Deserialize<Alumno>(contenidoJson, opcionesJson);

            return alumno;
        }
        catch (HttpRequestException error)
        {
            Console.WriteLine($"No se pudo consultar el endpoint de alumnos: {error.Message}");
            return null;
        }
        catch (JsonException error)
        {
            Console.WriteLine($"La respuesta no tenia el formato esperado: {error.Message}");
            return null;
        }
    }
}
```

### Desafio 2

```csharp
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AlumnoCarga
{
    public int Id { get; set; }
    public string Carnet { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
}

public class ControlAcademicoContext : DbContext
{
    public DbSet<AlumnoCarga> Alumnos { get; set; }

    public ControlAcademicoContext(DbContextOptions<ControlAcademicoContext> opciones) : base(opciones)
    {
    }
}

[ApiController]
[Route("api/alumnos")]
public class AlumnosController : ControllerBase
{
    private readonly ControlAcademicoContext _contexto;

    public AlumnosController(ControlAcademicoContext contexto)
    {
        _contexto = contexto;
    }

    [HttpPost("carga-csv")]
    public async Task<IActionResult> CargarAlumnosDesdeCsv(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
        {
            return BadRequest("Debe enviar un archivo CSV con datos");
        }

        List<AlumnoCarga> alumnosParaGuardar = new List<AlumnoCarga>();

        using StreamReader lector = new StreamReader(archivo.OpenReadStream());

        string? encabezado = await lector.ReadLineAsync();
        string? lineaActual;

        while ((lineaActual = await lector.ReadLineAsync()) != null)
        {
            if (string.IsNullOrWhiteSpace(lineaActual))
            {
                continue;
            }

            AlumnoCarga alumno = ConvertirLineaEnAlumno(lineaActual);
            alumnosParaGuardar.Add(alumno);
        }

        _contexto.Alumnos.AddRange(alumnosParaGuardar);
        await _contexto.SaveChangesAsync();

        return Ok(new
        {
            mensaje = "Carga masiva terminada",
            totalRegistros = alumnosParaGuardar.Count
        });
    }

    private AlumnoCarga ConvertirLineaEnAlumno(string lineaCsv)
    {
        string[] columnas = lineaCsv.Split(',');

        return new AlumnoCarga
        {
            Carnet = columnas[0].Trim(),
            Nombre = columnas[1].Trim(),
            Correo = columnas[2].Trim()
        };
    }
}
```

## Parte 3

Facultad de Ingenieria USAC (2026) Sesion 20: Integracion de Datos, Consumo de APIs Externas y Carga Masiva CSV XML, Laboratorio del curso Introduccion a la Programacion y Computacion 2, Guatemala
