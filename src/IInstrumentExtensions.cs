using System;

namespace Monitor
{
    public static class IInstrumentExtensions
    {
        public static void Measure(this IInstrument instrument, Exception exception) =>
            instrument.Measure(default, exception);
    }
}
