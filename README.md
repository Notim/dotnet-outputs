# Notim.Outputs

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Tests passed](https://github.com/Notim/dotnet-outputs/actions/workflows/dotnet.yml/badge.svg?event=push)
![NuGet Downloads](https://img.shields.io/nuget/dt/Notim.Outputs.svg)

This is a Basic Wrapper for use case Outputs, commom used with Mediatr Output or CQRS Patterns

you can use in your project by [nuget.org](https://www.nuget.org/packages/Notim.Outputs/)

### you can install with nuget package manager
```shell
dotnet add package Notim.Outputs --version 3.0.0
```

### Simple Usage
the usage is very simple, you only need to instace Output with classe that you need to transport.

```csharp
var output = new Output<ClassThatYouNeedToTransport>();

if (something is not Good) {
  output.AddFaultMessage("something is not good");
}
else{
  output.AddMessage("Success");
  output.AddResult(new ClassThatYouNeedToTransport(argsThenYouNeed));
}

return output;
```

### Faults treatment by Fault type
you have the functionality to determine the Fault type to filter if you want to return a especific status code or retries on your topic consumers

```csharp
var output = new Output<ClassThatYouNeedToTransport>();

if (something is not Good && externalServiceIsOffline)
{
  output.AddFault(new Fault(FaultType.ExternalServiceUnavailable, "customer service is down"));
}
else if (something is not Good && cannotFindResultOnDatabase)
{
  output.AddFault(new Fault(FaultType.ResourceNotFound, "the user with id xxx cannot be find"));
}
else if (something is not Good && invalidInputReceived)
{
  output.AddFault(new Fault(FaultType.InvalidInput, "invalid fields"));
}
else
{
  output.AddMessage("Success");
  output.AddResult(new ClassThatYouNeedToTransport(argsThenYouNeed));
}

return output;
```

Asp .Net WebApi Controller using "use case" pattern example:

```csharp
[HttpGet("/")]
public async Task<IActionResult> GetOrder(ClassThatYouReceiveDataInput input, CancelationToken cancelationToken)

    Output<ClassThatYouNeedToTransport> output = await _findUserByIdUseCase.Handle(input, cancelationToken);
    
    if (!output.IsValid && output.Fault?.FaultType  is FaultType.ExternalServiceUnavailable)
        return BadGateway(output.Fault);
    
    if (!output.IsValid && output.Fault?.FaultType is FaultType.ResourceNotFound)
        return NotFound(output.Fault);
    
    if (!output.IsValid && output.Fault?.FaultType is FaultType.InvalidInput)
        return UnprocessableEntity(output.Fault);
    
    if (!output.IsValid && output.Fault?.FaultType is FaultType.InvalidOperation)
        return UnprocessableEntity(output.Fault);
    
    return Ok(output.GetResult());
}
```

### Single Line Fluent Form to build Output
#### After version 2 you can use the single line Build Form to create Output

success use case output
```csharp
var output = Output<ClassThatYouNeedToTransport>.WithSuccess("use case finished with success", new ClassThatYouNeedToTransport());
```

Fault use case output
```csharp
var output = Output<ClassThatYouNeedToTransport>.WithFault("An Fault was ocurred");
```

Fault object use case output
```csharp
var output = Output<ClassThatYouNeedToTransport>.WithFault(new Fault(FaultType.ExternalServiceUnavailable, "external service is unavaiable"));
```

### Empty body can be returned
#### After version 3 you can use the Output with empty body to create Output

```csharp
var output = Output();
output.AddFault("simple error");
```

```csharp
var output = Output();
output.AddFault(new Fault(ErrorType.ExternalServiceUnavailable), "external error");
```



# Notim.Outputs.FluentValidation

you can use FluentValidation library "ValidationResult" to create an Output with FaultType.InvalidInput, you just only need to install the extension package:

[nuget.org](https://www.nuget.org/packages/Notim.Outputs.FluentValidation/)

### you can install Notim.Outputs.FluentValidation with nuget package manager, run the command below:
```shell
dotnet add package Notim.Outputs.FluentValidation --version 3.0.0
```

### simple usage
The usage is very simple:
```csharp
IValidator<SomeClass> validator = new SomeClassValidator<SomeClass>();

var validationResult = validator.Validate(someObject);

var output = new Output<SomeClassThatUseToTransport>();

output.AddValidationResult(validationResult);
```

# Contribution

If you would like to contribute to this project, follow these steps:

2. Create a branch for your changes (`git checkout -b feature/MyFeature`)
3. Commit your changes (`git commit -am 'feat: add a feature'`)
4. Push to the branch (`git push origin feature/MyFeature`)
5. Open a Pull Request

some issues you can talk with me [paulino.joaovitor@yahoo.com.br](mailto:paulino.joaovitor@yahoo.com.br)
