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

	private readonly TemplateReader _templateReader;
	public PdfBuilder(String templatePath, ExpandoObject model)
	{
		_templatePath = templatePath;
		_model = model;
		_templateReader = new TemplateReader();		
	}	

	public Stream Build()
	{
		var ms = new MemoryStream();
		Build(ms);
		ms.Seek(0, SeekOrigin.Begin);
		return ms;
	}

	public void Build(Stream stream)
	{
		var rep = _templateReader.ReadReport(_templatePath);
		var doc = new ReportDocument(rep, _model);
		doc.GeneratePdf(stream);
	}
}
