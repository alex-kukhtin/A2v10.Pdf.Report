// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Markup;

namespace A2v10.Pdf.Xaml;

[ContentProperty("Children")]
public class Report : XamlElement
{
	public String? Title { get; init; }
}
