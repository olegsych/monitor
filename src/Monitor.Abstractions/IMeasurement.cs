using System;
using Chronology;

namespace Athene.Monitor
{
    /// <summary>
    /// Measures a sample value of type <typeparamref name="T"/> with 0 custom dimensions.
    /// Use <see cref="IMeasurementFactory0D{T}"/> to create.
    /// </summary>
    /// <remarks>
    /// This interface derives from <see cref="IDisposable"/> because implementations aggregating
    /// samples in memory need a reliable notification to publish metrics at the end of their lifetime.
    /// </remarks>
    public interface IMeasurement0D<T>: IDisposable
    {
        void Measure(T value);
        void Measure(UtcDateTime timestamp, T value);
    }

    /// <summary>
    /// Measures a sample value of type <typeparamref name="T"/> with 1 custom dimension.
    /// Use <see cref="IMeasurementFactory1D{T}"/> to create.
    /// </summary>
    /// <remarks>
    /// This interface derives from <see cref="IDisposable"/> because implementations aggregating
    /// samples in memory need a reliable notification to publish metrics at the end of their lifetime.
    /// </remarks>
    public interface IMeasurement1D<T>: IDisposable
    {
        void Measure(T value, string dimensionValue);
        void Measure(UtcDateTime timestamp, T value, string dimensionsValue);
    }

    /// <summary>
    /// Measures a sample value of type <typeparamref name="T"/> with 1 custom dimension.
    /// Use <see cref="IMeasurementFactory1D{T}"/> to create.
    /// </summary>
    /// <remarks>
    /// This interface derives from <see cref="IDisposable"/> because implementations aggregating
    /// samples in memory need a reliable notification to publish metrics at the end of their lifetime.
    /// </remarks>
    public interface IMeasurement2D<T>: IDisposable
    {
        void Measure(T value, string dimensionValue1, string dimensionValue2);
        void Measure(UtcDateTime timestamp, T value, string dimensionsValue1, string dimensionValue2);
    }
}
