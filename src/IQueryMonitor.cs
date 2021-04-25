using System;

namespace Monitor
{
    public interface IQueryMonitor<in TOutput>: IDisposable
    {
        void Observe(TOutput output);
        void Observe(Exception e);
        IObservation<TOutput> Start();
    }
}
