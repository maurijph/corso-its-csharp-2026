// LIVELLO 3 — REPOSITORY: l'unico livello che sa dove sono salvati i dati.
// Se domani si passa a un database, si cambia solo questa classe.
// EN: LAYER 3 — REPOSITORY: the only layer that knows where the data is stored.
// EN: If tomorrow we move to a database, only this class changes.
public class PersonRepository
{
    // private: nessun altro livello può toccare il dizionario
    // EN: private: no other layer can touch the dictionary
    private Dictionary<int, Person> dict = new Dictionary<int, Person>();

    public IEnumerable<Person> GetPeople()
    {
        if(dict.Count > 0)
            return dict.Values.ToArray();
        else
            return [];
    }
    public Person? GetPerson(int id)
    {
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
