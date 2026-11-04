// =====================================================================
// ESERCITAZIONE 9: API CRUD di persone con il modello CSR
// EN: EXERCISE 9: CRUD API of people with the CSR pattern
//
// Stessa API dell'esercitazione 8, ma il codice è diviso in tre livelli:
//   Controller (Endpoints)    -> gestisce HTTP
//   Service    (Services)     -> contiene le regole
//   Repository (Repositories) -> legge e scrive i dati
// Ogni livello parla solo con quello subito sotto:
//   client -> Controller -> Service -> Repository -> dati
// EN: Same API as exercise 8, but the code is split into three layers:
// EN:   Controller (Endpoints)    -> handles HTTP
// EN:   Service    (Services)     -> holds the rules
// EN:   Repository (Repositories) -> reads and writes the data
// EN: Each layer only talks to the one right below it:
// EN:   client -> Controller -> Service -> Repository -> data
// =====================================================================

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Map.Post, Map.Put, Map.Delete non sono più in Program.cs, ma sono gestiti all'interno di MapAPIEndpoints (nel livello Controller)
// EN: Map.Post, Map.Put, Map.Delete are handled inside MapAPIEndpoints (in the Controller layer)

// Mappatura degli endpoint API, è il livello Controller che gestisce le richieste HTTP
// EN: Mapping of API endpoints, it is the Controller layer that handles HTTP requests
app.MapAPIEndpoints(); // Fare 'go to definition' per vedere dove sono definiti gli endpoint

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
