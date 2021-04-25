using System;

namespace Monitor
{
    public interface IQueryMonitor<in TOutput>: IDisposable
    {
        void Observe(TOutput output);
        void Observe(Exception e);
        IObservation<TOutput> Start();
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
    /// Instances of <see cref="IQueryMonitor{TInput, TOutput}"/> are obtained using <see cref="IMonitor.Create{TInput, TOutput}(Type)"/>
    /// because in addition to the input and output, the monitor also needs information about the actor.
    /// </remarks>
    public interface IQueryMonitor<in TInput, in TOutput>: IDisposable
    {
        void Observe(TInput input, TOutput output);
        void Observe(TInput input, Exception exception);
        IObservation<TOutput> Start(TInput input);
    }
}
