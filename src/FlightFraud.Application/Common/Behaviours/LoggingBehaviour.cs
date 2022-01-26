using FightFraud.Application.Common.Interfaces;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FightFraud.Application.Common.Behaviours
{

    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(
            ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            var requestGuid = Guid.NewGuid().ToString();

            var requestNameWithGuid = $"{requestName} [{requestGuid}]";

            _logger.LogInformation("[START] {Name}", requestNameWithGuid);
            TResponse response;

            var stopwatch = Stopwatch.StartNew();
            try
            {
                try
                {
                    _logger.LogInformation("[PROPS] {Name} {@RequestContent}", requestNameWithGuid, JsonSerializer.Serialize(request));
                }
                catch (NotSupportedException)
                {
                    _logger.LogInformation("[Serialization ERROR] {Name} Could not serialize the request.", requestNameWithGuid);
                }

                response = await next();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation(
                    "[END] {Name}; Execution time={stopwatch.ElapsedMilliseconds}ms", requestNameWithGuid);
            }

            return response;
        }

    }
}
