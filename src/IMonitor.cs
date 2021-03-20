using System;

namespace Monitor
{
    /// <summary>
    /// Monitors operations with a given <typeparamref name="TInput"/> type and no output.
    /// </summary>
    /// <typeparam name="TInput">
    /// Type of input. Operations with multiple inputs should define a distinct type
    /// to combine the inputs into the same class or struct, or use a <see cref="Tuple"/>.
    /// </typeparam>
    /// <remarks>
    /// Instances of <see cref="IMonitor{TInput}"/> are obtained using <see cref="IMonitorFactory.Create{TInput}(Type)"/>
    /// because in addition to the input, the monitor also needs information about the actor.
    /// </remarks>
    public interface IMonitor<in TInput>: IDisposable
    {
        void Observe(TInput input);
        void Observe(TInput input, Exception exception);
        IObservation Start(TInput input);
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
    /// Instances of <see cref="IMonitor{TInput, TOutput}"/> are obtained using <see cref="IMonitorFactory.Create{TInput, TOutput}(Type)"/>
    /// because in addition to the input and output, the monitor also needs information about the actor.
    /// </remarks>
    public interface IMonitor<in TInput, in TOutput>: IDisposable
    {
        void Observe(TInput input, TOutput output);
        void Observe(TInput input, Exception exception);
        IObservation<TOutput> Start(TInput input);
    }
}
