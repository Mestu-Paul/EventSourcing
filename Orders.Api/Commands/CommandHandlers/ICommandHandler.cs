namespace Orders.Api.Commands.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand: ICommand
    {
        Task HandleAsync(TCommand  command);
    }

    /*public interface ICommandHandler
    {
        Task HandleAsync(ICommand command);
    }*/
}
