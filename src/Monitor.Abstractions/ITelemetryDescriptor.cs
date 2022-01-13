namespace Athene.Monitor
{
    /// <summary>
    /// Describes telemetry of a given <typeparamref name="TSubject"/> type.
    /// </summary>
    /// <typeparam name="TSubject">
    /// Type for which telemetry needs to be collected.
    /// </typeparam>
    /// <remarks>
    /// <see cref="Monitor"/> will provide a default implementation of
    /// <see cref="ITelemetryDescriptor{TSubject}"/> that infers telemetry from the
    /// public <typeparamref name="TSubject"/> properties and fields using reflection.
    /// Application developers can provide their own implementation of this interface
    /// to take complete control of telemetry extraction of their types.
    /// </remarks>
    public interface ITelemetryDescriptor<TSubject>
    {
        void Describe(ITelemetryDescription<TSubject> description);
    }
}
