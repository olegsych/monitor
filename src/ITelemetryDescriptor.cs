namespace Monitor
{
    /// <summary>
    /// Describes telemetry of a given <typeparamref name="TSubject"/> type.
    /// </summary>
    /// <typeparam name="TSubject"><
    /// Type for which telemetry needs to be collected.
    /// </typeparam>
    public interface ITelemetryDescriptor<TSubject>
    {
        void Describe(ITelemetryDescription<TSubject> description);
    }
}
