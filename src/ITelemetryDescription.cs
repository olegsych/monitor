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
        /// <summary>
        /// Adds a property to telemetry of <typeparamref name="TSubject"/>.
        /// Properties can be of any type convertible to string for logging.
        /// </summary>
        void AddProperty<TValue>(Expression<Func<TSubject, TValue>> getter);

        /// <summary>
        /// Adds a metric to telemetry of <typeparamref name="TSubject"/>.
        /// Metrics must be convertible to integer for aggregation.
        /// </summary>
        void AddMetric<TValue>(Expression<Func<TSubject, TValue>> getter);

        /// <summary>
        /// Adds a dimension to telemetry of <typeparamref name="TSubject"/>.
        /// Dimensions are low-cardinality properties used for metric aggregation.
        /// </summary>
        void AddDimension<TValue>(Expression<Func<TSubject, TValue>> getter);
    }
}
