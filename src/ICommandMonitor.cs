using System;

namespace Monitor
{
    /// <summary>
    /// Monitors operations without inputs or outputs
    /// </summary>
    /// <remarks>
    /// Instances of <see cref="ICommandMonitor"/> are obtained using <see cref="IMonitor.Command()"/>
    /// methods because the monitor needs information about the operation.
    /// </remarks>
    public interface ICommandMonitor
    {
        void Observe();
        void Observe(Exception exception);
        IObservation Start();
    }

    /// <summary>
    /// Monitors operations with a given <typeparamref name="TInput"/> type and no output.
    /// </summary>
    /// <typeparam name="TInput">
    /// Type of input. Operations with multiple inputs should define a distinct type
    /// to combine the inputs into the same class or struct, or use a <see cref="Tuple"/>.
    /// </typeparam>
    /// <remarks>
    /// Instances of <see cref="ICommandMonitor{TInput}"/> are obtained using <see cref="IMonitor.Command{TInput}()"/>
    /// methods because in addition to the input, the monitor also needs information about the operation.
    /// </remarks>
    public interface ICommandMonitor<in TInput>: IDisposable
    {
        void Observe(TInput input);
        void Observe(TInput input, Exception exception);
        IObservation Start(TInput input);
    }
}
