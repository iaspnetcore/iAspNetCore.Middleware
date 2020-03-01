using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using iAspNetCore.Middleware.Logging.ColorConsole;


namespace Microsoft.Extensions.Logging
{
   
    public static class ColorConsoleLoggerExtensions
    {

        /// <summary>
        /// Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        public static ILoggingBuilder  AddiAspNetcoreColorConsole(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, ColorConsoleLoggerProvider>();
            return builder;
        }

       
    }
}



