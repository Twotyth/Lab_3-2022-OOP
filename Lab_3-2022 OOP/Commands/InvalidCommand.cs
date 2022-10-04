namespace Lab_3_2022.Commands;

public class InvalidCommand : ICommand
{
    public void Execute() => throw new NotSupportedException();
}