using Fuzzy;

namespace Monitor
{
    /// <summary>
    /// A base class for other examples.
    /// </summary>
    public abstract class Example
    {
        protected static readonly IFuzz fuzzy = new RandomFuzz();
    }
}
