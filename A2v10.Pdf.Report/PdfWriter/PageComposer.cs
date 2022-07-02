// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace A2v10.Pdf.Report;

internal class PageComposer
{
	private readonly A2v10.Xaml.Report.Report _report;
	private readonly ScriptEngine _engine;

	internal PageComposer(A2v10.Xaml.Report.Report report, ScriptEngine engine)
	{
		_report = report;
		_engine = engine;
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
				var cc = new ColumnComposer(c, _engine);
				cc.Compose(column);
			});
		}
	}
}
