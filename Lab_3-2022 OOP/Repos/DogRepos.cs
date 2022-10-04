namespace Lab_3_2022.Repos;

using Model;

public class DogRepos : IObjRepos<Dog>
{
    private static DogRepos? _instance;
    private List<Dog> _dogs;
    private DogRepos()
    {
        _dogs = new();
    }

    public int Count => _dogs.Count;

    public DogRepos DogRepos1
    {
        get => default;
        set
        {
        }
    }

    public void Add(Dog item) => _dogs.Add(item);
    public void Delete(Dog item) => _dogs.Remove(item);
    public Dog? Find(Predicate<Dog> predicate) => _dogs.Find(predicate);
    public Dog? Find(int index) => index >= _dogs.Count ? null : _dogs[index];
    public List<Dog> FindAll(Predicate<Dog> predicate) => _dogs.FindAll(predicate);
    public bool Any() => _dogs.Any();
    public void Display(int index) => Console.WriteLine($"{index + 1}. {_dogs[index].AsString}");
    public void DisplayAll()
    {
        for (int i = 0; i < _dogs.Count; i++)
            Display(i);
    }

    public bool ParseNew(string s)
    {
        Dog? parsed = Dog.Parse(s);
        if (parsed != null) Add(parsed);
        return parsed != null;
    }
    public static DogRepos GetInstance() => _instance ??= new();
}