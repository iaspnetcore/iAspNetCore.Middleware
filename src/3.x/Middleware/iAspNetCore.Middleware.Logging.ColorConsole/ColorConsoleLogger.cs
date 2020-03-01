using System;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

//https://dzone.com/articles/how-to-add-custom-logging-in-aspnet-core
//https://dzone.com/articles/customizing-aspnet-core-part-1-logging

namespace iAspNetCore.Middleware.Logging.ColorConsole
{
   
    public class ColorConsoleLogger : ILogger

    {
        private static object _lock = new Object();
        private readonly string _name;

       



        public ColorConsoleLogger(string name)

        {

            _name = name;

          

        }



        public IDisposable BeginScope<TState>(TState state)

        {

            return null;

        }



        public bool IsEnabled(LogLevel logLevel)

        {

            return logLevel == LogLevel.Information;

        }



        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)

        {

            if (!IsEnabled(logLevel))

            {

                return;

            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }


            lock (_lock)
            {
                if (true)
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {_name} - {formatter(state, exception)}");
                    Console.ForegroundColor = color;
                }
            }


            string message = formatter(state, exception);

            if (!(message.Contains("dd")))
            {
                return;
            }


            var builder = new StringBuilder();
            builder.Append(DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"));
            builder.Append(" [");
            builder.Append(GetLogLevelString(logLevel));
            builder.Append("] ");
           // builder.Append(_category);
            builder.Append("[");
            builder.Append(eventId);
            builder.Append("]");
            builder.Append(": ");
            builder.AppendLine(formatter(state, exception));

             message = builder.ToString();

         //   System.Console.WriteLine("---" + builder.ToString() + "--origal");


            if (message.Contains("dd"))
            {

             //   var color = Console.ForegroundColor;
               

             //   Console.ForegroundColor = ConsoleColor.DarkRed;

              

               

             ////   System.Console.WriteLine("---" + builder.ToString() + "--has dd");
             //    Console.ForegroundColor = color;
            }

            else
            {
                return;
            }

            //var color = Console.ForegroundColor;

            //    Console.ForegroundColor = ConsoleColor.Blue;

            //    Console.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {_name} - {formatter(state, exception)}");

            //    Console.ForegroundColor = color;

            

        }


        private static string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "trce";
                case LogLevel.Debug:
                    return "dbug";
                case LogLevel.Information:
                    return "CustomInfo";
                case LogLevel.Warning:
                    return "warn";
                case LogLevel.Error:
                    return "fail";
                case LogLevel.Critical:
                    return "crit";
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

    }
}
