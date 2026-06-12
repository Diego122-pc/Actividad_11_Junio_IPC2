# Actividad_11_Junio_IPC2# Actividad de Investigación y Práctica: Estructuras de Datos Avanzadas y APIs con ASP.NET Core

**Nombre: Diego Enrique Flores Ruiz**
##
**Carnet: 202503934**
##
**Fecha: 11 junio ipc2**

---

## Parte 1: Investigación Teórica

### 1. Árboles Binarios de Búsqueda (ABB)

**Regla de ordenamiento:**
En un Árbol Binario de Búsqueda, para cada nodo:
- Todos los nodos del **subárbol izquierdo** tienen valores **menores** que el nodo raíz
- Todos los nodos del **subárbol derecho** tienen valores **mayores** que el nodo raíz

**Principal desventaja (degeneración en lista vinculada):**
Cuando los datos se insertan en orden secuencial (por ejemplo: 1, 2, 3, 4, 5...), el árbol pierde su estructura balanceada y se convierte en una lista enlazada. Esto hace que:
- La altura del árbol sea O(n) en lugar de O(log n)
- Las operaciones de búsqueda, inserción y eliminación pasen de O(log n) a O(n)
- El árbol deja de ser eficiente, comportándose como una lista simplemente enlazada

### 2. Árboles AVL

**¿Qué es un árbol auto-balanceado?**
Es un árbol que automáticamente mantiene su altura balanceada después de cada operación de inserción o eliminación, utilizando rotaciones para corregir desequilibrios.

**Factor de balanceo:**

- Si el factor es **-1, 0 o 1**: el árbol está balanceado
- Si el factor es **menor a -1 o mayor a 1**: el árbol está desbalanceado y requiere rotación

**Complejidad O(log n):**
La complejidad se mantiene en O(log n) para búsqueda, inserción y eliminación porque la altura del árbol siempre se mantiene en aproximadamente log₂(n), gracias al balanceo automático que evita la degeneración en lista.

### 3. Fundamentos de Web APIs

**¿Qué es una API y cómo funciona el modelo Cliente-Servidor?**

**API (Application Programming Interface):** Es un conjunto de reglas y protocolos que permite que diferentes aplicaciones se comuniquen entre sí.

**Modelo Cliente-Servidor:**
- **Cliente:** Solicita recursos o servicios (ej: navegador web, aplicación móvil)
- **Servidor:** Provee los recursos o servicios solicitados

**Viaje de una petición HTTP:**
1. **Request (Petición):** El cliente envía un mensaje HTTP que incluye:
   - Método (GET, POST, etc.)
   - URL del recurso
   - Headers (información adicional como tipo de contenido)
   - Body (cuerpo con datos, opcional)
2. **Response (Respuesta):** El servidor procesa y devuelve:
   - Código de estado (200 OK, 201 Created, 404 Not Found, etc.)
   - Headers
   - Body (datos solicitados o confirmación)

### 4. Verbos HTTP

| Característica | GET | POST |
|----------------|-----|------|
| **Uso correcto** | Recuperar/leer recursos existentes | Crear nuevos recursos en el servidor |
| **Datos** | En la URL (query string) | En el Body de la petición |
| **Idempotencia** | Sí (varias solicitudes idénticas producen el mismo resultado) |  No (varias solicitudes idénticas crean múltiples recursos) |
| **Seguridad** | No modifica datos | Modifica/crea datos |
| **Ejemplo** | `GET /api/nodos` | `POST /api/nodos` con JSON en el body |

---

## Parte 2: Implementación Práctica

### Código fuente

#### Clase NodoElemento.cs
```csharp
namespace ApiEstructurasDemo;

public class NodoElemento
{
    public int Id { get; set; }
    public string Valor { get; set; } = string.Empty;
}

```

### Instrucciones de ejecución
```bash

dotnet new webapi -o ApiEstructurasDemo
cd ApiEstructurasDemo
dotnet run

```

### Peticion
``` bash
Invoke-RestMethod -Uri "http://localhost:5259/api/nodos"

```
```bash
POST (crear nuevo nodo)
Invoke-RestMethod -Uri "http://localhost:5259/api/nodos" -Method POST -Body '{"id":15,"valor":"Nuevo Nodo Derecho"}' -ContentType "application/json"

```
### GET final (verificación)
``` bash
Invoke-RestMethod -Uri "http://localhost:5259/api/nodos"

