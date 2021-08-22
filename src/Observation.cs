using System;

namespace Monitor
{
    public struct Observation
    {
        public Utc StartTime {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }
    }

    public struct Observation<T>
    {
        public Utc StartTime {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }

        public T Input {
            get => throw new NotImplementedException();
            internal set => throw new NotImplementedException();
        }
    }
}
