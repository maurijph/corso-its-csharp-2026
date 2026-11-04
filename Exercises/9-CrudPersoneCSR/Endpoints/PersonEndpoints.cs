// LIVELLO 1 — CONTROLLER: riceve la richiesta HTTP, la passa al Service
// e trasforma il risultato in una risposta (200, 400, 404).
// Non contiene regole e non tocca mai i dati.
// EN: LAYER 1 — CONTROLLER: receives the HTTP request, passes it to the Service
// EN: and turns the result into a response (200, 400, 404).
// EN: It holds no rules and never touches the data.
public static class PersonEndpoints
{
    // Perchè è statico? Perché gli endpoint sono definiti come metodi di estensione statici (MapAPIEndpoints) e non hanno stato.
    // E' condivisa tra tutti gli endpoint altrimenti i dati andrebbero persi ad ogni richiesta.
    // EN: Why is it static? Because the endpoints are defined as static extension methods and have no state.
    // EN: It is shared among all endpoints, otherwise the data would be lost on each request.
    private static PersonService personService = new PersonService();

    public static void MapAPIEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/getPeople", () => personService.GetPeople());
        app.MapGet("/getPerson/{id}", (int id) => personService.GetPerson(id));

        // Il filtro sui maggiorenni lo fa il Service: qui ci si limita a chiamarlo
        // EN: The Service filters the adults: here we just call it
        app.MapGet("/getAdults", () => personService.GetAdults());

        // Il Service risponde true/false: è il Controller a scegliere il codice HTTP
        // EN: The Service answers true/false: the Controller picks the HTTP code
        app.MapPost("/create", (Person person) =>
        {
            // Controller si limita a decidere lo status HTTP da restituire
            // Non decide il risultato dell'operazione: lo fa il Service
            // EN: Controller only decides the HTTP status to return
            // EN:It does not decide the outcome of the operation: the Service does
            bool result = personService.Create(person);
            if (result == true)
                return Results.Ok();
            else
                return Results.BadRequest();
        });
        app.MapPut("/update", (Person person) =>
        {
            bool result = personService.Update(person);
            if (result == true)
                return Results.Ok();
            else
                return Results.NotFound();
        });
        app.MapDelete("/delete/{id}", (int id) =>
        {
            bool result = personService.Delete(id);
            if (result == true)
                return Results.Ok();
            else
                return Results.NotFound();
        });
    }
}
