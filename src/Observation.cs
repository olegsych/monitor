using System;

namespace Monitor
{
    public readonly struct Observation
    {
        public Utc StartTime {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }
    }

    public readonly struct Observation<T>
    {
        public Utc StartTime {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }

        public T Input {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }
    }
}
