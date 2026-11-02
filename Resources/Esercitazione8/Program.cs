// =====================================================================
// ESERCITAZIONE 8: API CRUD di persone con le Minimal API
// Corso C# e .NET 8
// EN: EXERCISE 8: CRUD API of people with Minimal APIs
// EN: C# and .NET 8 course
//
// CRUD = Create, Read, Update, Delete: le quattro operazioni base sui dati.
// Ognuna corrisponde a un verbo HTTP:
//   Create -> POST    Read -> GET    Update -> PUT    Delete -> DELETE
// I dati vivono in un dizionario in memoria: quando l'applicazione si
// ferma, tutto quello che è stato inserito va perso.
// EN: CRUD = Create, Read, Update, Delete: the four basic operations on data.
// EN: Each one matches an HTTP verb:
// EN:   Create -> POST    Read -> GET    Update -> PUT    Delete -> DELETE
// EN: The data lives in an in-memory dictionary: when the application
// EN: stops, everything that was inserted is lost.
// =====================================================================

// Il builder prepara l'applicazione: configurazione (appsettings.json),
// log e "servizi" da registrare prima di avviarla.
// EN: The builder prepares the application: configuration (appsettings.json),
// EN: logging and "services" to register before starting it.
var builder = WebApplication.CreateBuilder(args);

// Servizi per Swagger: il primo elenca gli endpoint, il secondo ne genera
// la descrizione OpenAPI (vedi l'esercitazione 7 per i dettagli).
// EN: Services for Swagger: the first lists the endpoints, the second generates
// EN: their OpenAPI description (see exercise 7 for details).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build: da qui in poi la configurazione è chiusa e abbiamo l'applicazione vera.
// EN: Build: from here on the configuration is closed and we have the real application.
var app = builder.Build();

// La pagina di Swagger serve solo mentre sviluppiamo
// EN: The Swagger page is only needed while developing
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // http://localhost:<porta>/swagger — EN: http://localhost:<port>/swagger
}

app.UseHttpsRedirection();

// Il nostro "database": un dizionario che associa l'id alla persona.
// Viene creato una sola volta all'avvio e le lambda qui sotto lo "catturano",
// quindi tutte le richieste lavorano sullo stesso oggetto.
// Attenzione: Dictionary non è pensato per essere usato da più richieste
// contemporaneamente; per una demo va bene, in un'app reale si usa un database.
// EN: Our "database": a dictionary mapping the id to the person.
// EN: It is created only once at startup and the lambdas below "capture" it,
// EN: so all requests work on the same object.
// EN: Watch out: Dictionary is not designed to be used by several requests
// EN: at the same time; for a demo it is fine, a real app uses a database.
Dictionary<int, Person> dict = new Dictionary<int, Person>();

// ---------------------------------------------------------------------
// READ (tutte): GET /getPeople
// Nessun parametro. Restituisce un array, che diventa un JSON [ {...}, {...} ].
// Se il dizionario è vuoto restituisce [] (array vuoto), non un errore.
// EN: READ (all): GET /getPeople
// EN: No parameters. Returns an array, which becomes a JSON [ {...}, {...} ].
// EN: If the dictionary is empty it returns [] (empty array), not an error.
// ---------------------------------------------------------------------
app.MapGet("/getPeople", () =>
{
    if (dict.Count > 0)
        return dict.Values.ToArray();
    else
        return [];
});

// ---------------------------------------------------------------------
// READ (una): GET /getPerson/{id}
// {id} nella rotta è un segnaposto: chiamando /getPerson/3 il valore 3 finisce
// nel parametro "int id" della lambda, perché ha lo stesso nome (route binding).
// Se scrivo /getPerson/abc la conversione in int fallisce e ASP.NET Core
// risponde da solo 400 Bad Request, senza nemmeno eseguire la lambda.
// Se la persona non c'è restituiamo null: la risposta è comunque 200 OK
// con corpo "null". Sarebbe più corretto rispondere 404 con Results.NotFound(),
// come fanno PUT e DELETE.
// EN: READ (one): GET /getPerson/{id}
// EN: {id} in the route is a placeholder: calling /getPerson/3 the value 3 ends up
// EN: in the lambda's "int id" parameter, because it has the same name (route binding).
// EN: If I write /getPerson/abc the conversion to int fails and ASP.NET Core
// EN: answers 400 Bad Request by itself, without even running the lambda.
// EN: If the person is not there we return null: the response is still 200 OK
// EN: with body "null". It would be more correct to answer 404 with
// EN: Results.NotFound(), like PUT and DELETE do.
// ---------------------------------------------------------------------
app.MapGet("/getPerson/{id}", (int id) =>
{
    if (dict.Count > 0 && dict.ContainsKey(id))
        return dict[id];
    else
        return null;
});

// ---------------------------------------------------------------------
// CREATE: POST /create
// Il parametro Person non sta nella rotta: ASP.NET Core lo legge dal corpo
// (body) della richiesta, che deve essere un JSON (body binding).
// Results.* costruisce la risposta HTTP con il codice di stato giusto:
//   Results.Ok()         -> 200 OK           tutto bene
//   Results.BadRequest() -> 400 Bad Request  la richiesta non va bene
// Qui l'id già presente è un errore del client, quindi 400.
// EN: CREATE: POST /create
// EN: The Person parameter is not in the route: ASP.NET Core reads it from the
// EN: request body, which must be a JSON (body binding).
// EN: Results.* builds the HTTP response with the right status code:
// EN:   Results.Ok()         -> 200 OK           all good
// EN:   Results.BadRequest() -> 400 Bad Request  the request is not acceptable
// EN: Here an id that already exists is a client error, hence 400.
// ---------------------------------------------------------------------
app.MapPost("/create", (Person person) =>
{
    if (dict.Count > 0 && dict.ContainsKey(person.id))
        return Results.BadRequest();
    else
    {
        dict.Add(person.id, person);
        return Results.Ok();
    }
});

// ---------------------------------------------------------------------
// UPDATE: PUT /update
// Anche qui la persona arriva nel body. PUT sostituisce l'intera risorsa:
// la persona salvata viene rimpiazzata da quella ricevuta.
// Se l'id non esiste rispondiamo 404 Not Found (Results.NotFound()).
// La condizione si potrebbe scrivere più semplicemente: !dict.ContainsKey(person.id)
// è già vero anche quando il dizionario è vuoto.
// EN: UPDATE: PUT /update
// EN: Here too the person comes in the body. PUT replaces the whole resource:
// EN: the stored person is replaced by the one received.
// EN: If the id does not exist we answer 404 Not Found (Results.NotFound()).
// EN: The condition could be written more simply: !dict.ContainsKey(person.id)
// EN: is already true when the dictionary is empty.
// ---------------------------------------------------------------------
app.MapPut("/update", (Person person) =>
{
    if ((dict.Count > 0 && !dict.ContainsKey(person.id)) || dict.Count == 0)
        return Results.NotFound();
    else
    {
        dict[person.id] = person;
        return Results.Ok();
    }
});

// ---------------------------------------------------------------------
// DELETE: DELETE /delete/{id}
// Come la GET singola, l'id arriva dalla rotta. Se non esiste -> 404.
// EN: DELETE: DELETE /delete/{id}
// EN: Like the single GET, the id comes from the route. If it does not exist -> 404.
// ---------------------------------------------------------------------
app.MapDelete("/delete/{id}", (int id) =>
{
    if ((dict.Count > 0 && !dict.ContainsKey(id)) || dict.Count == 0)
        return Results.NotFound();
    else
    {
        dict.Remove(id);
        return Results.Ok();
    }
});

// Avvia il server web e resta in ascolto delle richieste finché non lo fermiamo
// EN: Starts the web server and keeps listening for requests until we stop it
app.Run();
