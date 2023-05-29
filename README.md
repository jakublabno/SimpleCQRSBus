# SimpleCQRSBus

Simple CQRS bus for .NET based applications

## Limitations
For now, library supports only commands.

## Configuration

```csharp

//Initilize middleware collection <MiddlewareCollection>
var middlewareCollection = new CommandBusMiddlewareCollection();

//Configure middleware implementing IMiddleware e.g. LogginMiddleware
var logginMiddleware = new LoggingMiddleware(NullLogger.Instance);

//Build default locators or create own implementation of <IHandlerLocator> and <IHandlerCreator>
var handlerNameLocator = new AdditionInflectionStrategy("Handler");
var handlerLocator = new NameInflectorLocator(handlerNameLocator);

//Create default handler activator or create own e.g. using dependency container
var handlerActivator = new SimpleHandlerActivator();

//Configure execution middleware
var executionMiddleware = new ExecutionMiddleware(handlerLocator, handlerActivator);

//Add middlewares to collection
middlewareCollection.AddLast(logginMiddleware);
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
```csharp
public class ChainableMiddleware : ICommandBusMiddleware
{
    public bool Executed;

    public dynamic? Execute(ICommand message, Func<IMessage, dynamic>? callable)
    {
        //put some logic here

        var del = (IMessage message) => callable(message);

        return del;
    }
}
```
