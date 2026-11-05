// =====================================================================
// ESERCITAZIONE 9.1: API CRUD di piloti con il modello CSR — SOLUZIONE (con i bonus)
// EN: EXERCISE 9.1: CRUD API of drivers with the CSR pattern — SOLUTION (with the bonuses)
//
// Stessa API dell'esercitazione 8.1, ma il codice è diviso in tre livelli:
//   Controller (Endpoints/PilotaEndpoints.cs)       -> gestisce HTTP
//   Service    (Services/PilotaService.cs)          -> contiene le regole
//   Repository (Repositories/PilotaRepository.cs)   -> legge e scrive i dati
//   client -> Controller -> Service -> Repository -> dati
// EN: Same API as exercise 8.1, but the code is split into three layers:
// EN:   Controller (Endpoints/PilotaEndpoints.cs)       -> handles HTTP
// EN:   Service    (Services/PilotaService.cs)          -> holds the rules
// EN:   Repository (Repositories/PilotaRepository.cs)   -> reads and writes the data
// EN:   client -> Controller -> Service -> Repository -> data
// =====================================================================

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Tutti gli endpoint sono nel livello Controller: qui basta una riga
// EN: All the endpoints live in the Controller layer: one line is enough here
app.MapPilotiEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // http://localhost:5082/swagger
}

app.Run();
