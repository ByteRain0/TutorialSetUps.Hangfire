using Hangfire;
using MediatR;

namespace HangfirePOC.Infrastructure
{
    public class HangFireDispatcher : IMessageDispatcher
    {
        private readonly ILogger<HangFireDispatcher> _logger;

        public HangFireDispatcher(ILogger<HangFireDispatcher> logger)
        {
            _logger = logger;
        }

        public void Dispatch(string jobIdentifier, IRequest request)
        {
            try
            {
                var backgroundJobClient = new BackgroundJobClient();
                backgroundJobClient.Enqueue<MediatRBridge>(x => x.Send(jobIdentifier, request));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, e.Message);
            }
        }

        public void Dispatch<T>(string jobIdentifier, IRequest<T> request)
        {
            try
            {
                var backgroundJobClient = new BackgroundJobClient();
                backgroundJobClient.Enqueue<MediatRBridge>(x => x.Send(jobIdentifier, request));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, e.Message);
            }
        }
    }
}
