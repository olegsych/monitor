using System;

namespace Athene.Monitor
{
    /// <summary>
    /// Creates an <see cref="IMeasurement0D{T}"/> for a given metric with 0 custom dimensions.
    /// </summary>
    public interface IMeasurementFactory0D<T>
    {
        IMeasurement0D<T> Create(Type subject, string metricSuffix);
        IMeasurement0D<T> Create(string metricName);
    }

    /// <summary>
    /// Creates an <see cref="IMeasurement1D{T}"/> for a given metric with 1 custom dimension.
    /// </summary>
    public interface IMeasurementFactory1D<T>
    {
        IMeasurement1D<T> Create(Type subject, string metricSuffix, string dimensionName);
        IMeasurement1D<T> Create(string metricName, string dimensionName);
    }

    /// <summary>
    /// Creates an <see cref="IMeasurement2D{T}"/> for a given metric with 2 custom dimensions.
    /// </summary>
    public interface IMeasurementFactory2D<T>
    {
        IMeasurement2D<T> Create(Type subject, string metricSuffix, string dimensionName1, string dimensionName2);
        IMeasurement2D<T> Create(string metricName, string dimensionName1, string dimensionName2);
    }
}
