using Chronology;

namespace Monitor
{
    public readonly struct Measurement
    {
        public HighResolutionTimestamp StartTime { get; }
        public object State { get; }
    }
}
