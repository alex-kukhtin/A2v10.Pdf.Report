﻿// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;

using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

internal static class Extensions
{
	public static FlowElementComposer CreateComposer(this FlowElement elem, RenderContext context)
	{
		return elem switch
		{
			Table table => new TableComposer(table, context),
			Text text => new TextComposer(text, context),
			_ => throw new InvalidOperationException($"There is no composer for {elem.GetType()}")
		};
	}
}
