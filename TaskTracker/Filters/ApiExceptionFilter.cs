using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskTracker.Logic.Exceptions;

namespace TaskTracker.Filters
{
    public class ApiExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            switch (context.Exception.GetType().Name)
            {
                case nameof(TaskNameIsNullException):
                    _logger.LogError("Название задачи null или пустое.");
                    context.Result = new StatusCodeResult(400);
                    break;
                default:
                    _logger.LogError(context.Exception.Message, context.Exception, context.HttpContext.Response.StatusCode);
                    context.Result = new StatusCodeResult(500);
                    break;
            }
        }
    }
}
