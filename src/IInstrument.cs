using System;
using Chronology;

namespace Monitor
{
    /// <summary>
    /// Instruments operations without inputs or outputs.
    /// </summary>
    /// <remarks>
    /// Instances of <see cref="IInstrument"/> are obtained using <see cref="IMonitor.Instrument()"/>
    /// methods because the instrument needs information about the operation. An
    /// <see cref="ITelemetryDescriptor{T}"/> defines telemetry that extracted from the
    /// <see cref="Exception"/> instances.
    /// </remarks>
    public interface IInstrument
    {
        /// <summary>
        /// Starts a measurement, recording it's start time.
        /// </summary>
        HighResolutionTimestamp Start();
        void Measure(HighResolutionTimestamp startTime = default, Exception? exception = null);
    }

    /// <summary>
    /// Instruments operations with a single input or a single output.
    /// </summary>
    /// <typeparam name="T">
    /// Type of input or output.
    /// </typeparam>
    /// <remarks>
    /// An <see cref="ITelemetryDescriptor{T}"/> defines telemetry that extracted from the
    /// <typeparamref name="T"/> and <see cref="Exception"/> instances.
    /// </remarks>
    public interface IInstrument<in T>
    {
        HighResolutionTimestamp Start();
        void Measure(HighResolutionTimestamp startTime = default, Exception? exception = null, T? subject = default);
    }

    /// <summary>
    /// Instruments operations with a single input and a single output.
    /// </summary>
    /// <typeparam name="T1">
    /// Type of input.
    /// </typeparam>
    /// <typeparam name="T2">
    /// Type of output.
    /// </typeparam>
    /// <remarks>
    /// An <see cref="ITelemetryDescriptor{T}"/> defines telemetry that extracted from the
    /// <typeparamref name="T1"/>, <typeparamref name="T2"/> and <see cref="Exception"/> instances.
    /// </remarks>
    public interface IInstrument<in T1, in T2>
    {
        HighResolutionTimestamp Start();
        void Measure(HighResolutionTimestamp startTime = default, Exception? exception = null, T1? subject1 = default, T2? subject2 = default);
    }
}
