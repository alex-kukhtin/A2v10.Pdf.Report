﻿// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;

[ContentProperty("Cells")]
public class TableRow : XamlElement
{
	public TableCellCollection Cells { get; init; } = new TableCellCollection();

	public override void ApplyStyles(String selector, StyleBag styles)
	{
		var sel = selector + ">Row";
		_runtimeStyle = styles.GetRuntimeStyle(sel);
		foreach (var cell in Cells)
		{
			cell.ApplyStyles(sel, styles);
		}
	}
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
