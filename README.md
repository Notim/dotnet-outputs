# Notim.Outputs

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![example event parameter](https://github.com/Notim/dotnet-outputs/actions/workflows/dotnet.yml/badge.svg?event=push)

This is a Basic Wrapper for use case Outputs, commom used with Mediatr Output or CQRS Patterns

you can use in your project by [nuget.org](https://www.nuget.org/packages/Notim.Outputs/)

### you can install with nuget package manager
```shell
dotnet add package Notim.Outputs --version 2.0.1
```
### Simple Usage
the usage is very simple, you only need to instace Output with classe that you need to transport.

```csharp
var output = new Output<ClassThatYouNeedToTransport>();

if (something is notGood) {
  output.AddErrorMessage("something is not good");
}
else{
  output.AddMessage("Success");
  output.AddResult(new ClassThatYouNeedToTransport(argsThenYouNeed));
}

return output;
```

### Errors treatment by error type
you have the functionality to determine the error type to filter if you want to return a especific status code or retries on your topic consumers

```csharp
var output = new Output<ClassThatYouNeedToTransport>();

if (something is notGood && externalServiceIsOffline)
{
  output.AddError(new Error(ErrorType.ExternalServiceUnavailable, "customer service is down"));
}
else if (something is notGood && cannotFindResultOnDatabase)
{
  output.AddError(new Error(ErrorType.ResourceNotFound, "the user with id xxx cannot be find"));
}
else if (something is notGood && invalidInputReceived)
{
  output.AddError(new Error(ErrorType.InvalidInput, "invalid fields"));
}
else
{
  output.AddMessage("Success");
  output.AddResult(new ClassThatYouNeedToTransport(argsThenYouNeed));
}

return output;
```

controller using "use case" pattern:

```csharp
[HttpGet("/")]
public async Task<IActionResult> GetOrder(ClassThatYouReceiveDataInput input, CancelationToken cancelationToken)

  Output<ClassThatYouNeedToTransport> output = await _findUserByIdUseCase.Handle(input, cancelationToken);
  
  if (!output.IsValid && output.Error.ErrorType is ErrorType.ExternalServiceUnavailable)
    return BadGateway(output.ErrorMessages);
  
  if (!output.IsValid && output.Error.ErrorType is ErrorType.ResourceNotFound)
    return NotFound(output.ErrorMessages);

  if (!output.IsValid && output.Error.ErrorType is ErrorType.InvalidInput)
    return UnprocessableEntity(output.ErrorMessages);
  
  return Ok(output.GetResult());
}
```

### Single Line Fluent Form to build Output
After version 2 you can use the single line Build Form to create Output

success use case output
```csharp
var output = Output<ClassThatYouNeedToTransport>.WithSuccess("use case finished with success", new ClassThatYouNeedToTransport());
```

error use case output
```csharp
var output = Output<ClassThatYouNeedToTransport>.WithError("An error was ocurred");
```

error object use case output
```csharp
var output = Output<ClassThatYouNeedToTransport>.WithError(new Error(ErrorType.ExternalServiceUnavailable, "external service is unavaiable"));
```

# Notim.Outputs.FluentValidation

you can use FluentValidation library "ValidationResult" to create an Output with ErrorType.InvalidInput, you just only need to install the extension package:

[nuget.org](https://www.nuget.org/packages/Notim.Outputs.FluentValidation/)

### you can install Notim.Outputs.FluentValidation with nuget package manager, run the command below:
```shell
dotnet add package Notim.Outputs.FluentValidation --version 2.0.1
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
