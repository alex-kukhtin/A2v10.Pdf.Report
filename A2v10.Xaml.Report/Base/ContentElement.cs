// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;


[ContentProperty("Content")]
public class ContentElement : XamlElement	
{
	public Object? Content { get; init; }
}


