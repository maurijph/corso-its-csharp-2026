// Progetto Web API: parte e resta in ascolto delle richieste HTTP finché non lo fermi (Ctrl+C).
// EN: Web API project: it starts and keeps listening for HTTP requests until you stop it (Ctrl+C).
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Un solo endpoint: GET / risponde con un testo.
// EN: A single endpoint: GET / answers with a text.
app.MapGet("/", () => "Hello, World! (da ProgettoWebAPI)");

app.Run();
