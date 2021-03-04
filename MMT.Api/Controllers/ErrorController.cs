using MMT.Domain.Common.Exceptions;
using MMT.Domain.Common.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Api
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {

        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("error")]
        public async Task<ErrorModel> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var code = 500;

            if (exception is EntityNotFoundException) code = 404;

            _logger.LogError($"Exception code {code} - {exception.Message} - {exception.StackTrace}");

            var error = new ErrorModel(exception);
            Response.StatusCode = code;

            var msg = (code >= 500) ? "" : exception.Message;

            byte[] data = Encoding.UTF8.GetBytes(msg);
            Response.ContentType = "application/json";
            await Response.Body.WriteAsync(data, 0, data.Length);

            return error;
        }
    }
}
