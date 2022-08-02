namespace HangfirePOC.Business.SimpleService
{
    public class SimpleService : ISimpleService
    {
        private readonly ILogger<SimpleService> _logger;

        public SimpleService(ILogger<SimpleService> logger)
        {
            _logger = logger;
        }

        public void DoWork()
        {
            _logger.LogInformation("Execution successful");
        }
    }
}
