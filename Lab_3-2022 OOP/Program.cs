namespace Lab_3_2022;

using Controller;

class Program
{
    public AppController controller
    {
        get => default;
        set
        {
        }
    }

    // exit, add, delete, find, display
    public static void Main()
    {
        AppController controller = new();
        controller.OnLaunch();
        controller.Poll();
    }

}