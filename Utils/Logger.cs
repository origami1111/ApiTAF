using log4net;
using log4net.Config;
using System.Reflection;

namespace ApiTAF.Utils
{
    public static class Logger
    {
        private static ILog log;

        public static ILog Log
        {
            get
            {
                if (log == null)
                {
                    var logRepo = LogManager.GetRepository(Assembly.GetExecutingAssembly());
                    XmlConfigurator.Configure(logRepo, new FileInfo("log4net.config"));
                    log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                }

                return log;
            }
        }
    }
}
