using System;

namespace Athene.Monitor
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
            public void Record(Observation observation = default, Exception? exception = null, Subject? subject = null) => throw new NotImplementedException();
            public Observation Start() => throw new NotImplementedException();
        }

    }
}
