// LIVELLO 2 — SERVICE: contiene le regole del programma (es. "l'età non può
// essere negativa"). Non sa nulla di HTTP né di come sono salvati i dati.
// Prende i dati dal Repository, li controlla o li elabora, e solo dopo
// li restituisce al Controller.
// EN: LAYER 2 — SERVICE: holds the program rules (e.g. "the age cannot be
// EN: negative"). It knows nothing about HTTP or how the data is stored.
// EN: It takes the data from the Repository, checks or processes it, and only
// EN: then returns it to the Controller.
public class PersonService
{
    // Età dalla quale una persona è considerata maggiorenne
    // EN: Age from which a person is considered an adult
    private const int AdultAge = 18;

    private PersonRepository personRepository = new PersonRepository();

    public IEnumerable<Person> GetPeople()
    {
        return personRepository.GetPeople();
    }
    public Person? GetPerson(int id)
    {
        return personRepository.GetPerson(id);
    }

    // Regola: restituisce solo le persone maggiorenni.
    // Il Repository non sa cosa sia un "adulto": il Service prende tutte le
    // persone con GetPeople() e tiene solo quelle che rispettano la regola.
    // EN: Rule: returns only the adult people.
    // EN: The Repository does not know what an "adult" is: the Service takes all
    // EN: the people with GetPeople() and keeps only those that follow the rule.
    public IEnumerable<Person> GetAdults()
    {
        IEnumerable<Person> people = personRepository.GetPeople();
        return people.Where(p => p.Age >= AdultAge).ToArray();
    }


    /// <summary>
    /// Crea una nuova persona se i dati sono validi.
    /// EN: Creates a new person if the data is valid.
    /// </summary>
    /// <param name="person">La persona da creare.
    /// EN: The person to create.
    /// </param>
    /// <returns>True se la persona è stata creata con successo, false altrimenti.
    /// EN: True if the person was successfully created, false otherwise.
    /// </returns>
    public bool Create(Person person)
    {
        // Dati non validi: il Repository non viene nemmeno chiamato
        // EN: Invalid data: the Repository is not even called
        if (!IsValid(person))
            return false;
        return personRepository.Create(person);
    }

    // Regola: non si può aggiornare una persona con età negativa.
    // anche qui non viene chiamato il Repository se i dati non sono validi.
    // EN: Rule: cannot update a person with negative age
    // EN: here too the Repository is not called if the data is not valid.
    public bool Update(Person person)
    {
        if (!IsValid(person))
            return false;
        return personRepository.Update(person);
    }
    
    public bool Delete(int id)
    {
        return personRepository.Delete(id);
    }

    // Regole comuni a Create e Update, scritte una volta sola.
    // private: è un dettaglio interno del Service, il Controller non lo vede.
    // EN: Rules shared by Create and Update, written only once.
    // EN: private: it is an internal detail of the Service, the Controller cannot see it.
    private bool IsValid(Person person)
    {
        if (string.IsNullOrWhiteSpace(person.Name))
            return false; // il nome è obbligatorio — EN: the name is required
        if (person.Age < 0)
            return false; // l'età non può essere negativa — EN: the age cannot be negative
        return true;
    }
}
