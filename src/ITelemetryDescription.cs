using System;
using System.Linq.Expressions;

namespace Monitor
{
    public interface ITelemetryDescription<TSubject>
    {
        void AddProperty<TValue>(Expression<Func<TSubject, TValue>> getter);
        void AddMeasurement<TValue>(Expression<Func<TSubject, TValue>> getter);
        void AddDimension<TValue>(Expression<Func<TSubject, TValue>> getter);
    }
}
