// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System.Dynamic;

using QuestPDF.Infrastructure;
using QuestPDF.Drawing;

using QuestPDF.Fluent;

namespace A2v10.Pdf.Report;

public class ReportDocument : IDocument
{
	private readonly A2v10.Xaml.Report.Report _report;
	private readonly ExpandoObject _data;
	private readonly ScriptEngine _engine;
	public ReportDocument(A2v10.Xaml.Report.Report report, ExpandoObject data)
	{
		_report = report;
		_data = data;
		_engine = new ScriptEngine(_data);
	}

	public void Compose(IDocumentContainer container)
	{
		container.Page(page =>
		{
			new PageComposer(_report, _engine).Compose(page);
		});
	}

	public DocumentMetadata GetMetadata()
	{
		var title = _report.Title;
		var md = DocumentMetadata.Default;
		md.Title = title;// "Title from C# code";
		return md;
	}
}
