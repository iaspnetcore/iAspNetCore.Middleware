/* history
 * 
 * 
 * update to netcore 2.2.0 by freeman 20190309
 */

using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft. AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Extensions;
using System;

namespace iAspNetcore.Middleware.LogRequest
{

    /// <summary>
    /// http://www.sulhome.com/blog/10/log-asp-net-core-request-and-response-using-middleware
    /// </summary>
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LogRequestMiddleware> _logger;
        private Func<string, Exception, string> _defaultFormatter = (state, exception) => state;

        public LogRequestMiddleware(RequestDelegate next, ILogger<LogRequestMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var url = UriHelper.GetDisplayUrl(context.Request);

            // _logger.LogInformation($"\n{DateTime.Now.ToString()}-Request url: {url},\nRequest Method: {context.Request.Method},Request Schem: {context.Request.Scheme}, UserAgent: {context.Request.Headers[HeaderNames.UserAgent].ToString()}");

            string requestUrlString = $"Request url: {url},Request Method: {context.Request.Method},Request Schem: {context.Request.Scheme}, UserAgent: {context.Request.Headers[HeaderNames.UserAgent].ToString()}";

            string allkeypair = "";
            IHeaderDictionary headers = context.Request.Headers;

            foreach (var headerValuePair in headers)
            {
                allkeypair += "\n" + headerValuePair.Key + ":" + headerValuePair.Value;

            }

            string requestHeadersString = $"Request.Headers:\n {allkeypair.ToString()}";

            // this._logger.LogInformation($"\n{DateTime.Now.ToString()}Request.Headers:{0}\n" + allkeypair.ToString());




            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);


            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            //    _logger.LogInformation($"\n{DateTime.Now.ToString()}-Request Body: {requestBodyText}");

            var requestBodyString = $"Request Body: {requestBodyText}";

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            _logger.LogInformation($"{DateTime.Now.ToString()}-{requestUrlString}\n{requestHeadersString}\n{requestBodyString}");

            await next(context);
            context.Request.Body = originalRequestBody;
        }
    }
}