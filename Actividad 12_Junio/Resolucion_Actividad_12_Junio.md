# Actividad 12 de Junio - AVL y Web APIs

## Parte 1: Investigacion teorica

### 1. Limite de las rotaciones simples y desbalanceo en Zig-Zag

En un arbol AVL, una rotacion simple funciona bien cuando el desbalance va en la misma direccion dos veces. Por ejemplo, si un nodo se carga hacia la izquierda y su hijo tambien se carga hacia la izquierda, una rotacion simple a la derecha corrige el problema.

El caso cambia con una secuencia cruzada como `30, 10, 20`. Primero entra `30`, luego `10` queda como hijo izquierdo, y al insertar `20` el nuevo nodo queda como hijo derecho de `10`. La forma ya no es una linea, sino un Zig-Zag. Si se intenta una sola rotacion sobre `30`, el peso solo se traslada al otro lado y el arbol no queda correctamente ordenado ni balanceado.

La Rotacion Izquierda-Derecha (RID) se activa cuando el padre esta cargado hacia la izquierda y su hijo izquierdo esta cargado hacia la derecha:

```text
FE(padre) = -2
FE(hijo izquierdo) = +1
```

Con la convencion usada en clase, el factor negativo indica peso en el lado izquierdo y el positivo indica peso en el lado derecho. Por eso el caso `30, 10, 20` necesita primero una rotacion izquierda sobre el hijo `10` y luego una rotacion derecha sobre el padre `30`.

Aplicar el principio DRY ayuda bastante aqui. RID y RDI no deberian reescribir todos los cambios de punteros desde cero, porque eso duplica logica y aumenta el riesgo de errores. Es mas limpio construirlas reutilizando las rotaciones simples: RID se puede resolver como rotacion izquierda sobre el hijo y rotacion derecha sobre el padre. Asi el codigo queda mas corto, mas facil de probar y mas facil de mantener.

### 2. Fundamentos de arquitectura web y protocolo HTTP

En el modelo cliente-servidor, el cliente puede ser un navegador, Postman, Thunder Client o cualquier programa que envie solicitudes HTTP. El servidor recibe esa solicitud, ejecuta una accion y devuelve una respuesta. En una request viajan datos como el metodo HTTP, la ruta, encabezados y, cuando aplica, un cuerpo JSON. En una response viajan el codigo de estado, encabezados y normalmente un cuerpo con los datos solicitados o el resultado de la operacion.

`GET` se usa para consultar informacion sin modificar el estado del servidor. En esta actividad corresponde al endpoint que devuelve la estructura actual del arbol.

`POST` se usa para enviar datos al servidor y provocar una accion, como insertar un nodo. En esta actividad corresponde al endpoint que recibe el nodo `20` y simula la rotacion doble RID.

## Parte 2: Implementacion practica

El proyecto creado se llama `ApiAvlSimulacion` y contiene una Minimal API en C#.

### Modelo usado

```csharp
public class NodoAVL
{
    public int Id { get; set; }
    public string Etiqueta { get; set; } = string.Empty;
    public int Altura { get; set; } = 1;
}
```

### Endpoints

`GET /api/arbol`

Devuelve el estado actual del arbol en memoria. Al iniciar la API, el estado representa el desbalance Zig-Zag:

```json
[
  {
    "id": 30,
    "etiqueta": "Nodo raiz (abuelo) - FE: -2",
    "altura": 3
  },
  {
    "id": 10,
    "etiqueta": "Hijo izquierdo - FE: +1",
    "altura": 2
  }
]
```

`POST /api/arbol/insertar`

Recibe un nodo nuevo. Si el nodo tiene `id` igual a `20`, la API simula el caso cruzado Izquierda-Derecha y reorganiza la lista en memoria para que `20` quede como nueva raiz balanceada.

Cuerpo de prueba:

```json
{
  "id": 20,
  "etiqueta": "Nieto derecho"
}
```

Respuesta esperada: `201 Created`, con el nuevo orden:

```text
20 como raiz
10 como hijo izquierdo
30 como hijo derecho
```

## Pruebas sugeridas

Ejecutar la API:

```bash
cd ApiAvlSimulacion
dotnet run
```

Consultar el estado inicial:

```bash
curl http://localhost:5170/api/arbol
```

Insertar el nodo que dispara la RID:

```bash
curl -X POST http://localhost:5170/api/arbol/insertar ^
  -H "Content-Type: application/json" ^
  -d "{\"id\":20,\"etiqueta\":\"Nieto derecho\"}"
```
