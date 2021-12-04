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
    /// Instances of <see cref="ICommandMonitor{TInput}"/> are obtained using <see cref="IMonitor.Command{TInput}()"/>
    /// methods because in addition to the input, the monitor also needs information about the operation.
    /// </remarks>
    public interface ICommandMonitor<TInput>: IDisposable
    {
        Observation<TInput> Start(TInput input);
        void Record(Observation<TInput> observation, Exception? exception);
    }
}
