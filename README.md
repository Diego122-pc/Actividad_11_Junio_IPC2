# Actividades Trabajadas en el Laboratorio de IPC2

## Actividad 11 de Junio - API de Estructuras de Datos con ASP.NET Core

### Descripción

Desarrollo de una Web API minimalista en C# utilizando ASP.NET Core que simula la gestión de un catálogo de nodos, demostrando los conceptos fundamentales de estructuras de datos (ABB y AVL) y su exposición a través de servicios web.

### Tecnologías Utilizadas

- C# (.NET 6/7/8)
- ASP.NET Core (Minimal APIs)
- HTTP Client (Postman / Bruno / REST Client)

### Estructura del Proyecto

[Actividad 11_Junio](https://github.com/Diego122-pc/Actividad_11_Junio_IPC2/tree/master/Actividad%2011_Junio)
---


## Actividad 12 de Junio - API de Simulación AVL

### Descripción

Desarrollo de una Web API en C# utilizando ASP.NET Core (Minimal APIs) que simula el comportamiento de rotaciones dobles en Árboles AVL, específicamente la rotación **Izquierda-Derecha (RID)** ante un desbalance tipo "Zig-Zag".

### Tecnologías Utilizadas

- C# (.NET Core)
- ASP.NET Core (Minimal APIs)
- HTTP Client (PowerShell / curl / Postman)

### Estructura del Proyecto

[Actividad 12_Junio](https://github.com/Diego122-pc/Actividad_11_Junio_IPC2/tree/master/Actividad%2012_Junio)
---

# Actividad 17 de Junio - Arquitectura Multi-Nivel y MVC en .NET

## Descripción

Desarrollo de una aplicación web interactiva utilizando ASP.NET Core MVC que emula un Sistema de Control Académico, demostrando los conceptos fundamentales de arquitectura multi-nivel (N-Tier) y el patrón de diseño MVC (Modelo-Vista-Controlador) para lograr desacoplamiento y buenas prácticas de ingeniería de software.

## Tecnologías Utilizadas

- C# (.NET 8)
- ASP.NET Core MVC
- Razor Views
- HTTP Client (Postman / Navegador Web)

[Actividad 17 de Junio](https://github.com/Diego122-pc/Actividad_11_Junio_IPC2/tree/master/Actividad_17_Junio_IPC2)
---

# Actividad 26 de Junio - Integración de Datos y Consumo de APIs

## Descripción
Desarrollo de una Web API en C# utilizando ASP.NET Core que implementa integración de datos mediante consumo de APIs externas y carga masiva de archivos CSV, demostrando los conceptos de serialización, deserialización y procesamiento por lotes para optimizar el rendimiento en sistemas de información.

## Tecnologías Utilizadas
- C# (.NET 6/7/8)
- ASP.NET Core Web API
- Entity Framework Core
- System.Text.Json
- HTTP Client

## Integración de Datos
- **Serialización y Deserialización**: Uso de `JsonSerializer.Serialize()` y `JsonSerializer.Deserialize<T>()` con `System.Text.Json`
- **Consumo de API Externa**: Cliente HTTP para consultar endpoint `https://api.usac.edu/v1/alumnos`
- **Carga Masiva CSV**: Procesamiento de archivos CSV con `StreamReader`
- **Problema N+1**: Solución mediante procesamiento por lotes con `AddRange()` y `SaveChangesAsync()`

## Tabla Comparativa de Formatos

| Formato | Ventajas | Desventajas |
|---------|----------|-------------|
| **CSV** | Simple de leer y escribir, ocupa poco espacio, funciona para cargas masivas | No guarda estructura compleja, no describe tipos de datos, falla si hay comas en valores |
| **XML** | Permite datos estructurados, usa etiquetas descriptivas, útil para validar jerarquías | Más pesado que CSV, ocupa más espacio, lectura lenta en archivos grandes |

## Serialización y Deserialización
- **Serialización**: Convierte un objeto de C# en texto JSON para enviar, guardar o compartir
- **Deserialización**: Convierte texto JSON en un objeto de C# para usar datos en el programa
- **Implementación**: `JsonSerializer.Serialize(objeto)` y `JsonSerializer.Deserialize<Tipo>(json)`

## Problema N+1
- **Problema**: Procesar archivo masivo haciendo consultas/inserciones individuales por cada fila
- **Consecuencia**: Mil filas = mil operaciones separadas, proceso muy lento
- **Solución**: Trabajar por lotes, preparar registros en memoria y guardar juntos con `AddRange()` y `SaveChangesAsync()`

[Actividad 26 Junio](https://github.com/Diego122-pc/Actividad_11_Junio_IPC2/blob/master/Actividad_26_Junio/Integracion_Datos.md)
---


