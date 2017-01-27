using Skogsaas.Legion;

namespace Skogsaas.Monolith.Configuration
{
    public interface Identifier : IObject
    {
        string Type { get; set; }
        string Plugin { get; set; }
    }
}
