// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using QuestPDF.Infrastructure;
using QuestPDF.Drawing;

using QuestPDF.Fluent;

namespace A2v10.Pdf.Report;

internal class ReportDocument : IDocument
{
	private readonly A2v10.Xaml.Report.Report _report;
	private readonly RenderContext _context;
	public ReportDocument(A2v10.Xaml.Report.Report report, RenderContext context)
	{
		_report = report;
		_context = context;
	}

	public void Compose(IDocumentContainer container)
	{
		container.Page(page =>
		{
			new PageComposer(_report, _context).Compose(page);
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
