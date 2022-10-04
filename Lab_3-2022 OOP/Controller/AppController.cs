namespace Lab_3_2022.Controller;

using Service;
using Commands;

public class AppController
{
    private readonly Service _service;
    public AppController() => _service = Service.GetInstance();

    public Service Service
    {
        get => default;
        set
        {
        }
    }

    public Command Command
    {
        get => default;
        set
        {
        }
    }

    public void OnLaunch()
    {
        Console.WriteLine("Enter the amount of objects to store:\n");

        int n;

        do
        {
            Console.Write("> ");
        } while (!int.TryParse(Console.ReadLine(), out n) || n < 1);

        _service.SetReposSize(n);
        
        Console.Clear();
        ShowInstructions();
    }
    
    public void Poll()
    {
        int consoleBuffer = 1;
        int count = 0;

        while (true)
        {
            ICommand command;

            


            Console.WriteLine("\nAwaiting input...\n");
            Console.Write("> ");
            while ((command = _service.ReadCommand(Console.ReadLine())) is InvalidCommand)
                Console.Write("\nInvalid command input! Try again.\n> ");

            if (command is Exit) break;
            
            if (count == consoleBuffer)
            {
                count = 0;
                Console.Clear();
                ShowInstructions();
            }

            command.Execute();
            
            count++;
        }
    }

    private static void ShowInstructions()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine
        (
            "Dog-market\n" +
            "Commands:\t'Add' -- randomly choose between typing in a new Dog-object or adding a random one\n" +
            "\t\t'Delete' -- type in an index of Dog-object to delete\n" +
            "\t\t'Find' -- type in an name or (and) age of some Dog-object(s) to display\n" +
            "\t\t'Parse' -- parse input string to a new Dog object\n" +
            "\t\t'Display' -- display all stored Dog-objects\n" +
            "\t\t'Exit' -- exit the program" +
            "\n"
        );
        Console.ResetColor();
    }
}