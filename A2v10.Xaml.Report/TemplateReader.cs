// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Xaml;

namespace A2v10.Xaml.Report;

public class TemplateReader
{
	public Page ReadReport(String path)
	{
		var obj = XamlServices.Load(path);
		if (obj is not Page page)
			throw new InvalidOperationException("Object is not a A2v10.Xaml.Report.Page");
		var styleBag = new StyleBag();
		page.ApplyStyles("Root", styleBag);
		return page;
	}
}
