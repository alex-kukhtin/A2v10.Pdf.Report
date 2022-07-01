// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace A2v10.Pdf.Xaml;



[ContentProperty("Children")]
public class Column
{
	public FlowElementCollection Children { get; init; } = new FlowElementCollection();
}

public class ColumnCollection : List<Column>
{
}