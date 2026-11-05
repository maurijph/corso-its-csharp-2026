// LIVELLO 1 — CONTROLLER: riceve la richiesta HTTP, la passa al Service
// e trasforma il risultato in una risposta (200, 400, 404).
// Non contiene regole e non tocca mai i dati.
// EN: LAYER 1 — CONTROLLER: receives the HTTP request, passes it to the Service
// EN: and turns the result into a response (200, 400, 404).
// EN: It holds no rules and never touches the data.
public static class PilotaEndpoints
{
    // static: deve esistere un solo Service (e quindi un solo Repository) per tutta
    // la vita dell'API, altrimenti i piloti aggiunti andrebbero persi.
    // EN: static: there must be only one Service (and so only one Repository) for the
    // EN: whole life of the API, otherwise the added drivers would be lost.
    private static PilotaService pilotaService = new PilotaService();

    public static void MapPilotiEndpoints(this IEndpointRouteBuilder app)
    {
        // READ (tutti): GET /piloti
        // EN: READ (all): GET /piloti
        app.MapGet("/piloti", () => pilotaService.GetPiloti());

        // READ (uno): GET /piloti/{id} — 404 se non esiste
        // EN: READ (one): GET /piloti/{id} — 404 if it does not exist
        app.MapGet("/piloti/{id}", (int id) =>
        {
            Pilota? pilota = pilotaService.GetPilota(id);
            if (pilota != null)
                return Results.Ok(pilota);
            else
                return Results.NotFound();
        });

        // Il filtro per scuderia lo fa il Service: qui ci si limita a chiamarlo
        // EN: The Service filters by team: here we just call it
        app.MapGet("/piloti/scuderia/{nome}", (string nome) => pilotaService.GetPilotiDiScuderia(nome));

        // BONUS 1: GET /piloti/ordinati — i piloti ordinati per numero di gara
        // EN: BONUS 1: GET /piloti/ordinati — the drivers sorted by race number
        app.MapGet("/piloti/ordinati", () => pilotaService.GetPilotiOrdinati());

        // CREATE: POST /piloti — il Service risponde true/false, il Controller sceglie il codice HTTP
        // EN: CREATE: POST /piloti — the Service answers true/false, the Controller picks the HTTP code
        app.MapPost("/piloti", (Pilota pilota) =>
        {
            bool result = pilotaService.Create(pilota);
            if (result == true)
                return Results.Ok();
            else
                return Results.BadRequest();
        });

        // UPDATE: PUT /piloti — 404 se l'id non esiste o i dati non vanno bene
        // EN: UPDATE: PUT /piloti — 404 if the id does not exist or the data is not acceptable
        app.MapPut("/piloti", (Pilota pilota) =>
        {
            bool result = pilotaService.Update(pilota);
            if (result == true)
                return Results.Ok();
            else
                return Results.NotFound();
        });

        // DELETE: DELETE /piloti/{id} — 404 se l'id non esiste
        // EN: DELETE: DELETE /piloti/{id} — 404 if the id does not exist
        app.MapDelete("/piloti/{id}", (int id) =>
        {
            bool result = pilotaService.Delete(id);
            if (result == true)
                return Results.Ok();
            else
                return Results.NotFound();
        });
    }
}
