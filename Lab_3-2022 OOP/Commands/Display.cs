namespace Lab_3_2022.Commands;

using Service;

public class Display : ICommand
{
    private readonly Service _service = Service.GetInstance();

    public void Execute()
    {
        _service.DisplayObjs();
    }
}