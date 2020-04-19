using System;

using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;


using Microsoft.AspNetCore.Mvc.Filters;






namespace iAspNetcore.Middleware.ErrorLoggingMiddleware
{
  public  class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        
        private readonly DiagnosticSource _diagnosticSource;
        

        public ErrorLoggingMiddleware(RequestDelegate next, DiagnosticSource diagnosticSource)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

           
            _next = next;
            _diagnosticSource = diagnosticSource;
          

        }

        public async Task Invoke(HttpContext context)
        {


            var ip = "127.0.0.1";
            ip = context.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }

            var PageUrl = context.Request.Host.ToString() + context.Request.Path.ToString() + context.Request.QueryString.ToString();
            var UserAgent = context.Request.Headers["User-Agent"];


            try
            {
                await _next(context);


            }
            catch (Exception ex)
            {
                //Log log = new Log();

                //log.LogLevel = NopLogLevel.Error;
                //log.ReferrerUrl = context.Request.Headers["referer"].ToString() + " " + context.Connection.RemoteIpAddress.ToString();
                //log.ShortMessage = string.Format("Error :provider by ErrorLoggingMiddleware.Status:{0} PageUrl:{1} UserAgent:{2} IpAddress:{3}", context.Response.StatusCode.ToString(), PageUrl, UserAgent, ip);
                //log.FullMessage = context.Request.Headers["User-Agent"] + "err message:" + ex.Message + "ex.StackTrace:" + ex.StackTrace;
                //log.PageUrl = context.Request.Host.ToString() + context.Request.Path.ToString() + context.Request.QueryString.ToString();
                //log.IpAddress = ip;
                //log.CreatedOnUtc = DateTime.UtcNow;


                //_logger.InsertLog(log);



                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error != null)
                {
                    // This error would not normally be exposed to the client
                    //await context.Response.WriteAsync("<br>Error: " + HtmlEncoder.Default.Encode(error.Error.Message) + "<br>\r\n");
                    //await context.Response.WriteAsync("<br>Error: " + HtmlEncoder.Default.Encode(ex.StackTrace) + "<br>\r\n");
                }

             //   System.Diagnostics.Debug.WriteLine(log.ShortMessage + log.FullMessage + log.PageUrl + $"The following error happened: {ex.Message}");

                throw; // Don't stop the error
            }
        }
    }

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        { 
            this.next = next;
        }
        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try { 
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            System.Net.HttpStatusCode code = System.Net.HttpStatusCode.InternalServerError; // 500 if unexpected



            string result = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    code = code,
                    error = exception.Message
                }
            );

           // var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }

}
