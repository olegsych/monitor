using System;
using System.Linq.Expressions;

namespace Monitor
{
    public static class ITelemetryDescriptionExtensions
    {
        public static void AddProperty<TSubject, TResult>(this ITelemetryDescription<TSubject> description, Expression<Func<TSubject, TResult>> getter) {
            Require(description, getter);
            description.AddProperty(Name(getter), getter.Compile());
        }

        public static void AddMeasurement<TSubject, TResult>(this ITelemetryDescription<TSubject> description, Expression<Func<TSubject, TResult>> getter) {
            Require(description, getter);
            description.AddMeasurement(Name(getter), getter.Compile());
        }

        static void Require<TSubject, TResult>(ITelemetryDescription<TSubject> description, Expression<Func<TSubject, TResult>> getter) {
            if(description is null)
                throw new ArgumentNullException(nameof(description));
            if(getter is null)
                throw new ArgumentNullException(nameof(getter));
        }

        static string Name<TSubject, TResult>(Expression<Func<TSubject, TResult>> getter)
            => ((MemberExpression)getter.Body).Member.Name;
    }
}
