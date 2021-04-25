using System;

namespace Monitor
{
    public interface ITelemetryDescription<TSubject>
    {
        void AddProperty<TValue>(string name, Func<TSubject, TValue> getter);
        void AddMeasurement<TValue>(string name, Func<TSubject, TValue> getter);
        void AddDimension<TValue>(string name, Func<TSubject, TValue> getter);
    }
}
