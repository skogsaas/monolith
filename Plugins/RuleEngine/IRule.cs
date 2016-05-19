using System;

namespace RuleEngine
{
	public interface IRule
	{
		bool initialize();
		bool uninitialize();
	}
}

