#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using ChilliSource.Mobile.Core;
using Serilog;


namespace ChilliSource.Mobile.Logging
{
   public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithApplicationInformation(this LoggerConfiguration config, IEnvironmentInformation information, Func<string> userKeyRetriever = null)
        {
            return config.Enrich.With(new ApplicationInformationEnricher(information, userKeyRetriever));
        }

        public static void Register(this Serilog.ILogger logger)
        {
            Log.Logger = logger;
        }

        public static Core.ILogger BuildLogger(this LoggerConfiguration config)
        {
            config.CreateLogger()
            .Register();

            return new LoggerProxy();
        }
    }
}
