// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System.Collections.Generic;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;

[ContentProperty("Cells")]
public class TableRow : XamlElement
{
	public TableCellCollection Cells { get; init; } = new TableCellCollection();
}

public class TableRowCollection : List<TableRow>
{
	public IEnumerable<TableCell> Cells()
	{
		foreach (var row in this)
			foreach (var c in row.Cells)
				yield return c;
	}
}
