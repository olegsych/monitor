using Chronology;

namespace Athene.Monitor
{
    public readonly record struct Measurement
    {
        public HighResolutionTimestamp StartTime { get; }
        public object State { get; }
    }
}
