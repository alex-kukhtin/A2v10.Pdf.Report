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

	void ApplyRuntimeStyle(TextDescriptor descr)
	{
		var rs = _text.RuntimeStyle;
		if (rs == null)
			return;
		var ts = QuestPDF.Infrastructure.TextStyle.Default;
		if (rs.FontSize != null)
			ts = ts.FontSize(rs.FontSize.Value);
		if (rs.Bold != null && rs.Bold.Value)
			ts = ts.Bold();
		if (rs.Italic != null && rs.Italic.Value)
			ts = ts.Italic();
		descr.DefaultTextStyle(ts);
	}

	void ApplyRuntimeStyle(TextSpanDescriptor descr, ContentElement elem)
	{
		var rs = elem.RuntimeStyle;
		if (rs == null)
			return;
		if (rs.FontSize != null)
			descr = descr.FontSize(rs.FontSize.Value);
		if (rs.Bold != null && rs.Bold.Value)
			descr = descr.Bold();
		if (rs.Italic != null && rs.Italic.Value)
			descr = descr.Italic();
	}

	internal override void Compose(IContainer container)
	{
		if (!_context.IsVisible(_text))
			return;
		container.ApplyDecoration(_text.RuntimeStyle).Text(txt =>
		{
			//_context.ApplyTextStyle(txt, _text.Style);
			ApplyRuntimeStyle(txt);
			//txt.DefaultTextStyle(TextStyle.Default.FontSize(16F));
			for (var i = 0; i < _text.Inlines.Count; i++)
			{
				var elem = _text.Inlines[i];
				var val = _context.GetValueAsString(elem);
				if (val != null)
				{
					var res = txt.Span(val);
					if (elem is ContentElement contElem)
					{
						ApplyRuntimeStyle(res, contElem);
					}
				}
			}
		});
	}
}
