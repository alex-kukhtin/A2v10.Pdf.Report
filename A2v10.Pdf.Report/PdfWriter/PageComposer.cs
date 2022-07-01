// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System.Dynamic;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

using A2v10.Pdf.Xaml;


namespace A2v10.Pdf;

internal class PageComposer
{
	private readonly Report _report;
	private readonly ExpandoObject _data;

	internal PageComposer(Report report, ExpandoObject data)
	{
		_report = report;
		_data = data;
	}

	internal void Compose(PageDescriptor page)
	{
		// TODO: styles
		page.Size(PageSizes.A4.Portrait());
		page.MarginVertical(20, Unit.Millimetre);
		page.MarginHorizontal(10, Unit.Millimetre);
		page.DefaultTextStyle(ts => ts
			.FontFamily(Fonts.Verdana)
			.FontSize(12F)
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
				var cc = new ColumnComposer(c, _data);
				cc.Compose(column);
			});
		}
	}
}
