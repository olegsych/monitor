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
        IInstrument<T1, T2> Instrument<T1, T2>(MethodBase method);
    }
}
