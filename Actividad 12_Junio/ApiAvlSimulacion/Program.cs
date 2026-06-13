using ApiAvlSimulacion;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var estadoArbol = new List<NodoAVL>
{
    new() { Id = 30, Etiqueta = "Nodo raiz (abuelo) - FE: -2", Altura = 3 },
    new() { Id = 10, Etiqueta = "Hijo izquierdo - FE: +1", Altura = 2 }
};

app.MapGet("/api/arbol", () =>
{
    return Results.Ok(estadoArbol);
});

app.MapPost("/api/arbol/insertar", (NodoAVL nuevoNodo) =>
{
    if (nuevoNodo.Id <= 0)
    {
        return Results.BadRequest("El ID del nodo debe ser mayor que cero.");
    }

    if (nuevoNodo.Id == 20)
    {
        estadoArbol.Clear();
        estadoArbol.Add(new NodoAVL { Id = 20, Etiqueta = "Nueva raiz balanceada por RID - FE: 0", Altura = 2 });
        estadoArbol.Add(new NodoAVL { Id = 10, Etiqueta = "Hijo izquierdo - FE: 0", Altura = 1 });
        estadoArbol.Add(new NodoAVL { Id = 30, Etiqueta = "Hijo derecho - FE: 0", Altura = 1 });

        return Results.Created("/api/arbol", new
        {
            Mensaje = "Rotacion Izquierda-Derecha ejecutada. El arbol queda balanceado.",
            Estructura = estadoArbol
        });
    }

    estadoArbol.Add(nuevoNodo);
    return Results.Created($"/api/arbol/{nuevoNodo.Id}", nuevoNodo);
});

app.Run();
