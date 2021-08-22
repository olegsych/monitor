using System;

namespace Monitor
{
    /// <summary>
    /// Monitors operations with given <typeparamref name="TOutput"/> type.
    /// </summary>
    /// <typeparam name="TOutput">
    /// Type of output. Operations with multiple outputs should define a distinct type
    /// to combine the outputs into the same class or struct, or use a <see cref="Tuple"/>.
    /// </typeparam>
    /// <remarks>
    /// Instances of <see cref="IQueryMonitor{TOutput}"/> are obtained using <see cref="IMonitor.Query{TOutput}()"/>
    /// methods because in addition to the output, the monitor also needs information about the operation.
    /// </remarks>
    public interface IQueryMonitor<in TOutput>: IDisposable
    {
        Observation Start();
        void Record(Observation observation, TOutput? output, Exception? exception);
    }

    /// <summary>
    /// Monitors operations with given <typeparamref name="TInput"/> and <typeparamref name="TOutput"/> types.
    /// </summary>
    /// <typeparam name="TInput">
    /// Type of input. Operations with multiple inputs should define a distinct type
    /// to combine the inputs into the same class or struct, or use a <see cref="Tuple"/>.
    /// </typeparam>
    /// <typeparam name="TOutput">
    /// Type of output. Operations with multiple outputs should define a distinct type
    /// to combine the outputs into the same class or struct, or use a <see cref="Tuple"/>.
    /// </typeparam>
    /// <remarks>
    /// Instances of <see cref="IQueryMonitor{TInput, TOutput}"/> are obtained using <see cref="IMonitor.Query{TInput, TOutput}()"/>
    /// methods because in addition to the input and output, the monitor also needs information about the operation.
    /// </remarks>
    public interface IQueryMonitor<TInput, in TOutput>: IDisposable
    {
        Observation<TInput> Start(TInput input);
        void Record(Observation<TInput> observation, TOutput? output, Exception? exception);
    }
}
