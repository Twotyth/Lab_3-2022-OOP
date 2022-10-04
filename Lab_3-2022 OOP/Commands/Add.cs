namespace Lab_3_2022.Commands;

using Service;
using Model;

public class Add : ICommand
{
    private static readonly Service _service = Service.GetInstance();



    public void Execute()
    {
        Random random = new();
        var m = random.Next(0, 2);
        Action method = m switch
        {
            0 => AddInput,
            1 => AddRandom
        };
        method.Invoke();
    }

    private static void AddInput()
    {   
        string? name = null;
        long age = 0;
        int breed;
        string? description;

        string? input = null;
        bool isParsed;
        
        Console.WriteLine("Enter dog's name:");
        do
        {
            Console.Write("> ");
            input = Console.ReadLine();
        } while (input == null);

        name = input.Trim();

        Console.WriteLine("Enter dog's age:");
        do
        {
            do
            {
                Console.Write("> ");
                input = Console.ReadLine();
            } while (input == null);
            
            isParsed = long.TryParse(input, out age);
        } while (!isParsed || age is < 0 or > uint.MaxValue);

        Console.WriteLine("Enter dog's breed, where 0 - none, 1 - mixed, 2 - pedigree:");
        do
        {
            do
            {
                Console.Write("> ");
                input = Console.ReadLine();
            } while (input == null);

            isParsed = int.TryParse(input, out breed);
        } while (!isParsed || breed is < 0 or > 2);
        
        Console.WriteLine("Enter dog's brief description. If it has none, enter an empty string:");
        Console.Write("> ");
        input = Console.ReadLine();

        if (input != null) input = input.Trim();
        description = input;
            
        _service.AddObj(new Dog(name, (uint)age, (DogBreed)breed, description));
    }

    private static void AddRandom()
    {
        var toAdd = new Dog();
        _service.AddObj(toAdd);
        Console.WriteLine($"Added dog {toAdd.Name} randomly.");
    }
}