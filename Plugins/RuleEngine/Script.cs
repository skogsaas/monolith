using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono.CSharp;
using Monolith.Plugins;

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

			// TODO Make a custom ReportPrinter that logs using the Monolith.Logging.Logger.
            this.evaluator = new Evaluator(new CompilerContext(new CompilerSettings(), new ConsoleReportPrinter()));
            this.evaluator.ReferenceAssembly(Assembly.GetCallingAssembly());

            load();
			initialize();
        }

        private void load()
        {
			this.Data = File.ReadAllText(this.Path);
        }

		private void initialize()
		{
			try
			{
				if (this.Data.Length > 0) 
				{
					if(this.evaluator.Run (this.Data))
					{
						string expression = "new " + System.IO.Path.GetFileNameWithoutExtension (this.Path) + "();";

						object obj = this.evaluator.Evaluate(expression);

						if (typeof(IRule).IsAssignableFrom (obj.GetType ()))
						{
							this.Rule = (IRule)obj;
							this.Rule.initialize();
						}
					}
				}
			}
			catch(Exception ex) 
			{
				Monolith.Logging.Logger.Error("Couldn't initialize the Rule <" + System.IO.Path.GetFileNameWithoutExtension (this.Path) + "> due to: " + ex.Message);
			}
		}
    }
}
