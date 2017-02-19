namespace Skogsaas.Monolith.Bindings
{
    public interface IBinding : Configuration.IIdentifier
    {
        string FromChannel { get; set; }
        string FromObject { get; set; }
        string FromProperty { get; set; } // Property path build up with the delimiter "/".

        string ToChannel { get; set; } 
        string ToObject { get; set; }
        string ToProperty { get; set; } // Property path build up with the delimiter "/".
    }
}
