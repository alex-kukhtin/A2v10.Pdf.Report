// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;

public enum TableStyle
{
	Default,
	Details,
	Simple
}

[ContentProperty("Body")]
public class Table : FlowElement
{
	public Object? ItemsSource { get; init; }

	public TableColumnCollection Columns { get; init; } = new TableColumnCollection();

	public TableRowCollection Header { get; init; } = new TableRowCollection();

	public TableRowCollection Footer { get; init; } = new TableRowCollection();

	public TableRowCollection Body { get; init; } = new TableRowCollection();

	public TableStyle Style { get; init; }

	public override void ApplyStyles(string selector, StyleBag styles)
	{
		var sel = $"Table.{Style}";
		_runtimeStyle = styles.GetRuntimeStyle(sel);
		foreach (var h in Header)
			h.ApplyStyles(sel + ">Header", styles);
		foreach (var b in Body)
			b.ApplyStyles(sel + ">Body", styles);
		foreach (var f in Footer)
			f.ApplyStyles(sel + ">Footer", styles);
		ApplyStylesSelf();
	}
}
