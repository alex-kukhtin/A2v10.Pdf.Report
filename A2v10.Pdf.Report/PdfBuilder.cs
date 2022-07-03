// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Dynamic;
using System.IO;

using QuestPDF.Fluent;

using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

public class PdfBuilder
{
	private readonly String _templatePath;
	private readonly ExpandoObject _model;
	private readonly IReportLocalizer _localizer;

	public PdfBuilder(IReportLocalizer localizer, String templatePath, ExpandoObject model)
	{
		_localizer = localizer;
		_templatePath = templatePath;
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
		return rdr.ReadReport(_templatePath);
	}

	public void Build(Stream stream)
	{
		var rdr = new TemplateReader();
		var rep = rdr.ReadReport(_templatePath);
		var context = new RenderContext(_localizer, _model);
		var doc = new ReportDocument(rep, context);
		doc.GeneratePdf(stream);
	}
}
