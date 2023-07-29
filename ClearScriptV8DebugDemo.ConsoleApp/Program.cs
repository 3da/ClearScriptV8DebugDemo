using Microsoft.ClearScript.V8;

namespace ClearScriptV8DebugDemo.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var flags = V8ScriptEngineFlags.AwaitDebuggerAndPauseOnStart
                        | V8ScriptEngineFlags.EnableRemoteDebugging
                        | V8ScriptEngineFlags.EnableTaskPromiseConversion
                        | V8ScriptEngineFlags.EnableValueTaskPromiseConversion
                        | V8ScriptEngineFlags.EnableDebugging;

            //flags = V8ScriptEngineFlags.None;

            var debugPort = 9981;

            using var engine = new V8ScriptEngine(flags, debugPort);
            engine.FormatCode = true;
            engine.AddHostType(typeof(Console));

            engine.Execute("for(let i = 0; i < 10; i++){ Console.Beep(2000+i*500, 500); }");
        }
    }
}