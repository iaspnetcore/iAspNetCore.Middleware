using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

using Microsoft.Extensions.Logging;

namespace iAspNetCore.Middleware.Logging.ColoredConsole
{
    public class ColoredConsoleLoggerProvider : ILoggerProvider
    {
       
        private readonly ConcurrentDictionary<string, ColoredConsoleLogger> _loggers = new ConcurrentDictionary<string, ColoredConsoleLogger>();
        public ColoredConsoleLoggerProvider()
        {
           
        }
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new ColoredConsoleLogger(name));
        }
        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
