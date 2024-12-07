namespace Orders.Api.EventListeners
{
    public interface IEventListener
    {
        Task Listen(CancellationToken token);
    }
}
