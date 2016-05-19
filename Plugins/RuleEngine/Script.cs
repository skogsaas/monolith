using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono.CSharp;
using Monolith;

namespace RuleEngine
{
    public class Script
    {
        public string Path { get; private set; }
		public string Script { get; private set; }
        public bool Loaded { get; private set; }

        private Evaluator evaluator;

        public Script(string path)
        {
            this.Path = path;

            this.evaluator = new Evaluator(new CompilerContext(new CompilerSettings(), new ConsoleReportPrinter()));
            this.evaluator.ReferenceAssembly(Assembly.GetCallingAssembly());
			this.evaluator.ReferenceAssembly(typeof(IPlugin).Assembly);

            load();
			initialize();
        }

        private void load()
        {
			this.Script = File.ReadAllText(this.Path);

			this.Loaded = this.Script.Count > 0;
        }

		private void initialize()
		{
			
		}
    }
}
