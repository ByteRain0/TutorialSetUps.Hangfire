using MediatR;

namespace HangfirePOC.Business.SimpleCommand
{
    public class SimpleCommandHandler : IRequestHandler<SimpleCommand>
    {
        public Task<Unit> Handle(SimpleCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Executed Successfully");
            return Task.FromResult(Unit.Value);
        }
    }
}
