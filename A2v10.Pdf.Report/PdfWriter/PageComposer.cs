﻿// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using A2v10.Xaml.Report;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace A2v10.Pdf.Report;

internal class PageComposer
{
	private readonly Page _page;
	private readonly RenderContext _context;

	internal PageComposer(A2v10.Xaml.Report.Page report, RenderContext context)
	{
		_page = report;
		_context = context;
	}

	internal void Compose(PageDescriptor page)
	{
		// TODO: styles

		var size = PageSizes.A4;
		switch (_page.Orientation)
		{
			case PageOrientation.Portrait:
				size = size.Portrait();
				break;
			case PageOrientation.Landscape:
				size = size.Landscape();
				break;
		}
		page.Size(size);
		var rs = _page.GetRuntimeStyle();
		if (rs != null && rs.Margin != null)
		{
			var mrg = rs.Margin;
			page.MarginLeft(mrg.Left.Value, mrg.Left.Unit.ToUnit());
			page.MarginRight(mrg.Right.Value, mrg.Right.Unit.ToUnit());
			page.MarginTop(mrg.Top.Value, mrg.Top.Unit.ToUnit());
			page.MarginBottom(mrg.Bottom.Value, mrg.Bottom.Unit.ToUnit());
		}

		page.DefaultTextStyle(ts => {
			ts.FontFamily(Fonts.Verdana);
			if (rs != null && rs.FontSize != null)
				ts = ts.FontSize(rs.FontSize.Value);
			else
				ts.FontSize(10F);
			return ts;
		});

		// header
		// TODO:

		// content
		page.Content().Element(ComposeContent);

		// footer
		// TODO
	}

	void ComposeContent(IContainer container)
	{
		foreach (var c in _page.Columns)
		{
			container.Column(column =>
			{
				var cc = new ColumnComposer(c, _context);
				cc.Compose(column);
			});
		}
	}
}
