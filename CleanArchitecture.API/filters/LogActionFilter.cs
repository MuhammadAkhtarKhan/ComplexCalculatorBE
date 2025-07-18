using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Text.Json;

namespace ComplexCalculator.API.filters
{
    public sealed class LogActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
            => _logger = logger;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // ---------- before -------------
            var stopwatch = Stopwatch.StartNew();

            var actionName = context.ActionDescriptor.DisplayName;
            var controller = context.RouteData.Values["controller"];
            var arguments = JsonSerializer.Serialize(context.ActionArguments);

            _logger.LogInformation("➡️ {Controller}.{Action} started. Args: {Args}",
                                   controller, actionName, arguments);

            // ---------- execute -----------
            var executedContext = await next();

            // ---------- after ------------
            stopwatch.Stop();

            var resultType = executedContext.Result?.GetType().Name ?? "No result";
            var statusCode = executedContext.HttpContext.Response?.StatusCode;

            if (executedContext.Exception is null)
            {
                _logger.LogInformation("✅ {Controller}.{Action} finished in {Elapsed} ms, Status {Status}, Result {Type}",
                                       controller, actionName, stopwatch.ElapsedMilliseconds, statusCode, resultType);
            }
            else
            {
                _logger.LogError(executedContext.Exception,
                                 "❌ {Controller}.{Action} threw after {Elapsed} ms",
                                 controller, actionName, stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
