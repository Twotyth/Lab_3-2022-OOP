using System.Text.Json;
using System.Text.Json.Nodes;

namespace Lab_3_2022.Model;

public class Dog
{
    private static readonly string[] DogNames
        = JsonSerializer.Deserialize<string[]>
              (File.ReadAllText(@"C:\Users\timka\source\CS repos\Lab_3-2022 OOP\Lab_3-2022 OOP\Model\DogNames.json"))
        ?? throw new Exception();

    private string _name;
    private uint _age;
    private DogBreed _breed;
    private string? _desc;

    public Guid Id { get; } = new();

    public string Name
    {
        get => _name;
        init => _name = value;
    }

    public uint Age
    {
        get => _age;
        set => _age = value;
    }

    public DogBreed Breed
    {
        get => _breed;
        set => _breed = value;
    }

    public string? Desc
    {
        get => _desc;
        set => _desc = value;
    }

    public string AsString => $"{Name}, {Age}-year(s) old, breed - {Enum.GetName(Breed)}. {Desc}";

    public DogBreed DogBreed
    {
        get => default;
        set
        {
        }
    }

    public Dog(string name, uint age, DogBreed breed, string? description)
    {
        Name = name;
        Age = age;
        Breed = breed;
        Desc = description;
    }
    public Dog() // Random new dog
    {
        var rand = Random.Shared;
        Name = DogNames[rand.Next(0, DogNames.Length - 1)];
        Age = (uint)rand.Next(0, 29);
        Breed = (DogBreed)rand.Next(0, 2);
        Desc = "Customer added dog.(//random)";
    }
    public Dog(uint age, DogBreed breed, string? description) 
        : this("", age, breed, description) // new dog with random name
    {
        Name = DogNames[Random.Shared.Next(0, DogNames.Length - 1)];
    }
    public static Dog? Parse(string s)
    {
        string name;
        uint age;
        DogBreed breed;
        string? description = null;

        var dogParams = s.Trim().Split(',');
        for (int i = 0; i < dogParams.Length; i++)
            dogParams[i] = dogParams[i].Trim();
        
        switch (dogParams.Length)
        {
            case 3:
                if (dogParams[0] == string.Empty)
                    return null;

                name = dogParams[0];

                if (!uint.TryParse(dogParams[1], out age))
                    return null;

                if (
                    int.TryParse(dogParams[2], out _)
                    || !Enum.TryParse(dogParams[2], true, out breed)
                )
                    return null;

                break;
            case 4:
                description = dogParams[^1];

                if (dogParams[0] == string.Empty)
                    return null;

                name = dogParams[0];

                if (!uint.TryParse(dogParams[1], out age))
                    return null;

                if (
                    int.TryParse(dogParams[2], out _)
                    || !Enum.TryParse(dogParams[2], true, out breed)
                )
                    return null;

                break;
            default: return null;
        }
        
        return new(name, age, breed, description);
    }
    public static bool Parse(string s, out Dog res) 
        => (res = Parse(s) ?? new("", 0, 0, "")) is not null;
    public string Parse() => this.ToString();
    public override string ToString() => $"{Name},{Age},{Breed},{Desc}";

}