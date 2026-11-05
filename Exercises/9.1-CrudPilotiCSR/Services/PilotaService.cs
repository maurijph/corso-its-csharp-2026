// LIVELLO 2 — SERVICE: contiene le regole del programma (es. "il nome è obbligatorio",
// "il numero di gara va da 1 a 99"). Non sa nulla di HTTP né di dove sono salvati i dati:
// chiede tutto al Repository.
// EN: LAYER 2 — SERVICE: holds the program rules (e.g. "the name is required",
// EN: "the race number goes from 1 to 99"). It knows nothing about HTTP or where the data
// EN: is stored: it asks the Repository for everything.
public class PilotaService
{
    // Numeri di gara ammessi
    // EN: Allowed race numbers
    private const int NumeroMinimo = 1;
    private const int NumeroMassimo = 99;

    private PilotaRepository pilotaRepository = new PilotaRepository();

    public IEnumerable<Pilota> GetPiloti()
    {
        return pilotaRepository.GetPiloti();
    }

    public Pilota? GetPilota(int id)
    {
        return pilotaRepository.GetPilota(id);
    }

    // Regola: tiene solo i piloti della scuderia indicata.
    // Il Repository non sa cosa sia una "scuderia": il Service prende tutti i piloti
    // e filtra. OrdinalIgnoreCase: "ferrari" e "Ferrari" sono la stessa scuderia.
    // EN: Rule: keeps only the drivers of the given team.
    // EN: The Repository does not know what a "team" is: the Service takes all the drivers
    // EN: and filters them. OrdinalIgnoreCase: "ferrari" and "Ferrari" are the same team.
    public IEnumerable<Pilota> GetPilotiDiScuderia(string nome)
    {
        IEnumerable<Pilota> piloti = pilotaRepository.GetPiloti();
        return piloti
            .Where(p => string.Equals(p.Scuderia, nome, StringComparison.OrdinalIgnoreCase))
            .ToArray();
    }

    // BONUS 1: l'ordinamento è una regola di presentazione, quindi sta nel Service
    // EN: BONUS 1: sorting is a presentation rule, so it lives in the Service
    public IEnumerable<Pilota> GetPilotiOrdinati()
    {
        IEnumerable<Pilota> piloti = pilotaRepository.GetPiloti();
        return piloti.OrderBy(p => p.Numero).ToArray();
    }

    public bool Create(Pilota pilota)
    {
        // Dati non validi: il Repository non viene nemmeno chiamato
        // EN: Invalid data: the Repository is not even called
        if (!IsValid(pilota))
            return false;

        // Regola solo per Create: il numero di gara non deve essere già usato
        // EN: Rule for Create only: the race number must not be already used
        if (NumeroGiaUsato(pilota.Numero, pilota.Id))
            return false;

        return pilotaRepository.Create(pilota);
    }

    public bool Update(Pilota pilota)
    {
        if (!IsValid(pilota))
            return false;

        // BONUS 2: anche in modifica il numero deve essere libero, ma il pilota
        // può tenere il proprio numero (per questo si esclude il suo id)
        // EN: BONUS 2: when changing, the number must be free too, but the driver
        // EN: may keep its own number (that is why its id is excluded)
        if (NumeroGiaUsato(pilota.Numero, pilota.Id))
            return false;

        return pilotaRepository.Update(pilota);
    }

    public bool Delete(int id)
    {
        return pilotaRepository.Delete(id);
    }

    // Regole comuni a Create e Update, scritte una volta sola.
    // private: è un dettaglio interno del Service, il Controller non lo vede.
    // EN: Rules shared by Create and Update, written only once.
    // EN: private: it is an internal detail of the Service, the Controller cannot see it.
    private bool IsValid(Pilota pilota)
    {
        if (string.IsNullOrWhiteSpace(pilota.Nome))
            return false; // il nome è obbligatorio — EN: the name is required
        if (string.IsNullOrWhiteSpace(pilota.Scuderia))
            return false; // la scuderia è obbligatoria — EN: the team is required
        if (pilota.Numero < NumeroMinimo || pilota.Numero > NumeroMassimo)
            return false; // numero fuori da 1-99 — EN: number outside 1-99
        return true;
    }

    // true se un ALTRO pilota (con id diverso da idEscluso) usa già quel numero.
    // In Create l'id è nuovo, quindi il confronto riguarda tutti i piloti.
    // EN: true if ANOTHER driver (with an id other than idEscluso) already uses that number.
    // EN: In Create the id is new, so the check covers all the drivers.
    private bool NumeroGiaUsato(int numero, int idEscluso)
    {
        return pilotaRepository.GetPiloti()
            .Any(p => p.Numero == numero && p.Id != idEscluso);
    }
    
}
