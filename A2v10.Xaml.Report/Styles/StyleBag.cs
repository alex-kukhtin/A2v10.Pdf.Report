// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;

namespace A2v10.Xaml.Report;

public class StyleBag
{
	public Dictionary<String, RuntimeStyle> _styles = new();

	public StyleBag()
	{
		_styles.Add("Text.Title", new RuntimeStyle()
		{
			FontSize = 16F,
			PaddingVertical = 10F
		});
		
		_styles.Add("Table.Details>Header>Row>Cell", new RuntimeStyle()
		{
			PaddingVertical = 2F,
			PaddingHorizontal = 4F,
			VAlign = VertAlign.Middle,
			Border = .2F,
			Background = "#f5f5f5"
		});

		_styles.Add("Table.Details>Body>Row>Cell", new RuntimeStyle()
		{
			PaddingVertical = 2F,
			PaddingHorizontal = 4F,
			Border = .2F,
		});

		_styles.Add("Table.Details>Footer>Row>Cell", new RuntimeStyle()
		{
			PaddingVertical = 2F,
			PaddingHorizontal = 4F,
			Border = .2F,
			Bold = true
		});


		_styles.Add("Table.Default>Body>Row>Cell", new RuntimeStyle()
		{
			Border = .2F,
			Padding = 5F
		});
	}

	public RuntimeStyle? GetRuntimeStyle(String selector)
	{
		Console.WriteLine(selector);
		return FindStyles(selector);
	}

	RuntimeStyle? FindStyles(String selector) 
	{ 
		foreach (var key in _styles.Keys)
		{
			if (selector.EndsWith(key))
			{
				return _styles[key].Clone();
			}
		}
		return null;
	}
}
