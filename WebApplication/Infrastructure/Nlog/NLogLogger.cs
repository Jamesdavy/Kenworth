using System;
using NLog;
using ILogger = WebApplication.Infrastructure.Logging.ILogger;


namespace WebApplication.Infrastructure.Nlog
{
    public class NLogLogger : ILogger
    {

        private Logger _logger;

        public NLogLogger(string currentClassName)
        {
            _logger = LogManager.GetLogger(currentClassName);
            
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void ObjectLogger<T>(string jsonObject, long id)
        {
            var Event = new LogEventInfo(LogLevel.Debug, "ObjectLogger", jsonObject);
            var logger = LogManager.GetLogger(typeof (T).FullName);
            Event.Properties["Id"] = id;
            Event.Properties["Object"] = typeof (T).FullName;
            logger.Log(Event);
        }

        public void SQLLogger()
        {
            
        }


        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }
    }

    public class LogUtility
    {

        public static string BuildExceptionMessage(Exception x)
        {

            Exception logException = x;
            if (x.InnerException != null)
                logException = x.InnerException;

            string strErrorMsg = Environment.NewLine + "Error in Path :" + System.Web.HttpContext.Current.Request.Path;

            // Get the QueryString along with the Virtual Path
            strErrorMsg += Environment.NewLine + "Raw Url :" + System.Web.HttpContext.Current.Request.RawUrl;

            // Get the error message
            strErrorMsg += Environment.NewLine + "Message :" + logException.Message;

            // Source of the message
            strErrorMsg += Environment.NewLine + "Source :" + logException.Source;

            // Stack Trace of the error
            strErrorMsg += Environment.NewLine + "Stack Trace :" + logException.StackTrace;

            // Method where the error occurred
            strErrorMsg += Environment.NewLine + "TargetSite :" + logException.TargetSite;
            return strErrorMsg;
        }
    }
}