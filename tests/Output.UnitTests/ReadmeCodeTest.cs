using Notim.Outputs;

namespace Outputs.UnitTests;

public record ClassThatYouNeedToTransport(object argsThenYouNeed)
{ }

public class ReadmeCodeTest
{

    public Output<ClassThatYouNeedToTransport> ReadmeSnippet01Test()
    {
        var output = new Notim.Outputs.Output<ClassThatYouNeedToTransport>();

        var something = false;
        
        if (something is not Good) {
            output.AddFaultMessage("something is not good");
        }
        else{
            output.AddMessage("Success");
            
            var argsThenYouNeed = new object();
            
            output.AddResult(new ClassThatYouNeedToTransport(argsThenYouNeed));
        }

        return output;
    }
    
    public Output<ClassThatYouNeedToTransport> ReadmwSnippet02Test()
    {
        var output = new Output<ClassThatYouNeedToTransport>();
        
        var something = false;
        var argsThenYouNeed = new object();

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
    }
    
    public interface IFindUserByIdUseCase
    {

        Output<ClassThatYouNeedToTransport> Handle(object input, CancellationToken cancellationToken);

    }
    
    public class FindUserByIdUseCase : IFindUserByIdUseCase
    {
        public Output<ClassThatYouNeedToTransport> Handle(object input, CancellationToken cancellationToken)
        {
            return new Output<ClassThatYouNeedToTransport>();
        }
    }
    
    public void ReadmeSnipopet03Test()
    {
        /*
        IFindUserByIdUseCase _findUserByIdUseCase = new FindUserByIdUseCase();
        object input = new object();
        CancellationToken cancelationToken = new CancellationToken();
        
        Output<ClassThatYouNeedToTransport> output = _findUserByIdUseCase.Handle(input, cancelationToken);
  
        if (!output.IsValid && output.Fault?.FaultType  is FaultType.ExternalServiceUnavailable)
            return BadGateway(output.Fault);
  
        if (!output.IsValid && output.Fault?.FaultType is FaultType.ResourceNotFound)
            return NotFound(output.Fault);

        if (!output.IsValid && output.Fault?.FaultType is FaultType.InvalidInput)
            return UnprocessableEntity(output.Fault);

        if (!output.IsValid && output.Fault?.FaultType is FaultType.InvalidOperation)
            return UnprocessableEntity(output.Fault);
  
        return Ok(output.GetResult());
        */
    }

    private const bool Good = true;

    private const bool externalServiceIsOffline = true;
    private const bool cannotFindResultOnDatabase = true;
    private const bool invalidInputReceived = true;

}
