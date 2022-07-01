// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System.Dynamic;

using QuestPDF.Infrastructure;
using QuestPDF.Drawing;

using A2v10.Pdf.Xaml;
using QuestPDF.Fluent;

namespace A2v10.Pdf;

public class ReportDocument : IDocument
{
	private readonly Report _report;
	private readonly ExpandoObject _data;
	public ReportDocument(Report report, ExpandoObject data)
	{
		_report = report;
		_data = data;
	}

	public void Compose(IDocumentContainer container)
	{
		container.Page(page =>
		{
			new PageComposer(_report, _data).Compose(page);
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
