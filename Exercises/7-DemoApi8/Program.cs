var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// AddEndpointsApiExplorer: accende l'"esploratore" degli endpoint.
// È lui che gira per l'applicazione, trova tutti gli endpoint scritti con app.MapGet/MapPost/...
// e ne fa un elenco (rotta, verbo HTTP, parametri, tipo restituito).
// Senza questa riga nessuno costruisce quell'elenco e Swagger risulterebbe vuoto:
// serve solo per le Minimal API, i Controller vengono già esplorati da soli.
// EN: AddEndpointsApiExplorer turns on the endpoint "explorer".
// EN: It walks through the application, finds every endpoint written with
// EN: app.MapGet/MapPost/... and builds a list of them (route, HTTP verb,
// EN: parameters, return type).
// EN: Without this line nobody builds that list and Swagger would come up empty:
// EN: it is only needed for Minimal APIs, Controllers are already explored on their own.
builder.Services.AddEndpointsApiExplorer();

// AddSwaggerGen: prende l'elenco qui sopra e lo trasforma nel documento OpenAPI,
// cioè il file JSON che descrive l'API (è quello che la pagina di Swagger legge e disegna).
// EN: AddSwaggerGen takes the list above and turns it into the OpenAPI document,
// EN: that is the JSON file describing the API (the one the Swagger page reads and draws).
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // UseSwagger: pubblica il documento OpenAPI come pagina web, all'indirizzo
    // /swagger/v1/swagger.json. È solo testo JSON: utile ai programmi, illeggibile per noi.
    // EN: UseSwagger publishes the OpenAPI document as a web page, at
    // EN: /swagger/v1/swagger.json. It is only JSON text: useful to programs,
    // EN: unreadable to us.
    app.UseSwagger();

    // UseSwaggerUI: aggiunge la pagina grafica che legge quel JSON e lo mostra come elenco
    // di endpoint, con il pulsante "Try it out" per provarli dal browser.
    // Le due righe insieme vanno dentro l'if: in produzione di solito non si espone.
    // EN: UseSwaggerUI adds the graphical page that reads that JSON and shows it as a
    // EN: list of endpoints, with the "Try it out" button to try them from the browser.
    // EN: Both lines go inside the if: in production this is usually not exposed.
    app.UseSwaggerUI(); //http://localhost:5280/swagger/index.html
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
// WithName: dà un nome all'endpoint ("GetWeatherForecast"), un'etichetta interna
// all'applicazione che non c'entra con l'indirizzo /weatherforecast.
// Serve per due cose:
//  1) farsi costruire l'URL dal nome, senza scrivere la rotta a mano, per esempio con
//     Results.CreatedAtRoute("GetWeatherForecast", ...) oppure con LinkGenerator;
//  2) finisce nel documento OpenAPI come "operationId", ed è il nome del metodo che
//     generatori come NSwag useranno nel client C# o TypeScript.
// Il nome deve essere unico in tutta l'applicazione.
//
// Dietro le quinte WithName non fa altro che attaccare all'endpoint un oggetto che
// implementa IEndpointNameMetadata: i "metadata" sono bigliettini appesi all'endpoint,
// che ASP.NET Core legge quando gli servono. IEndpointNameMetadata è il bigliettino
// che contiene il nome; non lo scriviamo quasi mai a mano, usiamo WithName.
// EN: WithName gives the endpoint a name ("GetWeatherForecast"), an internal label
// EN: of the application that has nothing to do with the /weatherforecast address.
// EN: It is useful for two things:
// EN:  1) having the URL built from the name, without writing the route by hand, for
// EN:     example with Results.CreatedAtRoute("GetWeatherForecast", ...) or LinkGenerator;
// EN:  2) it ends up in the OpenAPI document as the "operationId", and it is the method
// EN:     name that generators such as NSwag will use in the C# or TypeScript client.
// EN: The name must be unique across the whole application.
// EN:
// EN: Behind the scenes WithName does nothing more than attach to the endpoint an object
// EN: implementing IEndpointNameMetadata: "metadata" are little notes pinned to the
// EN: endpoint, that ASP.NET Core reads when it needs them. IEndpointNameMetadata is the
// EN: note holding the name; we hardly ever write it by hand, we use WithName.
.WithName("GetWeatherForecast")

// WithOpenApi: dice "di questo endpoint voglio la descrizione completa in Swagger".
// Analizza la firma della lambda e aggiunge al documento OpenAPI i dettagli
// (parametri, tipo restituito, codici di risposta). Permette anche di ritoccarli:
//   .WithOpenApi(op => { op.Summary = "Previsioni dei prossimi 5 giorni"; return op; })
// Arriva dal pacchetto Microsoft.AspNetCore.OpenApi, non da Swashbuckle.
// EN: WithOpenApi says "for this endpoint I want the full description in Swagger".
// EN: It analyses the lambda signature and adds the details to the OpenAPI document
// EN: (parameters, return type, response codes). It also lets you tweak them:
// EN:   .WithOpenApi(op => { op.Summary = "Forecast for the next 5 days"; return op; })
// EN: It comes from the Microsoft.AspNetCore.OpenApi package, not from Swashbuckle.
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
