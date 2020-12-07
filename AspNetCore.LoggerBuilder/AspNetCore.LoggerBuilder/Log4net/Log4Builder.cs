using System;
using System.IO;
using log4net;
using log4net.Repository;

namespace AspNetCore.LoggerBuilder
{
    /// <summary>
    /// 功能描述    ：Log4netBuilder  
    /// 创 建 者    ：jinghe
    /// 创建日期    ：2020/11/25 8:43:37
    /// 版权说明    ：Copyright ©- 2020 -xxx 所有
    /// </summary>
    public class Log4Builder
    {
        private static ILog logger;
        private static ILoggerRepository loggerRepository { get; set; }

        /// <summary>
        /// 初始化加载log4net.config配置文件
        /// </summary>
        /// <param name="filePath">指定配置文件log4net.config的全路径,默认加载应用程序的根路径下</param>
        public static void LoadLog4netconfig(string filePath = "")
        {
            if (logger != null) return;
            if (!string.IsNullOrEmpty(filePath))
            {
                if (!File.Exists(filePath))
                    throw new Exception($"未能正确加载指定目录下的log4net.config配置文件，请检查输入的路径：{filePath} 是否存在该文件...");
            }
            else
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "log4net.config");
                if (!File.Exists(filePath))
                    throw new Exception($"未能正确加载应用程序根路径下的log4net.config配置文件，请检查路径：{filePath} 是否存在该文件...");
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                Stream stream = new MemoryStream(bytes);
                loggerRepository = LogManager.CreateRepository("NETCoreLog4netRepository");
                log4net.Config.XmlConfigurator.Configure(loggerRepository, stream);
                logger = LogManager.GetLogger("NETCoreLog4netRepository", "日志信息：");
                stream.Close();
                stream.Dispose();
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志Title</param>
        /// <param name="exception">异常</param>
        /// <param name="level">日志级别-默认级别-Error</param>
        public static void WriteLog(string message, Exception exception = null, Log4Level level= Log4Level.Error)
        {
            switch (level)
            {
                case Log4Level.Info:
                    Info(message,exception);
                    break;
                case Log4Level.Warn:
                    Warn(message, exception);
                    break;
                case Log4Level.Error:
                    Error(message, exception);
                    break;
                case Log4Level.Fatal:
                    Fatal(message, exception);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        private static void Info(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Info(message);
            else
                logger.Info(message, exception);
        }

        /// <summary>
        /// 告警日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        private static void Warn(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Warn(message);
            else
                logger.Warn(message, exception);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        private static void Error(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Error(message);
            else
                logger.Error(message, exception);
        }

        /// <summary>
        /// 严重日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        private static void Fatal(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Fatal(message);
            else
                logger.Fatal(message, exception);
        }
    }
}
