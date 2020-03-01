using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

using Microsoft.Extensions.Logging;


namespace iAspNetCore.Middleware.Logging.ColorConsole
{
   

    public class ColorConsoleLoggerProvider : ILoggerProvider
    {

        private readonly ConcurrentDictionary<string, ColorConsoleLogger> _loggers = new ConcurrentDictionary<string, ColorConsoleLogger>();

        public ColorConsoleLoggerProvider()

        {

           

        }


        public ILogger CreateLogger(string categoryName)

        {

            return _loggers.GetOrAdd(categoryName, name => new ColorConsoleLogger(name));

        }
        

        public void Dispose()
        {
            _loggers.Clear();

        }

        
    }
}
