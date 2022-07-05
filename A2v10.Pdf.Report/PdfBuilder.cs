// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Dynamic;
using System.IO;

using QuestPDF.Fluent;

using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

public class PdfBuilder
{
	private readonly String? _templatePath;
	private readonly Stream? _templateStream;
	private readonly ExpandoObject _model;
	private readonly IReportLocalizer _localizer;

	public PdfBuilder(IReportLocalizer localizer, String templatePath, ExpandoObject model)
	{
		_localizer = localizer;
		_templatePath = templatePath;
		_model = model;
		_templateStream = null;
	}

	public PdfBuilder(IReportLocalizer localizer, Stream stream, ExpandoObject model)
	{
		_localizer = localizer;
		_templateStream = stream;
		_model = model;
	}

	public Stream Build()
	{
		var ms = new MemoryStream();
		Build(ms);
		ms.Seek(0, SeekOrigin.Begin);
		return ms;
	}

	public Page Read()
	{
		var rdr = new TemplateReader();
		if (_templateStream != null)
			return rdr.ReadReport(_templateStream);
		else if (_templatePath != null)
			return rdr.ReadReport(_templatePath);
		throw new ArgumentNullException(nameof(_templatePath));
	}



	public void Build(Stream stream)
	{
		var page = Read();
		var context = new RenderContext(_localizer, _model, page.Code);
		var doc = new ReportDocument(page, context);
		doc.GeneratePdf(stream);
	}
}
