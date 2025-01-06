// Copyright 2007-2012 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Topshelf.Logging
{
    using System;
    using ElmahCore;

    public class ElmahLogWriterFactory :
        LogWriterFactory
    {
        private readonly ErrorLog _errorLog;
        private readonly ElmahLogLevels _logLevels;
        private ElmahLogWriterFactory(ErrorLog errorLog, ElmahLogLevels logLevels)
        {
            _errorLog = errorLog;
            _logLevels = logLevels;
        }

        public LogWriter Get(string name)
        {
            return new ElmahLogWriter(_errorLog, _logLevels);
        }

        public void Shutdown()
        {

        }

        public static void Use(ErrorLog errorLog, ElmahLogLevels logLevels = null)
        {
            HostLogger.UseLogger(new ElmahHostLoggerConfigurator(errorLog, logLevels));
        }


        [Serializable]
        public class ElmahHostLoggerConfigurator :
            HostLoggerConfigurator
        {
            private readonly ErrorLog _errorLog;
            private readonly ElmahLogLevels _logLevels;            
            public ElmahHostLoggerConfigurator(ErrorLog errorLog, ElmahLogLevels logLevels)
            {
                _logLevels = logLevels;
            }

            public LogWriterFactory CreateLogWriterFactory()
            {
                return new ElmahLogWriterFactory(_errorLog, _logLevels);
            }
        }
    }
}
