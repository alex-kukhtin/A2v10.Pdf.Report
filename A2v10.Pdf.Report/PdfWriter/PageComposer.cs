// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace A2v10.Pdf.Report;

internal class PageComposer
{
	private readonly A2v10.Xaml.Report.Page _report;
	private readonly RenderContext _context;

	internal PageComposer(A2v10.Xaml.Report.Page report, RenderContext context)
	{
		_report = report;
		_context = context;
	}

	internal void Compose(PageDescriptor page)
	{
		// TODO: styles
		page.Size(PageSizes.A4.Portrait());
		page.MarginVertical(20, Unit.Millimetre);
		page.MarginHorizontal(10, Unit.Millimetre);
		page.DefaultTextStyle(ts => ts
			.FontFamily(Fonts.Verdana)
			.FontSize(10F)
		);

		// header
		// TODO:

		// content
		page.Content().Element(ComposeContent);

		// footer
		// TODO
	}

	void ComposeContent(IContainer container)
	{
		foreach (var c in _report.Columns)
		{
			container.Column(column =>
			{
				var cc = new ColumnComposer(c, _context);
				cc.Compose(column);
			});
		}
	}
}
