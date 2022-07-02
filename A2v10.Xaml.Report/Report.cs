// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;

[ContentProperty("Columns")]
public class Report : XamlElement
{
	public String? Title { get; init; }


	public ColumnCollection Columns { get; init; } = new ColumnCollection();
}
