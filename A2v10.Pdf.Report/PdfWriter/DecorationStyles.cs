// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using QuestPDF.Infrastructure;

using QuestPDF.Fluent;
using QuestPDF.Elements.Table;

using A2v10.Xaml.Report;


namespace A2v10.Pdf.Report;

internal static class DecorationStyles
{

	public static IContainer ApplyAlign(this IContainer container, RuntimeStyle? style)
	{
		if (style == null)
			return container;
		if (style.Align != null)
		{
			switch (style.Align.Value)
			{
				case TextAlign.Left:
					container = container.AlignLeft();
					break;
				case TextAlign.Center:
					container = container.AlignCenter();
					break;
				case TextAlign.Right:
					container = container.AlignRight();
					break;
			}
		}
		if (style.VAlign != null)
		{
			switch (style.VAlign.Value)
			{
				case VertAlign.Top:
					container = container.AlignTop();
					break;
				case VertAlign.Middle:
					container = container.AlignMiddle();
					break;
				case VertAlign.Bottom:
					container = container.AlignBottom();
					break;
			}
		}
		return container;
	}
	public static IContainer ApplyDecoration(this IContainer container, RuntimeStyle? style)
	{
		if (style == null)
			return container;
		if (style.Background != null)
			container = container.Background(style.Background);
		if (style.Border != null)
			container = container.Border(style.Border.Value);
		container = ApplyAlign(container, style);
		if (style.PaddingVertical != null)
			container = container.PaddingVertical(style.PaddingVertical.Value);
		if (style.PaddingHorizontal != null)
			container = container.PaddingHorizontal(style.PaddingHorizontal.Value);
		if (style.Padding != null)
			container = container.Padding(style.Padding.Value);
		return container;
	}

	public static IContainer ApplyCellDecoration(this ITableCellContainer container, RuntimeStyle? style)
	{
		if (style == null)
			return container;
		return container.ApplyDecoration(style);
	}

	public static void ApplyText(this TextSpanDescriptor container, RuntimeStyle? style)
	{
		if (style == null)
			return;
		if (style.Bold != null && style.Bold.Value)
			container.Bold();
	}
}
