using System;
using System.Dynamic;
using System.IO;
using A2v10.Pdf.Xaml;
using QuestPDF.Fluent;

namespace A2v10.Pdf.ReportBuilder;

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
		return ms;
	}

	public void Build(Stream stream)
	{
		Report rep = _templateReader.ReadReport(_templatePath);
		var doc = new ReportDocument(rep, _model);
		doc.GeneratePdf(stream);
		stream.Seek(0, SeekOrigin.Begin);
	}
}
