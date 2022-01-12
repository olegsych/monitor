using System;

namespace Monitor
{
    class InstrumentExample
    {
        class Subject
        {
            public int Foo;
            public string? Bar;
            public Color Color;
        }

        enum Color
        {
            Red, Green, Blue
        }

        class SubjectInstrument: IInstrument<Subject>
        {
            public void Record(Measurement measurement = default, Exception? exception = null, Subject? subject = null) => throw new NotImplementedException();
            public Measurement Start() => throw new NotImplementedException();
        }

    }
}
