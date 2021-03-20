using System;

namespace Monitor
{
    public interface IObservation: IDisposable
    {
        void Finish(Exception e);
    }

    public interface IObservation<in TOutput>: IObservation
    {
        void Finish(TOutput output);
    }
}
