using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CakeBootstrapper
{
    class Program
    {
        static int Main(string[] args)
        {
            var cakePath = Path.Combine(Environment.GetEnvironmentVariable("CAKE_HOME")??".", "cake.dll");

            IEnumerable<string> arguments = new[] { $"\"{cakePath}\"" };

            if (args.Length > 0 && args[0].EndsWith(".cake"))
            {
                arguments = arguments.Concat(new[] { $"\"{args[0]}\"" });
                args = args.Skip(1).ToArray();
            }

            arguments = arguments
                .Concat(new[] { "--nuget_useinprocessclient=true" })
                .Concat(args);
            
            var process = Process.Start("dotnet", $"{string.Join(" ", arguments)}");

            process.WaitForExit();

            return process.ExitCode;
        }
    }
}
