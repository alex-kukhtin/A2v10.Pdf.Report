// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Dynamic;
using System.IO;

using A2v10.Infrastructure;

namespace A2v10.Pdf.Report;

public class PdfReportShim : IPdfReportShim
{
	private IReportLocalizer? _localizer = null;

	public Stream Build(String path, ExpandoObject data)
	{
		if (_localizer == null)
			throw new InvalidOperationException("PdfReportShim. _localizer is null");
		var rep = new PdfBuilder(_localizer, path, data);
		return rep.Build();
	}

	public Stream Build(Stream stream, ExpandoObject data)
	{
		if (_localizer == null)
			throw new InvalidOperationException("PdfReportShim. _localizer is null");
		var rep = new PdfBuilder(_localizer, stream, data);
		return rep.Build();
	}

	public void Inject(ILocalizer localizer, IUserLocale userLocale)
	{
		_localizer = new PdfReportLocalizer(userLocale.Locale, localizer);
	}
}
