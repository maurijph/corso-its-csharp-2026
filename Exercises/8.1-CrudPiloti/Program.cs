// =====================================================================
// ESERCITAZIONE 8.1: API CRUD di piloti — SOLUZIONE (con i bonus)
// Corso C# e .NET 8
// EN: EXERCISE 8.1: CRUD API of drivers — SOLUTION (with the bonuses)
// EN: C# and .NET 8 course
// =====================================================================

var builder = WebApplication.CreateBuilder(args);

// Servizi per Swagger, come nell'esercitazione 8
// EN: Services for Swagger, as in exercise 8
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // http://localhost:5281/swagger
}

// Il nostro "database" in memoria: l'id è la chiave, il pilota il valore
// EN: Our in-memory "database": the id is the key, the driver is the value
Dictionary<int, Pilota> piloti = new Dictionary<int, Pilota>();
piloti.Add(1, new Pilota { Id = 1, Nome = "Charles Leclerc", Scuderia = "Ferrari", Numero = 16 });
piloti.Add(2, new Pilota { Id = 2, Nome = "Lando Norris", Scuderia = "McLaren", Numero = 4 });

// READ (tutti): GET /piloti
// EN: READ (all): GET /piloti
app.MapGet("/piloti", () =>
{
    return piloti.Values.ToArray();
});

// READ (uno): GET /piloti/{id} — 404 se non esiste
// EN: READ (one): GET /piloti/{id} — 404 if it does not exist
app.MapGet("/piloti/{id}", (int id) =>
{
    if (piloti.ContainsKey(id))
        return Results.Ok(piloti[id]);
    else
        return Results.NotFound();
});

// CREATE: POST /piloti — 400 se l'id (o, bonus, il numero) è già usato
// EN: CREATE: POST /piloti — 400 if the id (or, bonus, the number) is already used
app.MapPost("/piloti", (Pilota pilota) =>
{
    if (piloti.ContainsKey(pilota.Id))
        return Results.BadRequest("Id già presente");

    // BONUS 2: due piloti non possono avere lo stesso numero di gara
    // EN: BONUS 2: two drivers cannot have the same race number
    if (piloti.Values.Any(p => p.Numero == pilota.Numero))
        return Results.BadRequest("Numero già usato da un altro pilota");

    piloti.Add(pilota.Id, pilota);
    return Results.Ok();
});

// UPDATE: PUT /piloti — 404 se l'id non esiste
// EN: UPDATE: PUT /piloti — 404 if the id does not exist
app.MapPut("/piloti", (Pilota pilota) =>
{
    if (!piloti.ContainsKey(pilota.Id))
        return Results.NotFound();

    piloti[pilota.Id] = pilota;
    return Results.Ok();
});

// DELETE: DELETE /piloti/{id} — 404 se l'id non esiste
// EN: DELETE: DELETE /piloti/{id} — 404 if the id does not exist
app.MapDelete("/piloti/{id}", (int id) =>
{
    if (!piloti.ContainsKey(id))
        return Results.NotFound();

    piloti.Remove(id);
    return Results.Ok();
});

// BONUS 1: GET /piloti/scuderia/{nome} — i piloti di una scuderia.
// StringComparison.OrdinalIgnoreCase: "ferrari" e "Ferrari" sono la stessa cosa.
// EN: BONUS 1: GET /piloti/scuderia/{nome} — the drivers of a team.
// EN: StringComparison.OrdinalIgnoreCase: "ferrari" and "Ferrari" are the same thing.
app.MapGet("/piloti/scuderia/{nome}", (string nome) =>
{
    return piloti.Values
        .Where(p => string.Equals(p.Scuderia, nome, StringComparison.OrdinalIgnoreCase)) // controllare firma del metodo
        .ToArray();
});

app.Run();
