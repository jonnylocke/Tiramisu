
namespace Restaurants.Corp.Domain.Messaging
{
    public class Logger
    {
        public void WriteToEventLog(string sEvent, string source)
        {
            //const string sLog = "Application";

            //if (!EventLog.SourceExists(source))
            //    EventLog.CreateEventSource(source, sLog);

            //EventLog.WriteEntry(source, sEvent);
            //EventLog.WriteEntry(source, sEvent, EventLogEntryType.Information, 0);
        }
    }
}
