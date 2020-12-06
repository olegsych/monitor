using Fuzzy;

namespace Monitor
{
    public abstract class TestFixture
    {
        protected static readonly IFuzz fuzzy = new RandomFuzz();
    }
}
