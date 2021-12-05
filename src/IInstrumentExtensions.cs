using System;

namespace Monitor
{
    public static class IInstrumentExtensions
    {
        public static void Record(this IInstrument instrument, Exception exception) =>
            instrument.Record(default, exception);

        public static void Record<T>(this IInstrument<T> instrument, T subject) =>
            instrument.Record(default, default, subject);

        public static void Record<T>(this IInstrument<T> instrument, Exception exception) =>
            instrument.Record(default, exception, default);

        public static void Record<T>(this IInstrument<T> instrument, Measurement measurement, T subject) =>
            instrument.Record(measurement, default, subject);

        public static void Record<T>(this IInstrument<T> instrument, Exception exception, T subject) =>
            instrument.Record(default, exception, subject);

        public static void Record<T1, T2>(this IInstrument<T1, T2> instrument, T1 subject1, T2 subject2) =>
            instrument.Record(default, default, subject1, subject2);

        public static void Record<T1, T2>(this IInstrument<T1, T2> instrument, Exception exception, T1 subject1) =>
            instrument.Record(default, exception, subject1, default);

        public static void Record<T1, T2>(this IInstrument<T1, T2> instrument, Measurement measurement, T1 subject1, T2 subject2) =>
            instrument.Record(measurement, default, subject1, subject2);
    }
}
