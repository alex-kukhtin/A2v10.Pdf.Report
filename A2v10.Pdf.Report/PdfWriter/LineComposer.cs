// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using A2v10.Xaml.Report;
using System;

namespace A2v10.Pdf.Report;

internal class LineComposer : FlowElementComposer
{
	private readonly Line _line;
	private readonly RenderContext _context;

	public LineComposer(Line line, RenderContext context)
	{
		_line = line;
		_context = context;
	}

	void ApplyRuntimeStyle(TextDescriptor _)
	{
	}

	void ApplyRuntimeStyle(TextSpanDescriptor descr, ContentElement _)
	{
	}

	internal override void Compose(IContainer container, Object? value = null)
	{
		container.ApplyDecoration(_line.RuntimeStyle).LineHorizontal(_line.Thickness.Value, _line.Thickness.Unit.ToUnit());
	}
}
