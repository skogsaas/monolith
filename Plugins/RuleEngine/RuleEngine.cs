using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Monolith.Plugins;

namespace RuleEngine
{
    public class RuleEngine : IPlugin
    {
		private Dictionary<string, Script> scripts;
		
        public RuleEngine()
        {
			this.scripts = new Dictionary<string, Script>();
        }

		public void initialize()
		{
			load();
		}

		private void load()
		{
			string path = Directory.GetCurrentDirectory();
			string[] files = Directory.GetFiles(path + "/Scripts", "*.cs");

			foreach (string p in files)
			{
				this.scripts[p] = new Script(p);
			}
		}
    }
}
