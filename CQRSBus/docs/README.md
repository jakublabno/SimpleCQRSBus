# SimpleCQRSBus

Simple CQRS bus for .NET based applications

## Configuration using builder

```csharp
//Configure middleware implementing IMiddleware e.g. LogginMiddleware
var loggingMiddleware = new LoggingMiddleware(NullLogger.Instance);

//Inistantiate bus instance, you can set your own locator/middlewares using relevant builder's methods
var commandBus = new CommandBusBuilder()
    .AddDefaultHandlerLocator()
    .AddDefaultHandlerFactory()
    .AddMiddleware(loggingMiddleware)
    .AddExecutionMiddleware()
    .Build();
```

## Or configure on your own
```csharp

//Initilize middleware collection <MiddlewareCollection>
var middlewareCollection = new CommandBusMiddlewareCollection();

//Configure middleware implementing IMiddleware e.g. LogginMiddleware
var loggingMiddleware = new LoggingMiddleware(NullLogger.Instance);

//Build default locators or create own implementation of <IHandlerLocator> and <IHandlerCreator>
var handlerNameLocator = new AdditionInflectionStrategy("Handler");
var handlerLocator = new NameInflectorLocator(handlerNameLocator);

//Create default handler activator or create own e.g. using dependency container
var handlerActivator = new SimpleHandlerActivator();

//Configure execution middleware
var executionMiddleware = new ExecutionMiddleware(handlerLocator, handlerActivator);

//Add middlewares to collection
middlewareCollection.AddLast(loggingMiddleware);
middlewareCollection.AddLast(executionMiddleware);

//Inistantiate bus instance
var commandBus = new CommandBus(middlewareCollection);
```

## Basic usage
```csharp
//Command class
public class SampleCommand : ICommand
{
    public string Input;

    public SampleCommand(string input)
    {
        Input = input;
    }
}

//Handler class
public class SampleCommandHandler : ICommandHandler<SampleCommand, string>
{
    public string Handle(SampleCommand message)
    {
        return message.Input;
    }
}

//Handling with void return type
var command = new SampleCommand("any");
commandBus.Handle(command);

//Handling with desired return type
var input = "fancy input";
var command = new SampleCommand(input);
var result = commandBus.Handle<string>(command); //fancy input
```

## Sample middleware
You can use middleware to manage transactions, logging, setting firewalls, validating command/queries etc.
```csharp
public class ChainableMiddleware : ICommandBusMiddleware
{
    public dynamic? Execute(ICommand message, Func<IMessage, dynamic>? callable)
    {
        //put some logic here

        var del = (IMessage message) => callable(message);

        return del;
    }
}
```

## Upcoming
Bus for events
