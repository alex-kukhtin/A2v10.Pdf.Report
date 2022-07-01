// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace A2v10.Pdf.Xaml;

[ContentProperty("Content")]
public class TableCell : XamlElement
{
	public Object? Content { get; init; }
}

public class TableCellCollection : List<TableCell>
{

}
