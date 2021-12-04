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

        public static void Measure<T>(this IInstrument<T> instrument, Exception exception) =>
            instrument.Measure(default, exception, default);

        public static void Measure<T>(this IInstrument<T> instrument, HighResolutionTimestamp start, T subject) =>
            instrument.Measure(start, default, subject);

        public static void Measure<T>(this IInstrument<T> instrument, Exception exception, T subject) =>
            instrument.Measure(default, exception, subject);

        public static void Measure<T1, T2>(this IInstrument<T1, T2> instrument, T1 subject1, T2 subject2) =>
            instrument.Measure(default, default, subject1, subject2);

        public static void Measure<T1, T2>(this IInstrument<T1, T2> instrument, Exception exception, T1 subject1) =>
            instrument.Measure(default, exception, subject1, default);

        public static void Measure<T1, T2>(this IInstrument<T1, T2> instrument, HighResolutionTimestamp start, T1 subject1, T2 subject2) =>
            instrument.Measure(start, default, subject1, subject2);
    }
}
