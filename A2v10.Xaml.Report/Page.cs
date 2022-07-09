﻿// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;

[ContentProperty("Columns")]
public class Page : XamlElement
{
	public String? Title { get; init; }
	public String? Code { get; init; }
	public ColumnCollection Columns { get; init; } = new ColumnCollection();

	public PageOrientation Orientation { get; init; }

	public override void ApplyStyles(String selector, StyleBag styles)
	{
		var sel = "Page";
		_runtimeStyle = styles.GetRuntimeStyle(sel);
		foreach (var col in Columns)
			col.ApplyStyles(sel, styles);
		ApplyStylesSelf();
	}
}
