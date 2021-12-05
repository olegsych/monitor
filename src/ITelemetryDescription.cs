using System;
using System.Linq.Expressions;

namespace Monitor
{
    /// <summary>
    /// Collects descriptinos of the <typeparamref name="TSubject"/> telemetry.
    /// </summary>
    /// <typeparam name="TSubject">
    /// Type whose telemetry is being described.
    /// </typeparam>
    public interface ITelemetryDescription<TSubject>
    {
        void AddProperty<TValue>(Expression<Func<TSubject, TValue>> getter);
        void AddMeasurement<TValue>(Expression<Func<TSubject, TValue>> getter);
        void AddDimension<TValue>(Expression<Func<TSubject, TValue>> getter);
    }
}
