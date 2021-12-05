using System;

namespace Monitor
{
    /// <summary>
    /// Tells <see cref="Monitor"/> that the member should be recorded as a property.
    /// </summary>
    /// <remarks>
    /// By default <see cref="Monitor"/> will record values of public properties and fields.
    /// Values of non-public properties and fields, as well as return values of methods can
    /// be recorded in telemetry when marked with this attribute.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class TelemetryPropertyAttribute: Attribute { }

    /// <summary>
    /// Tells <see cref="Monitor"/> that the member should be recorded as a metric.
    /// </summary>
    /// <remarks>
    /// By default <see cref="Monitor"/> will record values of public properties and field of
    /// integer types as metrics. Values of non-public properties and fields, as well as return
    /// values of methods can be recorded as metrics when marked with this attribute.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class TelemetryMetricAttribute: Attribute { }

    /// <summary>
    /// Tells <see cref="Monitor"/> that the member should be recorded as a dimension.
    /// </summary>
    /// <remarks>
    /// <see cref="Monitor"/> can't tell if a particular value is of low enough cardinality
    /// to be appropriate for use as a metric dimension. Because metric aggregation requires
    /// memory allocation for each distinct combination of dimension values, dimensions must
    /// be designated explicitly.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class TelemetryDimensionAttribute: Attribute { }
}
