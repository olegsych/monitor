using System;

namespace Athene.Monitor
{
    public static class IInstrumentExtensions
    {
        public static void Record(this IInstrument instrument, Exception exception) =>
            throw new NotImplementedException();

        public static void Record<T>(this IInstrument<T> instrument, T subject) =>
            throw new NotImplementedException();

        public static void Record<T>(this IInstrument<T> instrument, Exception exception) =>
            throw new NotImplementedException();

        public static void Record<T>(this IInstrument<T> instrument, Observation observation, T subject) =>
            throw new NotImplementedException();

        public static void Record<T>(this IInstrument<T> instrument, Exception exception, T subject) =>
            throw new NotImplementedException();

        public static void Record<T1, T2>(this IInstrument<T1, T2> instrument, T1 subject1, T2 subject2) =>
            throw new NotImplementedException();

        public static void Record<T1, T2>(this IInstrument<T1, T2> instrument, Exception exception, T1 subject1) =>
            throw new NotImplementedException();

        public static void Record<T1, T2>(this IInstrument<T1, T2> instrument, Observation observation, T1 subject1, T2 subject2) =>
            throw new NotImplementedException();
    }
}
