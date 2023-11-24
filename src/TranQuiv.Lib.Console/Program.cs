﻿// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Reflection;
using Humanizer;
using Serilog;
using Serilog.Events;
using TranQuiv.Lib.Console;
using TranQuiv.Lib.Crypto;
using TranQuiv.Lib.Extensions;
using TranQuiv.Lib.Json.Extensions;
using TranQuiv.Lib.TryHandlers;

const string LogTemplate = "[{Timestamp:dd/MM/yyyy HH:mm:ss}] [{Level:u3}] <{ThreadId}> [{SourceContext}] {Message:lj}{NewLine}{Exception}";
Serilog.Log.Logger = new LoggerConfiguration()
.MinimumLevel.Verbose()
      .Enrich.FromLogContext()
        .Enrich.With(new ThreadIdEnricher())
      .WriteTo.File($"./logs/log.log", rollingInterval: RollingInterval.Day, outputTemplate: LogTemplate, restrictedToMinimumLevel: LogEventLevel.Verbose)
      .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: LogTemplate)
      .CreateLogger();

var handler = new TryHandler()
{
    ExceptionHandleAction = ex => Log.Error(ex, "error:")
};
handler.TryEnsure(() => new TestStruct() { Val = 12 });
var result = handler.Try(() => new TestClass() { Val = 12 }).Result;

var name = result?.GetDisplayName(m => m.Val);
var des = result?.GetDescription(m => m.Val);
var name2 = result?.GetMemberInfo(m => m.Val)?.GetAttributes<DisplayNameAttribute>();

var ta = TestEnum.Blue.GetDescription().ToJsonString();
long val = 3501;
Log.Information(val + " " + val.ToWords(new System.Globalization.CultureInfo("vi-VN")));
Log.Information(val + " " + val.ToWords());
Log.Information(val.ToVietnameseNumberString() + " " + val.ToVietnameseNumberString(true));
// handler.Try(() => throw new NotImplementedException());

// var handlerAsync = new TryHandlerAsync()
// {
//     BeforeTask = () => Task.Run(() => Log.Information("before")),
//     AfterTask = () => Task.Run(() => Log.Information("after")),
//     FinallyTask = () => Task.Run(() => Log.Information("finally")),
//     ExceptionHandleTask = (ex) => Task.Run(() => Serilog.Log.Error(ex, "error:"))
// };
// handlerAsync.Try(() => new TestStruct() { Val = 12 });
// handlerAsync.Try(() => new TestClass() { Val = 12 });

// await handlerAsync.Try(async () => await Task.FromResult<TestStruct?>(new TestStruct() { Val = 12 }));
// await handlerAsync.TryEnsure(() => Task.Run(() => new TestClass() { Val = 12 }));
// handlerAsync.Try(() => new TestClass() { Val = 12 });
// handlerAsync.TryEnsure(() =>
// {
//     Log.Information("starting");
//     Task.Delay(3000).Wait();
//     Log.Information("done");
//     return new TestStruct() { Val = 123 };
// });
