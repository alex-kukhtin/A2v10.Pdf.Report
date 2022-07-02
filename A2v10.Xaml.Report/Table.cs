// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;

[ContentProperty("Body")]
public class Table : FlowElement
{
	public Object? ItemsSource { get; init; }

	public TableColumnCollection Columns { get; init; } = new TableColumnCollection();

	public TableRowCollection Header { get; init; } = new TableRowCollection();

	public TableRowCollection Footer { get; init; } = new TableRowCollection();

	public TableRowCollection Body { get; init; } = new TableRowCollection();
}
