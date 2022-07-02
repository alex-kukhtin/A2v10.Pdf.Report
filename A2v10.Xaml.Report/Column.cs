// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System.Collections.Generic;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;



[ContentProperty("Children")]
public class Column
{
	public FlowElementCollection Children { get; init; } = new FlowElementCollection();
}

public class ColumnCollection : List<Column>
{
}