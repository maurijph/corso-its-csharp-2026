// LIVELLO 1 — CONTROLLER: riceve la richiesta HTTP, la passa al Service
// e trasforma il risultato in una risposta (200, 400, 404).
// Non contiene regole e non tocca mai i dati.
// EN: LAYER 1 — CONTROLLER: receives the HTTP request, passes it to the Service
// EN: and turns the result into a response (200, 400, 404).
// EN: It holds no rules and never touches the data.
public static class PersonEndpoints
{
    private static PersonService personService = new PersonService();

    public static void MapAPIEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/getPeople", () => personService.GetPeople());
        app.MapGet("/getPerson/{id}", (int id) => personService.GetPerson(id));

        // Il Service risponde true/false: è il Controller a scegliere il codice HTTP
        // EN: The Service answers true/false: the Controller picks the HTTP code
        app.MapPost("/create", (Person person) =>
        {
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
