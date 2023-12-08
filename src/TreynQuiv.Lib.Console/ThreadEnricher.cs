using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;

namespace TreynQuiv.Lib.Console;

internal class ThreadIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
          "ThreadId", Environment.CurrentManagedThreadId.ToString("D4")));
    }
}
