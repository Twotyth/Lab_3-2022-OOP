namespace Lab_3_2022.Commands;

using Service;

public class Parse : ICommand
{
    private static readonly Service _service = Service.GetInstance();
    public void Execute()
    {
        Console.WriteLine("Choose an operation:" +
                          "\n\t1 - Parse a new dog from input" +
                          "\n\t2 - Get stored dog in Parse format");

        string? input;
        int choice;
        do
        {
            Console.Write("\n> ");
            input = Console.ReadLine()?.Trim();
        } while ( 
            !int.TryParse(input, out choice) 
            && choice is not 1 and not 2);

        switch (choice)
        {
            case 1: InternalParse();
                break;
            case 2: InternalWriteToParsable();
                break;
        }
    }

    private void InternalParse()
    {
        if (_service.ReposCount == _service.ReposSize)
        {
            Console.WriteLine("Maximum repository size reached. Can't add a new dog.\n");
            return;
        }
        
        Console.WriteLine(
            "\nParse format:" +
            "\n\tmust be 3 or 4 values splitted with ',' in exact order as said:" +
            "\n\t1 - dog Name -- can be any, but not empty" +
            "\n\t2 - dog Age -- must be >= 0" +
            "\n\t3 - dog Breed -- must either 'none' or 'mixed' or 'pedigree'. Case is ignored." +
            "\n\t4 - dog Description (optional) -- can be any."
        ); // Format instructions
        
        Console.Write("\n> ");
        bool isParsed = _service.ParseNew(Console.ReadLine());

        Console.WriteLine(
            isParsed 
                ? "\nSuccessfully parsed new Dog object.\n" 
                : "\nInvalid string format, can't be parsed\n");
    }

    private void InternalWriteToParsable()
    {
        if (_service.ReposCount == 0)
        {
            Console.WriteLine("No objects stored to get.");
            return;
        }
        
        string? input;
        int choice;

        if (_service.ReposCount == 1)
            choice = 1;
        else
        {
            _service.DisplayObjs();
            Console.WriteLine("\nType in dog's num from displayed to get parse-format string:");


            do
            {
                Console.Write("\n> ");
                input = Console.ReadLine()?.Trim();
            } while (
                !int.TryParse(input, out choice)
                && (choice < 0 || choice >= _service.ReposCount));
        }

        var chosen = _service.FindObj(choice - 1);
        
        Console.WriteLine($"\n{chosen!.Name}'s parse-format string:");
        Console.WriteLine(_service.GetParseString(chosen));
    }
}