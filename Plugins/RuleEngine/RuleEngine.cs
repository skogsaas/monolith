using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.CSharp;

namespace RuleEngine
{
    public class RuleEngine
    {
        private Evaluator evaluator;

        public RuleEngine()
        {
            this.evaluator = new Evaluator(new CompilerContext(new CompilerSettings(), new ConsoleReportPrinter()));

            this.evaluator.
        }
    }
}
