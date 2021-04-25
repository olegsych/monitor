using System;
using System.Reflection;

namespace Monitor
{
    public class Operation: IEquatable<Operation>
    {
        public Operation(MethodBase method) => Method = method;
        public MethodBase Method { get;  }
        public override bool Equals(object other) => (other as Operation)?.Equals(this) ?? false;
        public bool Equals(Operation other) => Method?.Equals(other.Method) ?? false;
    }
}
