using System.Reflection;

namespace Athene.Monitor
{
    /// <summary>
    /// Creates instruments for different types of operations.
    /// </summary>
    /// <remarks>
    /// <see cref="IMonitor"/> is the main instrumentation API.
    /// Application types typically inject this interface as a
    /// constructor parameter and create instruments for measuring
    /// their methods.
    /// </remarks>
    public interface IMonitor
    {
        IInstrument Instrument(MethodBase method);
        IInstrument<T> Instrument<T>(MethodBase method);
        IInstrument<T1, T2> Instrument<T1, T2>(MethodBase method);
    }
}
