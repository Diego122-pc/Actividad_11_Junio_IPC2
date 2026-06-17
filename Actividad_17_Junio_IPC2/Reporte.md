# Actividad de Laboratorio: Arquitectura Multi-Nivel y MVC en .NET

## Parte 1: Fundamentación teórica

### El tránsito hacia sistemas distribuidos y multi-capa

Un monolito local concentra interfaz, lógica de aplicación y almacenamiento en una sola máquina física. Esa decisión puede funcionar en ejercicios pequeños, pero crea problemas cuando varios usuarios necesitan consultar o modificar los mismos datos. La sincronización se vuelve manual o frágil, la disponibilidad depende de un único equipo y el crecimiento queda limitado por los recursos de esa máquina. Si el almacenamiento no está centralizado o expuesto mediante servicios controlados, cada copia local puede terminar con información distinta.

La diferencia entre *layers* y *tiers* es una distinción clave. Las capas lógicas (*layers*) son una forma de organizar responsabilidades dentro del código, por ejemplo presentación, dominio y acceso a datos. Los niveles físicos (*tiers*) indican dónde se ejecutan esas responsabilidades: navegador o cliente, servidor de aplicación y servidor de base de datos. Una aplicación puede tener varias capas lógicas dentro de un mismo nivel físico, o distribuirlas en varios servidores.

En una arquitectura de 3 niveles, el nivel de presentación se encarga de la interacción con el usuario. Su tecnología común puede ser HTML, CSS, JavaScript o vistas generadas por ASP.NET Core MVC. El nivel de aplicación o negocio procesa las peticiones, aplica validaciones, coordina casos de uso y expone controladores o servicios. En .NET suele implementarse con C# y ASP.NET Core. El nivel de datos almacena y recupera información persistente, usualmente mediante un motor como SQL Server, PostgreSQL o MySQL. En esta práctica se simula con almacenamiento en memoria para evitar una base de datos real.

Exponer públicamente el puerto de una base de datos a internet es un error crítico porque aumenta la superficie de ataque: cualquier actor externo podría intentar fuerza bruta, explotación de vulnerabilidades, enumeración de servicios o extracción directa de datos. La buena práctica es ubicar la base de datos en una red privada, permitir acceso solo desde el nivel de aplicación y controlar la entrada pública mediante HTTPS, firewall, autenticación y reglas de mínimo privilegio.

### Desacoplamiento lógico con MVC

El código espagueti aparece cuando SQL, cálculos de negocio y etiquetas visuales se mezclan en el mismo archivo. Esa mezcla dificulta el mantenimiento porque un cambio visual puede romper una consulta, una validación puede depender de detalles de HTML y las pruebas unitarias se vuelven costosas. También reduce la claridad del sistema: no hay un lugar único para entender cada responsabilidad.

El patrón MVC separa preocupaciones. El Modelo representa datos y reglas del dominio; no debe conocer cómo se muestran los datos porque su responsabilidad no es visual. La Vista presenta información al usuario y debe mantenerse pasiva: no debe contener consultas SQL ni lógica de negocio. El Controlador actúa como intermediario táctico: recibe la petición HTTP, valida datos de entrada mínimos, selecciona el modelo adecuado y devuelve una vista o una respuesta HTTP.

MVC ayuda a lograr alta cohesión porque cada componente agrupa responsabilidades relacionadas. El modelo modela datos, la vista presenta y el controlador coordina. También reduce el acoplamiento porque la vista no necesita saber cómo se guardan los datos y el modelo no depende del HTML. En un entorno profesional, esta separación facilita pruebas, revisión de código, mantenimiento y evolución del sistema.

## Parte 2: Mapeo de enrutamiento

La plantilla convencional usada por ASP.NET Core MVC es:

```csharp
{controller=Home}/{action=Index}/{id?}
```

| URL entrante del cliente | Clase controladora buscada por framework | Método accion ejecutado | Parámetro inyectado |
| --- | --- | --- | --- |
| `https://ingenieria.usac.edu.gt/ControlAcademico/Login` | `ControlAcademicoController` | `Login` | Ninguno |
| `https://ingenieria.usac.edu.gt/Estudiante/Historial/20260123` | `EstudianteController` | `Historial` | `id = 20260123` |
| `https://ingenieria.usac.edu.gt/Asignacion/Detalle/10` | `AsignacionController` | `Detalle` | `id = 10` |
| `https://ingenieria.usac.edu.gt/Home` | `HomeController` | `Index` | Ninguno |

## Parte 3: Flujo de petición

1. El usuario hace clic en un botón o enlace del navegador. El navegador construye una petición HTTP con la URL, el método, los encabezados y, si aplica, el cuerpo enviado.
2. La petición llega al servidor ASP.NET Core. El middleware procesa la solicitud y el sistema de enrutamiento compara la URL con la plantilla `{controller=Home}/{action=Index}/{id?}`.
3. El framework identifica el controlador y la acción. Si la ruta es `/Estudiante/Listar`, instancia o reutiliza el flujo hacia `EstudianteController` y ejecuta la acción `Listar`.
4. El controlador coordina la respuesta. Obtiene los datos del modelo o de la fuente simulada en memoria, valida datos de entrada cuando corresponde y selecciona una vista o un resultado HTTP.
5. La vista Razor recibe el modelo tipado, genera HTML dinámico y ASP.NET Core envía la respuesta al navegador. El navegador interpreta el HTML, CSS y JavaScript para mostrar la pantalla final al usuario.


## Parte 4: Auditoría y Control de Calidad

### Prueba de Cohesión (GET)
Accede a la acción de listado (`/Estudiante/Listar`). La respuesta es limpia y el controlador se limitó a despachar la información sin calcular variables internas o mezclar sentencias SQL en texto plano.

### Evaluación de Antipatrones
Revisa el archivo `EstudianteController.cs`. Los métodos no exceden las 20 líneas de código, cumpliendo con la regla de controladores delgados y evitando el antipatrón de Controladores Gordos (Fat Controllers).

![Imagen](https://github.com/Diego122-pc/Actividad_11_Junio_IPC2/blob/master/Actividad_17_Junio_IPC2/Anexos/Captura%20de%20pantalla%202026-06-17%20174408.png)

![imagen2](https://github.com/Diego122-pc/Actividad_11_Junio_IPC2/blob/master/Actividad_17_Junio_IPC2/Anexos/Captura%20de%20pantalla%202026-06-17%20174413.png)

## Parte 5: Referencias bibliográficas

> Facultad de Ingeniería, USAC. (2026). Sesión 11: Modelado Base y Arquitecturas de Despliegue. Evolución de Sistemas Distribuidos, Fundamentos del Modelo Cliente-Servidor y Diseño Físico Multi-Capas (N-Tier). Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.

> Facultad de Ingeniería, USAC. (2026). Sesión 12: Arquitectura y Componentes del Patrón MVC. Desacoplamiento Lógico de Software, Ciclo de Vida de las Peticiones y Enrutamiento en Aplicaciones Interactivas Modernas. Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.


