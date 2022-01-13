using Fuzzy;

namespace Athene.Monitor
{
    public abstract class TestFixture
    {
        protected static readonly IFuzz fuzzy = new RandomFuzz();
    }
}
