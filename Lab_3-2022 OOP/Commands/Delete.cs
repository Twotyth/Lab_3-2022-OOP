namespace Lab_3_2022.Commands;

using Service;
using Model;

public class Delete : ICommand
{
    private readonly Service _service = Service.GetInstance();

    public void Execute()
    {
        if (_service.ReposCount == 0)
        {
            Console.WriteLine("\nNo objects found to delete.\n");
            return;
        }
        string? input;
        Dog? chosenObj;
        
        Console.WriteLine("\nWhich method of deletion you want to proceed with?\nType 'Search' for search-deletion method and 'Index' for index-deletion method:\n");

        do
        {
            do Console.Write("\n> ");
            while ((input = Console.ReadLine()) == null);
            
            if (input is not ("Search" or "Index"))
                Console.WriteLine("\nInvalid choice. Please type 'Search' or 'Index' instead:");
        } while (input is not ("Search" or "Index"));

        chosenObj = input switch
        {
            "Search" => SearchMethod(),
            "Index" => IndexMethod(),
            _ => throw new()
        };

        if (chosenObj == null)
        {
            Console.WriteLine("Command discarded.");
            return;
        }
        
        Console.WriteLine($"\n{chosenObj.Name}'s going to be deleted. Type 'Yes' to proceed:");
        

        do Console.Write("\n> ");
        while ((input = Console.ReadLine()) == null);

        if (input.Trim() != "Yes")
        {
            Console.WriteLine("Command discarded.");
            return;
        }
        
        _service.DeleteObj(chosenObj);
    }

    private Dog? IndexMethod()
    {
        int index;
        string? input;

        Console.WriteLine("\nPlease enter desired object's index from stored to delete (or anything else to discard command):");
        
        do Console.Write("\n> ");
        while ((input = Console.ReadLine()) == null);

        if (!int.TryParse(input, out index) || index > _service.ReposCount || index < 1)
            return null;

        index--;
        
        return _service.FindObj(index);
    }

    private Dog? SearchMethod()
    {
        int index;
        string? input;
        
        var found = new Find().ExecuteReturn();
        
        if (found.Count == 0) return null;

        if (found.Count == 1) return found.First();
        
        Console.WriteLine("\nPlease enter desired object's index from displayed search results to delete (or anything else to discard command):");

        
        do Console.Write("\n> ");
        while ((input = Console.ReadLine()) == null);

        if (!int.TryParse(input, out index) || index > found.Count || index < 1)
            return null;

        index--;

        return _service.FindObj(index);
    }
}