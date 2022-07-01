// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System.Dynamic;

using A2v10.Pdf.Xaml;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
		page.Size(PageSizes.A4.Portrait());
		page.MarginVertical(20, Unit.Millimetre);
		page.MarginHorizontal(10, Unit.Millimetre);
		page.DefaultTextStyle(ts => ts
			.FontFamily(Fonts.Verdana)
			.FontSize(12F)
		);

		// header
		page.Content().Element(ComposeContent);
		// footer
	}

	void ComposeContent(IContainer container)
	{
		container.Column(column =>
		{
			column.Item()
				.Text("Hello from C#");
		});
	}
}
