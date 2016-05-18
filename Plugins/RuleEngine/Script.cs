using Mono.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine
{
    public class Script
    {
        public string Path { get; private set; }
        public bool Initialized { get; private set; }

        private Evaluator evaluator;

        public Script(string path)
        {
            this.Path = path;

            this.evaluator = new Evaluator(new CompilerContext(new CompilerSettings(), new ConsoleReportPrinter()));
            this.evaluator.ReferenceAssembly(Assembly.GetCallingAssembly());

            load();
        }

        private void load()
        {
            string script = File.ReadAllText(this.Path);

            this.Initialized = this.evaluator.Run(script);
        }
    }
}
