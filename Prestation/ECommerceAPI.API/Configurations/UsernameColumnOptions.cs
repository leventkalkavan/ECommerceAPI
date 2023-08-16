using System.Data;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace ECommerceAPI.API.Configurations;

public class UsernameColumnOptions : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var (username, value) = logEvent.Properties.FirstOrDefault(x => x.Key == "UserName");
        if (value != null)
        {
            var getValue=  propertyFactory.CreateProperty(username,value);
            logEvent.AddPropertyIfAbsent(getValue);
        }          
    }
}