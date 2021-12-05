using System;
using Chronology;

namespace Monitor
{
    /// <summary>
    /// Instruments operations without inputs or outputs
    /// </summary>
    /// <remarks>
    /// Instances of <see cref="IInstrument"/> are obtained using <see cref="IMonitor.Instrument()"/>
    /// methods because the instrument needs information about the operation.
    /// </remarks>
    public interface IInstrument
    {
        /// <summary>
        /// Starts an measurement, recording it's start time.
        /// </summary>
        HighResolutionTimestamp Start();
        void Measure(HighResolutionTimestamp startTime = default, Exception? exception = null);
    }

    public interface IInstrument<in T>
    {
        HighResolutionTimestamp Start();
        void Measure(HighResolutionTimestamp startTime = default, Exception? exception = null, T? subject = default);
    }

    public interface IInstrument<in T1, in T2>
    {
        HighResolutionTimestamp Start();
        void Measure(HighResolutionTimestamp startTime = default, Exception? exception = null, T1? subject1 = default, T2? subject2 = default);
    }
}