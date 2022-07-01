// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Xaml;

namespace A2v10.Pdf.Xaml;

public class TemplateReader
{
	public Report ReadReport(String path)
	{
		var obj = XamlServices.Load(path);
		if (obj is not Report rep)
			throw new InvalidOperationException("Object is not a A2v10.Pdf.Xaml.Report");
		return rep;
	}
}
