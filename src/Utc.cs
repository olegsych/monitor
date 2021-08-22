using System;

namespace Monitor
{
    public struct Utc
    {
        public static implicit operator DateTime(Utc utc) =>
            throw new NotImplementedException();

        public static implicit operator DateTimeOffset(Utc utc) =>
            throw new NotImplementedException();
    }
}
