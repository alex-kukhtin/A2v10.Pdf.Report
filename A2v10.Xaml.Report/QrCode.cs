// Copyright © 2022-2024 Oleksandr Kukhtin. All rights reserved.


using System;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;

[ContentProperty("Value")]
public class QrCode : FlowElement
{
	public Object? Value { get; set; }
	public Length? Size { get; set; }
}
