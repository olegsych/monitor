namespace Monitor
{
    class TelemetryDescriptorExample
    {
        enum EnumSubject
        {
            FooItem,
            BarItem,
            BazItem
        }

        class Subject
        {
            public string? Foo = default;
            public int Bar = default;
            public EnumSubject Baz = default;
        }

        class TelemetryDescriptor: ITelemetryDescriptor<Subject>
        {
            public void Describe(ITelemetryDescription<Subject> description) {
                description.AddProperty(_ => _.Foo);
                description.AddMeasurement(_ => _.Bar);
                description.AddDimension(_ => _.Baz);
            }
        }
    }
}
