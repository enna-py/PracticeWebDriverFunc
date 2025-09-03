using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config;
public static class LogManagerHelper
{
    public static ILog ConfigureLogger()
    {
        var logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        if (!Directory.Exists(logFolder))
            Directory.CreateDirectory(logFolder);

        var logRepository = LogManager.GetRepository(typeof(LogManagerHelper).Assembly);

        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        return LogManager.GetLogger(typeof(LogManagerHelper));
    }
}
