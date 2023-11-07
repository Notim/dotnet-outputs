# Outputs

this is a Basic wrapper for use case Outputs, commom used with Mediatr Output

get in [nuget](https://www.nuget.org/packages/Notim.Outputs/)

you can install with nuget package manager
```śhell
dotnet add package Notim.Outputs --version 1.0.2
```

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

you have the functionality to determine the error type to filter if you want to return a especific status code or retries on your topic consumers;

example:

```csharp
...

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
...

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

some issues you can talk with me paulino.joaovitor@yahoo.com.br
