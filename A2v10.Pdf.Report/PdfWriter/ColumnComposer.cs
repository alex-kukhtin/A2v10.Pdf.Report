// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System.Dynamic;

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using A2v10.Pdf.Xaml;

namespace A2v10.Pdf;

internal class ColumnComposer
{
	private readonly Column _column;
	private readonly ExpandoObject _data;

	internal ColumnComposer(Column column, ExpandoObject data)
	{
		_column = column;
		_data = data;
	}

	public void Compose(ColumnDescriptor descriptor)
	{
		foreach (var ch in _column.Children)
		{
			descriptor.Item().Element(cont =>
			{
				ComposeElement(cont, ch);
			});
		}
	}

	void ComposeElement(IContainer container, FlowElement elem)
	{
		var comp = elem.CreateComposer(_data);
		comp.Compose(container);
	}
}
