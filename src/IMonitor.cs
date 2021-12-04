using System.Reflection;

namespace Monitor
{
    /// <summary>
    /// Creates monitors for different types of operations.
    /// </summary>
    public interface IMonitor
    {
        IInstrument Instrument(MethodBase method);
        IInstrument<T> Instrument<T>(MethodBase method);
        IQueryMonitor<TOutput> Query<TOutput>(MethodBase query);
        IQueryMonitor<TInput, TOutput> Query<TInput, TOutput>(MethodBase query);
    }
}
