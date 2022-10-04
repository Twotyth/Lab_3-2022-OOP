namespace Lab_3_2022.Commands;

using Service;
using Model;

public class Find : ICommand
{
    private readonly Service _service = Service.GetInstance();

    public void Execute()
    {
        List<Dog> found = new();
        
        string? searchQuery;


        Console.WriteLine("\nPlease input dog's Name and(or) Age in exact order as said: ");

        do Console.Write("\n> ");
        while 
        (
            (searchQuery = Console.ReadLine()) == null ||
            (searchQuery = searchQuery.Trim()) == string.Empty 
        );

        var searchTuple = searchQuery.Split(' ');
        
        switch (searchTuple.Length)
        {
            case 1:

                if (int.TryParse(searchQuery, out int tryInt1))
                    found.AddRange(_service.FindAllObjs(d => d.Age == tryInt1));
                
                found.AddRange(_service.FindAllObjs(d => d.Name.Contains(searchQuery)));
                break;
            
            default:

                var temp = new string[2];

                for (int i = 0; i < searchTuple.Length - 1; i++)
                    temp[0] += searchTuple[i];
                temp[1] = searchTuple[^1];

                searchTuple = temp;
                
                bool isLastParsed = int.TryParse(searchTuple[1], out int tryInt2);
                
                
                
                if (isLastParsed)
                {
                    found.AddRange(_service.FindAllObjs(d => d.Name.Contains(searchTuple[0])));
                    found.AddRange(_service.FindAllObjs(d => d.Age == tryInt2));
                }
                
                found.AddRange(_service.FindAllObjs(d => d.Name.Contains(searchQuery)));

                found = found.ToHashSet().ToList();
                break;
        }

        if (found.Count == 0)
        {
            Console.WriteLine("\nNone dogs have been found by this search. Announce command again to try another search query.\n");
            return;
        }
        
        Console.WriteLine($"\n{found.Count} dog(s) have been found:\n");
        
        for (int i = 1; i <= found.Count; i++)
            Console.WriteLine($"{i}. " + found[i-1]);
    }

    public List<Dog> ExecuteReturn()
    {
        List<Dog> found = new();

        string? searchQuery;

        Console.WriteLine("\nPlease input dog's Name and(or) Age in exact order as said: ");

        do Console.Write("\n> ");
        while 
        (
            (searchQuery = Console.ReadLine()) == null ||
            (searchQuery = searchQuery.Trim()) == string.Empty 
        );

        var searchTuple = searchQuery.Split(' ');
        
        switch (searchTuple.Length)
        {
            case 1:

                if (int.TryParse(searchQuery, out int tryInt1))
                    found.AddRange(_service.FindAllObjs(d => d.Age == tryInt1));
                
                found.AddRange(_service.FindAllObjs(d => d.Name.Contains(searchQuery)));
                break;
            
            default:

                var temp = new string[2];

                for (int i = 0; i < searchTuple.Length - 1; i++)
                    temp[0] += searchTuple[i];
                temp[1] = searchTuple[^1];

                searchTuple = temp;
                
                bool isLastParsed = int.TryParse(searchTuple[1], out int tryInt2);
                
                
                
                if (isLastParsed)
                {
                    found.AddRange(_service.FindAllObjs(d => d.Name.Contains(searchTuple[0])));
                    found.AddRange(_service.FindAllObjs(d => d.Age == tryInt2));
                }
                
                found.AddRange(_service.FindAllObjs(d => d.Name.Contains(searchQuery)));

                found = found.ToHashSet().ToList();
                break;
        }

        if (found.Count == 0)
        {
            Console.WriteLine("\nNone dogs have been found by this search. Announce command again to try another search query.\n");
            return found;
        }
        
        Console.WriteLine($"\n{found.Count} dog(s) have been found:\n");
        
        for (int i = 1; i <= found.Count; i++)
            Console.WriteLine($"{i}. " + found[i-1]);

        return found;
    }
    
}