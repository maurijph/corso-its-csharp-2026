// LIVELLO 3 — REPOSITORY: l'unico livello che sa dove sono salvati i piloti.
// Legge e scrive nel dizionario e controlla solo i vincoli dei dati
// (es. "due piloti non possono avere la stessa chiave"), mai le regole del programma.
// EN: LAYER 3 — REPOSITORY: the only layer that knows where the drivers are stored.
// EN: It reads and writes the dictionary and only checks data constraints
// EN: (e.g. "two drivers cannot have the same key"), never the program rules.
public class PilotaRepository
{
    // private: nessun altro livello può toccare il dizionario
    // EN: private: no other layer can touch the dictionary
    private Dictionary<int, Pilota> piloti = new Dictionary<int, Pilota>();

    // Il costruttore riempie il dizionario con i due piloti di partenza, come nella 8.1
    // EN: The constructor fills the dictionary with the two starting drivers, as in 8.1
    public PilotaRepository()
    {
        piloti.Add(1, new Pilota { Id = 1, Nome = "Charles Leclerc", Scuderia = "Ferrari", Numero = 16 });
        piloti.Add(2, new Pilota { Id = 2, Nome = "Lando Norris", Scuderia = "McLaren", Numero = 4 });
    }

    // Restituisce una copia dei valori: chi la riceve non può modificare il dizionario
    // EN: Returns a copy of the values: whoever receives it cannot change the dictionary
    public IEnumerable<Pilota> GetPiloti()
    {
        return piloti.Values.ToArray();
    }

    // null se l'id non esiste: sarà il Controller a trasformarlo in 404
    // EN: null if the id does not exist: the Controller will turn it into a 404
    public Pilota? GetPilota(int id)
    {
        if (piloti.ContainsKey(id))
            return piloti[id];
        else
            return null;
    }

    // Vincolo dei dati: la chiave del dizionario deve essere unica
    // EN: Data constraint: the dictionary key must be unique
    public bool Create(Pilota pilota)
    {
        if (piloti.ContainsKey(pilota.Id))
            return false;

        piloti.Add(pilota.Id, pilota);
        return true;
    }

    public bool Update(Pilota pilota)
    {
        if (!piloti.ContainsKey(pilota.Id))
            return false;

        piloti[pilota.Id] = pilota;
        return true;
    }

    public bool Delete(int id)
    {
        if (!piloti.ContainsKey(id))
            return false;

        piloti.Remove(id);
        return true;
    }
}
