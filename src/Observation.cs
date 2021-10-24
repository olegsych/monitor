using System;
using Chronology;

namespace Monitor
{
    public readonly struct Observation
    {
        public HighResolutionTimestamp StartTime {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }
    }

    public readonly struct Observation<T>
    {
        public HighResolutionTimestamp StartTime {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }

        public T Input {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }
    }
}
