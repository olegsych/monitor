using System;
using Chronology;

namespace Monitor
{
    public static class IInstrumentExtensions
    {
        public static void Measure(this IInstrument instrument, Exception exception) =>
            instrument.Measure(default, exception);

        public static void Measure<T>(this IInstrument<T> instrument, T subject) =>
            instrument.Measure(default, default, subject);

        public static void Measure<T>(this IInstrument<T> instrument, HighResolutionTimestamp start, T subject) =>
            instrument.Measure(start, default, subject);

        public static void Measure<T>(this IInstrument<T> instrument, Exception? exception, T subject) =>
            instrument.Measure(default, exception, subject);
    }
}
