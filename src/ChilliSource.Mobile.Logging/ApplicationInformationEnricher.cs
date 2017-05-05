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
using Serilog.Core;
using Serilog.Events;

namespace ChilliSource.Mobile.Logging
{
    public class ApplicationInformationEnricher : ILogEventEnricher
    {
        private readonly IEnvironmentInformation _information;
        private readonly Func<string> _userKeyRetriever;

        public ApplicationInformationEnricher(IEnvironmentInformation information, Func<string> userKeyRetriever = null)
        {
            _information = information;
            _userKeyRetriever = userKeyRetriever;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_information.ApplicationName), _information.ApplicationName));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_information.AppId), _information.AppId));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_information.AppVersion), _information.AppVersion));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_information.ExecutionEnvironment), _information.ExecutionEnvironment));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_information.Platform), _information.Platform));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_information.Timezone), _information.Timezone));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_information.DeviceName), _information.DeviceName));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserKey", _userKeyRetriever?.Invoke()));
        }
    }
}

