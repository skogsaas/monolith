using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuleEngine;

public class Toggle : IRule
{
	public bool initialize()
	{
		return true;
	}

	public bool uninitialize()
	{
		return true;
	}
}