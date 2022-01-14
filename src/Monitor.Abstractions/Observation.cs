using Chronology;

namespace Athene.Monitor
{
    public readonly record struct Observation
    {
        public HighResolutionTimestamp StartTime { get; }
        public object State { get; }
    }
}
