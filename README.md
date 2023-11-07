# Outputs

This is a Basic Wrapper for use case Outputs, commom used with Mediatr Output or CQRS Patterns

get in [nuget](https://www.nuget.org/packages/Notim.Outputs/)

you can install with nuget package manager
```shell
dotnet add package Notim.Outputs
```

the usage is very simple, you only need to instace Output with classe that you need to transport.
```csharp
[...]

var output = new Output<ClassThatYouNeedToTransport>();

if (something is notGood) {
  output.AddErrorMessage("something is not good");
}
else{
  output.AddMessage("Success");
  output.AddResult(new ClassThatYouNeedToTransport(argsThenYouNeed));
}

return output;

[...]
```

you have the functionality to determine the error type to filter if you want to return a especific status code or retries on your topic consumers;

example:

```csharp
[...]

var output = new Output<ClassThatYouNeedToTransport>();

if (something is notGood because external service is offline)
{
  output.AddError(new Error(ErrorType.ExternalServiceUnavailable, "something is not good"));
}
else if (something is notGood because you dont find result on database)
{
  output.AddError(new Error(ErrorType.ResourceNotFound, "something is not good"));
}
else
{
  output.AddMessage("Success");
  output.AddResult(new ClassThatYouNeedToTransport(argsThenYouNeed));
}

return output;
[...]

```

controller:

```csharp
[HttpGet("/")]
public async Task<IActionResult> GetOrder(ClassThatYouNeedToTransport input, CancelationToken cancelationToken)

  var output = await _usecase.Execute(input, cancelationToken);
  
  if (!output.IsValid && output.Error.ErrorType is ErrorType.ExternalServiceUnavailable)
    return BadGateway(output.ErrorMessages);
  
  if (!output.IsValid && output.Error.ErrorType is ErrorType.ResourceNotFound)
    return NotFound(output.ErrorMessages);
  
  return Ok(output.GetResult());
}
```

After version 2 you can use the single line Build Form to create Output

success use case output
```csharp
var output = Output<ObjectThatYouWantToReturn>.WithSuccess(message, new ObjectThatYouWantToReturn());
```

error use case output
```csharp
var output = Output<ObjectThatYouWantToReturn>.WithError(errorMessage);
```

error object use case output
```csharp
var output = Output<ObjectThatYouWantToReturn>.WithError(new Error(ErrorType.ExternalServiceUnavailable, errorMessage));
```

## Contribution

If you would like to contribute to this project, follow these steps:

2. Create a branch for your changes (`git checkout -b feature/MyFeature`)
3. Commit your changes (`git commit -am 'feat: add a feature'`)
4. Push to the branch (`git push origin feature/MyFeature`)
5. Open a Pull Request

some issues you can talk with me [paulino.joaovitor@yahoo.com.br](mailto:paulino.joaovitor@yahoo.com.br)