// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Dynamic;
using System.IO;

using QuestPDF;
using QuestPDF.Infrastructure;

using A2v10.Infrastructure;

namespace A2v10.Pdf.Report;

public class PdfReportShim : IPdfReportShim
{
	private IReportLocalizer? _localizer = null;
	private String _rootPath = String.Empty;

	public PdfReportShim()
	{
		Settings.License ??= LicenseType.Community;
	}

	public Stream Build(String path, ExpandoObject data)
	{
		if (_localizer == null)
			throw new InvalidOperationException("PdfReportShim. _localizer is null");
		var rep = new PdfBuilder(_rootPath, _localizer, path, data);
		return rep.Build();
	}

	public Stream Build(String path, Stream stream, ExpandoObject data)
	{
		if (_localizer == null)
			throw new InvalidOperationException("PdfReportShim. _localizer is null");
		var rep = new PdfBuilder(_rootPath, _localizer, path, stream, data);
		return rep.Build();
	}

	public void Inject(ILocalizer localizer, IUserLocale userLocale, String rootPath)
	{
		_localizer = new PdfReportLocalizer(userLocale.Locale, localizer);
		_rootPath = rootPath;
	}
}
