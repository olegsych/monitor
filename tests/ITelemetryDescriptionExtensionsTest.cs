using System;
using System.Linq.Expressions;
using Fuzzy;
using NSubstitute;
using Xunit;

namespace Monitor
{
    public class ITelemetryDescriptionExtensionsTest: TestFixture
    {
        readonly ITelemetryDescription<TestSubject> description = Substitute.For<ITelemetryDescription<TestSubject>>();

        public class TestSubject
        {
            public TestValue TestProperty { get; set; } = new TestValue();
        }

        public class TestValue
        {
            public int value = fuzzy.Int32();
        }

        public class AddProperty: ITelemetryDescriptionExtensionsTest
        {
            [Fact]
            public void ThrowsDescriptiveExceptionWhenDescriptionIsNull() {
                ITelemetryDescription<TestSubject>? @null = null;
                var thrown = Assert.Throws<ArgumentNullException>(() => @null!.AddProperty(subject => subject.TestProperty));
            }

            [Fact]
            public void ThrowsDescriptiveExceptionWhenExpressionIsNull() {
                Expression<Func<TestSubject, TestValue>>? @null = null;
                var thrown = Assert.Throws<ArgumentNullException>(() => description.AddProperty(@null!));
            }

            [Fact]
            public void AddsPropertyWithNameDerivedFromExpression() {
                string? actual = null;
                description.AddProperty(Arg.Do<string>(_ => actual = _), Arg.Any<Func<TestSubject, TestValue>>());

                description.AddProperty(subject => subject.TestProperty);

                Assert.Equal(nameof(TestSubject.TestProperty), actual);
            }

            [Fact]
            public void AddsPropertyWithGetterCompiledFromExpression() {
                Func<TestSubject, TestValue>? actual = null;
                description.AddProperty(Arg.Any<string>(), Arg.Do<Func<TestSubject, TestValue>>(_ => actual = _));

                description.AddProperty(subject => subject.TestProperty);

                var expected = new TestSubject();
                Assert.Equal(expected.TestProperty.value, actual!(expected).value);
            }
        }

        public class AddMeasurement: ITelemetryDescriptionExtensionsTest
        {
            [Fact]
            public void ThrowsDescriptiveExceptionWhenDescriptionIsNull() {
                ITelemetryDescription<TestSubject>? @null = null;
                var thrown = Assert.Throws<ArgumentNullException>(() => @null!.AddMeasurement(subject => subject.TestProperty));
            }

            [Fact]
            public void ThrowsDescriptiveExceptionWhenExpressionIsNull() {
                Expression<Func<TestSubject, TestValue>>? @null = null;
                var thrown = Assert.Throws<ArgumentNullException>(() => description.AddMeasurement(@null!));
            }

            [Fact]
            public void AddsMeasurementWithNameDerivedFromExpression() {
                string? actual = null;
                description.AddMeasurement(Arg.Do<string>(_ => actual = _), Arg.Any<Func<TestSubject, TestValue>>());

                description.AddMeasurement(subject => subject.TestProperty);

                Assert.Equal(nameof(TestSubject.TestProperty), actual);
            }

            [Fact]
            public void AddsMeasurementWithGetterCompiledFromExpression() {
                Func<TestSubject, TestValue>? actual = null;
                description.AddMeasurement(Arg.Any<string>(), Arg.Do<Func<TestSubject, TestValue>>(_ => actual = _));

                description.AddMeasurement(subject => subject.TestProperty);

                var expected = new TestSubject();
                Assert.Equal(expected.TestProperty.value, actual!(expected).value);
            }
        }

    }
}
