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
            public string? Foo;
            public int Bar;
            public EnumSubject Baz;
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
