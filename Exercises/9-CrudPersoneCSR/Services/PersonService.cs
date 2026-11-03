// LIVELLO 2 — SERVICE: contiene le regole del programma (es. "l'età non può
// essere negativa"). Non sa nulla di HTTP né di come sono salvati i dati.
// Qui non ci sono ancora regole, quindi passa tutto al Repository.
// EN: LAYER 2 — SERVICE: holds the program rules (e.g. "the age cannot be
// EN: negative"). It knows nothing about HTTP or how the data is stored.
// EN: There are no rules here yet, so it passes everything to the Repository.
public class PersonService
{
    private PersonRepository personRepository = new PersonRepository();

    public IEnumerable<Person> GetPeople()
    {
        return personRepository.GetPeople();
    }
    public Person? GetPerson(int id)
    {
        return personRepository.GetPerson(id);
    }
    public bool Create(Person person)
    {
        return personRepository.Create(person);
    }
    public bool Update(Person person)
    {
        return personRepository.Update(person);
    }
    public bool Delete(int id)
    {
        return personRepository.Delete(id);
    }
}
