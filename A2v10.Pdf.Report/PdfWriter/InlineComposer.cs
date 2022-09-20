﻿// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

internal class InlinedComposer : FlowElementComposer
{
	private readonly Inlined _inlined;
	private readonly RenderContext _context;

	internal InlinedComposer(Inlined inlined, RenderContext context)
	{
		_inlined = inlined;
		_context = context;
	}

	internal override void Compose(IContainer container)
	{
		if (!_context.IsVisible(_inlined))
			return;
		container.ApplyDecoration(_inlined.RuntimeStyle).Inlined(Compose);
	}

	public void Compose(InlinedDescriptor inl)
	{
		foreach (var ch in _inlined.Children)
		{
			inl.Item().Element(elem =>
			{
				ComposeElement(elem, ch);
			});
		}
	}

	void ComposeElement(IContainer container, FlowElement elem)
	{
		var comp = elem.CreateComposer(_context);
		comp.Compose(container);
	}
}
