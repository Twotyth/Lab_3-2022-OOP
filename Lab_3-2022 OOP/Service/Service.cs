namespace Lab_3_2022.Service;

using Commands;
using Repos;
using Model;

public class Service 
{
    private static Service? _instance;
    private readonly DogRepos _dogRepos;
    private readonly List<string> _commands;

    public int ReposSize { get; private set; }
    public int ReposCount => _dogRepos.Count;


    public DogRepos DogRepos
    {
        get => default;
        set
        {
        }
    }

    private Service()
    {
        _dogRepos = DogRepos.GetInstance();
        _commands = Enum.GetNames<Command>().ToList();
    }
    public ICommand ReadCommand(string? input)
    {
        if (input == null || !_commands.Contains(input))
            return new InvalidCommand();
        var commandType = Type.GetType("Lab_3_2022.Commands." + input);

        if (commandType == typeof(Add) && ReposSize == _dogRepos.Count) 
            return new InvalidCommand();
        
        return (ICommand)Activator.CreateInstance(commandType)!;
    }

    public void SetReposSize(int size) => ReposSize = size;
    
    public void AddObj(Dog item) => _dogRepos.Add(item);
    public void DeleteObj(Dog item) => _dogRepos.Delete(item);

    public Dog? FindObj(Predicate<Dog> predicate) => _dogRepos.Find(predicate);
    public Dog? FindObj(int index) => _dogRepos.Find(index);
    public List<Dog> FindAllObjs(Predicate<Dog> predicate) => _dogRepos.FindAll(predicate);
    
    public void DisplayObjs() => _dogRepos.DisplayAll();

    public bool ParseNew(string? s)
    {
        if (s is null) return false;
        return _dogRepos.ParseNew(s);
    }

    public string GetParseString(int index) => _dogRepos.Find(index)!.Parse();
    public string GetParseString(Dog obj) => obj.Parse();

    public static Service GetInstance() => _instance ??= new Service();
}

