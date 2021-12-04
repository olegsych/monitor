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
}
