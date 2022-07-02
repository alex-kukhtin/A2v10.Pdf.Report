// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Xaml;

namespace A2v10.Xaml.Report;

public class TemplateReader
{
	public Report ReadReport(String path)
	{
		var obj = XamlServices.Load(path);
		if (obj is not Report rep)
			throw new InvalidOperationException("Object is not a A2v10.Xaml.Report.Report");
		return rep;
	}
}
