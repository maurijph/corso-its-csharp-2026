namespace Esercitazione2Plus;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        if (args.Length > 0)
        {
            if(args[0] == "1")
            {
                Console.WriteLine("Ferrari");
            }
            else if(args[0] == "2")
            {
                Console.WriteLine("Lamborghini");
            }   
            else
            {
                Console.WriteLine("Unknown argument");
            }
        }
    }
}
