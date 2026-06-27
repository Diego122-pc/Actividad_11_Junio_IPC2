# Actividad corta de laboratorio

# 202503934
# Diego Enrique Flores Ruiz

## De ADO.NET tradicional a la automatizacion con EF Core

## Parte 1 Diagnostico tecnico y brecha de impedancia

### 1 Brecha de impedancia

La brecha de impedancia aparece porque C# trabaja con objetos y SQL trabaja con tablas

En C# se piensa en clases propiedades e instancias

En SQL se piensa en tablas columnas y filas

EF Core ayuda a unir esos dos mundos porque toma las clases del programa y las relaciona con la base de datos

| Elemento en C# | Mapeo en SQL |
| --- | --- |
| Clase clasica POCO | Tabla |
| Propiedad o atributo | Columna |
| Instancia de objeto | Fila o registro |

### 2 Mitigacion de vulnerabilidades

En ADO NET tradicional el riesgo aparece cuando se arma una consulta SQL pegando texto con variables

Eso permite que un usuario malintencionado pueda meter instrucciones SQL dentro de un campo normal

EF Core reduce ese problema porque sus consultas LINQ se traducen a SQL usando parametros de forma automatica

Es decir que el valor enviado por el usuario viaja como dato y no como parte del comando SQL

En ADO NET la forma manual de mitigar este problema era usar parametros en el comando

Un ejemplo de eso era usar `cmd.Parameters.AddWithValue`

### 3 Uso de AsNoTracking

El metodo `AsNoTracking` se usa cuando solo se necesita leer informacion

En una consulta normal EF Core guarda los objetos en el rastreador de cambios por si despues se van a modificar

Si la pantalla solo va a mostrar una lista ese seguimiento no aporta nada

Al usar `AsNoTracking` se consume menos memoria RAM y el servidor trabaja con menos carga

Por eso se puede decir que es solidaridad computacional porque se evita gastar recursos que otros procesos o estudiantes tambien pueden necesitar

## Parte 2 Desafio de refactorizacion de codigo

### 1 Contexto con DbContext

```csharp
using Microsoft.EntityFrameworkCore;

public class UnidadAcademicaContext : DbContext
{
    public UnidadAcademicaContext(DbContextOptions<UnidadAcademicaContext> options)
        : base(options)
    {
    }

    public DbSet<Catedratico> Catedraticos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catedratico>(entidad =>
        {
            entidad.ToTable("Tbl_Catedraticos");

            entidad.HasKey(catedratico => catedratico.Id);

            entidad.Property(catedratico => catedratico.Id)
                .HasColumnName("Id");

            entidad.Property(catedratico => catedratico.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(150)
                .IsRequired();
        });
    }
}

public class Catedratico
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;
}
```

### 2 Consulta LINQ moderna

```csharp
using Microsoft.EntityFrameworkCore;

public class CatedraticoServicio
{
    private readonly UnidadAcademicaContext _context;

    public CatedraticoServicio(UnidadAcademicaContext context)
    {
        _context = context;
    }

    public async Task<List<Catedratico>> ObtenerCatedraticosIngenierosAsync()
    {
        var catedraticos = await _context.Catedraticos
            .AsNoTracking()
            .Where(catedratico => catedratico.Nombre.StartsWith("Ing."))
            .ToListAsync();

        return catedraticos;
    }
}
```

## Explicacion corta del cambio

El codigo viejo con ADO NET abre la conexion crea el comando agrega parametros lee fila por fila y arma cada objeto manualmente

Con EF Core el contexto representa la conexion logica con la base de datos y el `DbSet` representa la tabla de catedraticos

La consulta LINQ expresa la condicion de forma mas clara y EF Core se encarga de traducirla a SQL

Como la lista solo se usa para lectura se agrega `AsNoTracking` para no guardar los objetos en memoria como entidades editables

## Parte 3 Referencias bibliograficas

Facultad de Ingeniería, USAC. (2026). Sesión 17: Conectividad con SQL Server. Acceso Estructurado a Datos mediante C# y ADO.NET. Laboratorio de Introducción a la Programación y Computación 2. Guatemala.

Facultad de Ingeniería, USAC. (2026). Sesión 18: Mapeo de Objetos Relacionales. Persistencia Automatizada con Entity Framework Core. Laboratorio de Introducción a la Programación y Computación 2. Guatemala.
