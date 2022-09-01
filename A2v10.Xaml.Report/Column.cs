// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;



[ContentProperty("Children")]
public class Column : FlowElement
{
	public FlowElementCollection Children { get; init; } = new FlowElementCollection();

	public override void ApplyStyles(String selector, StyleBag styles)
	{
		var sel = selector + ">Column";
		_runtimeStyle = styles.GetRuntimeStyle(sel);
		foreach (var ch in Children)
			ch.ApplyStyles(sel, styles);
	}
}

public class ColumnCollection : List<Column>
{
}