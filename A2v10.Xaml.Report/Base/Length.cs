// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Globalization;
using System.Linq;

namespace A2v10.Xaml.Report;

public record Length
{
	public Single Value { get; init; } = 1;
	public String Unit { get; init; } = "fr";

	static String[] ValidLength = { "mm", "cm", "pt", "in", "fr" };

	public Boolean IsEmpty()
	{
		return Value == 0;
	}

	public static Length Empty()
	{
		return new Length() { Value = 0, Unit = "pt" };
	}

	public static Length FromString(String strVal)
	{
		strVal = strVal.Trim().ToLowerInvariant();
		if (strVal.Length < 3)
			strVal += "pt";
		if (strVal.Length >= 3)
		{
			var ext = strVal.Substring(strVal.Length - 2, 2);
			var val = strVal.Substring(0, strVal.Length - 2);
			if (ValidLength.Any(x => x == ext) && Single.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out Single snglVal))
				return new Length() { Value = snglVal, Unit = ext };
		}
		throw new XamlException($"Invalid length value '{strVal}'");
	}
}
