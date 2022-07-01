using A2v10.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp;

public class NullDataProfiler : IDataProfiler
{
	public IDisposable? Start(String command)
	{
		return null;
	}
}

public class NullDataLocalizer : IDataLocalizer
{
	public String Localize(String content)
	{
		return content;
	}
}

public class SimpleDataConfiguration : IDataConfiguration
{
	public String ConnectionString(String source)
	{
		return "Data Source=localhost;Initial Catalog=a2v10demo;Integrated Security=True";
	}
}
