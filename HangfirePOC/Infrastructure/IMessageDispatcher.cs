using MediatR;

namespace HangfirePOC.Infrastructure
{
    public interface IMessageDispatcher
    {
        void Dispatch(string jobIdentifier, IRequest request);

        void Dispatch<T>(string jobIdentifier, IRequest<T> request);
    }
}
