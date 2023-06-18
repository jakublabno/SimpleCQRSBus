namespace CQRSBus.CommandBus.Specialized;

[AttributeUsage(AttributeTargets.Class)]
public sealed class Handler : Attribute
{
    public readonly Type? HandlerClass;

    public Handler(Type? handler = null)
    {
        HandlerClass = handler;
    }
}
