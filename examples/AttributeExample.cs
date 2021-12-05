namespace Monitor
{
    /// <summary>
    /// Shows how telemetry attributes can be applied to customize telemetry recording.
    /// </summary>
    public class AttributeExample
    {
        class Subject
        {
            [TelemetryProperty]
            string GetProperty() => string.Empty;

            [TelemetryMetric]
            int GetMetric() => default;

            [TelemetryDimension]
            string GetDimension() => string.Empty;
        }
    }
}
