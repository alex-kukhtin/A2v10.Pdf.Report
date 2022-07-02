// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;

[ContentProperty("Content")]
public class TableCell : ContentElement
{
	public UInt32 ColSpan { get; init; }
	public UInt32 RowSpan { get; init; }
}

public class TableCellCollection : List<TableCell>
{

}
