using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\dev\Projects\Roslyn_TEST\Roslyn_TEST\Roslyn_TEST.sln";
            var properties = new Dictionary<string, string>() { };
            var msws = MSBuildWorkspace.Create(properties);

            msws.WorkspaceFailed += (s, e) =>
            {
                Console.WriteLine($"Workspace failed with: {e.Diagnostic}");
            };

            var soln = msws.OpenSolutionAsync(path).Result;

            // Verander 'Name' check om het te compileren project te veranderen.
            var comp = soln.Projects.First(x => x.Name == "RoslynTest").GetCompilationAsync().Result;
            var errs = comp.GetDiagnostics().Where(n => n.Severity == DiagnosticSeverity.Error).ToList();

            comp.Emit(@"E:\dev\Projects\Roslyn_TEST\Roslyn_TEST\output");

            Console.WriteLine(".exe is created!");
            Console.ReadLine();
        }
    }
}
