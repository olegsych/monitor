namespace Athene.Monitor
{
    /// <summary>
    /// Shows how telemetry attributes can be applied to customize telemetry recording.
    /// </summary>
    public class AttributeExample
    {
        class Subject
        {
            [TelemetryProperty("foo")]
            string GetFoo() => string.Empty;

            [TelemetryMetric("bar")]
            int GetBar() => default;

            [TelemetryDimension("baz")]
            string GetBaz() => string.Empty;
        }
    }
}
