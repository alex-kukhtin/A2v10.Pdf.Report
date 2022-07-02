// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

internal class TextComposer : FlowElementComposer
{
	private readonly Text _text;
	private readonly RenderContext _context;

	public TextComposer(Text text, RenderContext context)
	{
		_text = text;
		_context = context;
	}

	void ApplyStyles(ContentElement elem, TextSpanDescriptor textSpan)
	{
		textSpan.Bold();
	}

	internal override void Compose(IContainer container)
	{
		container.ApplyDecoration().Text(txt =>
		{
			//_context.ApplyTextStyle(txt, _text.Style);
			//txt.DefaultTextStyle(TextStyle.Default.FontSize(16F));
			for (var i = 0; i < _text.Inlines.Count; i++)
			{
				var elem = _text.Inlines[i];
				var val = _context.GetValueAsString(elem);
				if (val != null)
				{
					var res = txt.Span(val);
					if (elem is ContentElement contElem)
						ApplyStyles(contElem, res);
				}
			}
		});
	}
}
