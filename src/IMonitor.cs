namespace Monitor
{
    /// <summary>
    /// Creates monitors for different types of operations.
    /// </summary>
    public interface IMonitor
    {
        ICommandMonitor Command(Command command);
        ICommandMonitor<TInput> Command<TInput>(Command<TInput> command);
        IQueryMonitor<TOutput> Query<TOutput>(Query<TOutput> query);
        IQueryMonitor<TInput, TOutput> Query<TInput, TOutput>(Query<TInput, TOutput> query);
    }
}
