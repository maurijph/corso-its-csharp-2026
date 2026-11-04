// LIVELLO 3 — REPOSITORY: l'unico livello che sa dove sono salvati i dati.
// Se domani si passa a un database, si cambia solo questa classe o se ne crea una nuova implementazione.
// EN: LAYER 3 — REPOSITORY: the only layer that knows where the data is stored.
// EN: If tomorrow we move to a database, only this class changes or a new implementation is created.
public class PersonRepository
{
    // private: nessun altro livello può toccare il dizionario
    // EN: private: no other layer can touch the dictionary
    private Dictionary<int, Person> dict = new Dictionary<int, Person>();

    // Si usa un IEnumerable per restituire tutte le persone ed evitare di esporre direttamente il dizionario.
    // EN: An IEnumerable is used to return all people and avoid exposing the dictionary directly.
    public IEnumerable<Person> GetPeople()
    {
        if(dict.Count > 0)
            return dict.Values.ToArray();
        else
            return [];
    }
    public Person? GetPerson(int id)
    {
        // Perchè si controlla l'id se le logiche devono essere gestite dal Service e non dal Repository? 
        // (vincolo dati vs regole programma)
        // EN: Why check the id if the logic should be handled by the Service and not the Repository?
        // EN: (data constraint vs program rules)
        if(dict.Count > 0 && dict.ContainsKey(id))
            return dict[id];
        else
            return null;
    }
    public bool Create(Person person)
    {
        if (dict.Count > 0 && dict.ContainsKey(person.id))
            return false;
        else
        {
            dict.Add(person.id, person);
            return true;
        }
    }
    public bool Update(Person person)
    {
        if ((dict.Count > 0 && !dict.ContainsKey(person.id)) || dict.Count == 0)
            return false;
        else
        {
            dict[person.id] = person;
            return true;
        }
    }
    public bool Delete(int id)
    {
        if ((dict.Count > 0 && !dict.ContainsKey(id)) || dict.Count == 0)
            return false;
        else
        {
            dict.Remove(id);
            return true;
        }
    }
}
