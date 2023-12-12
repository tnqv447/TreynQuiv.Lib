// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using Humanizer;
using Serilog;
using Serilog.Events;
using TreynQuiv.Lib.Console;
using TreynQuiv.Lib.Common.Extensions;
using TreynQuiv.Lib.Json.Extensions;
using TreynQuiv.Lib.TryHandlers;
using System.Linq.Expressions;

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
var result = handler.Try(() => new TestClass() { Val = 12 }).Value;

var name = result?.GetDisplayName(m => m.Val);
var des = result?.GetDescription(m => m.Val);
var name2 = result?.GetMemberInfo(m => m.Val)?.GetAttributes<DisplayNameAttribute>();

var ta = TestEnum.Blue.GetDescription().ToJsonString();
long val = 3501;
Log.Information(val + " " + val.ToWords(new System.Globalization.CultureInfo("vi-VN")));
Log.Information(val + " " + val.ToWords());
Log.Information(val.ToVietnameseNumberString() + " " + val.ToVietnameseNumberString(true));

Expression<Func<TestClass, bool>> testExp = m => m.Val >= 100 && m.Val >= 150;
Expression<Func<TestClass, bool>> testExp2 = m => m.Val >= 200;
Expression<Func<TestClass, bool>> testExp3 = y => y.Val >= 200;

var mergeExp = testExp.AndAlso(testExp2);

Console.WriteLine(testExp);
Console.WriteLine(testExp2);
Console.WriteLine(mergeExp);
Console.WriteLine(testExp.AndAlso(testExp3));
Console.WriteLine(testExp.OrElse(testExp3));
// Console.WriteLine(Expression.Lambda<Func<TestClass, bool>>(Expression.AndAlso(testExp, testExp2), Expression.Parameter(typeof(int), "k")));

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
