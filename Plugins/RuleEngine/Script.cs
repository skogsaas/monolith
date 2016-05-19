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
		public string Data { get; private set; }

		public IRule Rule { get; private set; }

        private Evaluator evaluator;

        public Script(string path)
        {
            this.Path = path;
			this.Rule = null;

            this.evaluator = new Evaluator(new CompilerContext(new CompilerSettings(), new ConsoleReportPrinter()));
            this.evaluator.ReferenceAssembly(Assembly.GetCallingAssembly());
			this.evaluator.ReferenceAssembly(typeof(IPlugin).Assembly);

            load();
			initialize();
        }

        private void load()
        {
			this.Data = File.ReadAllText(this.Path);
        }

		private void initialize()
		{
			if (this.Data.Count > 0) 
			{
				this.evaluator.Run (this.Data);

				object obj = this.evaluator.Evaluate("new " + System.IO.Path.GetFileNameWithoutExtension (this.Path) + "();");

				if (typeof(IRule).IsAssignableFrom (obj.GetType ()))
				{
					this.Rule = (IRule)obj;
				}
			}
		}
    }
}
