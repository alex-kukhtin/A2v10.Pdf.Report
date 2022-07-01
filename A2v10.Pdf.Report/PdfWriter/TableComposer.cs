// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System.Dynamic;

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using A2v10.Pdf.Xaml;

namespace A2v10.Pdf;

internal class TableComposer : FlowElementComposer
{
	private readonly Table _table;
	private readonly ExpandoObject _data;
	public TableComposer(Table table, ExpandoObject data)
	{
		_table = table;
		_data = data;
	}

	internal override void Compose(IContainer container)
	{
		container.Table(tbl =>
		{
			tbl.ColumnsDefinition(columns =>
			{
				if (_table.Columns.Count == 0)
					columns.RelativeColumn();
				else
				{
					foreach (var cx in _table.Columns)
						columns.RelativeColumn();
				}
			});

			tbl.Cell().Border(.5F).AlignCenter().Text("Text 1");
			tbl.Cell().Border(.5F).AlignCenter().Text("Text 2");
			tbl.Cell().Border(.5F).AlignCenter().Text("Text 3");
		});
	}
}
