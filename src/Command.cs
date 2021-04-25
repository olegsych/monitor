using System.Reflection;

namespace Monitor
{
    public class Command: Operation
    {
        public Command(MethodBase method): base(method) { }
    }

    public class Command<TInput>: Command
    {
        public Command(MethodBase method): base(method) { }
    }
}
