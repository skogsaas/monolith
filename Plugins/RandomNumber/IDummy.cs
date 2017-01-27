using Skogsaas.Legion;

namespace Skogsaas.Monolith.Plugins.RandomNumber
{
    public interface IDummy : IObject
    {
        double Value { get; set; }
    }
}
