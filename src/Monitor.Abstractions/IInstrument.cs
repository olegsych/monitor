using System;

namespace Athene.Monitor
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
        /// Starts a observation, recording it's start time.
        /// </summary>
        Observation Start();
        void Record(Observation observation = default, Exception? exception = null);
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
        Observation Start();
        void Record(Observation observation = default, Exception? exception = null, T? subject = default);
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
        Observation Start();
        void Record(Observation observation = default, Exception? exception = null, T1? subject1 = default, T2? subject2 = default);
    }
}
