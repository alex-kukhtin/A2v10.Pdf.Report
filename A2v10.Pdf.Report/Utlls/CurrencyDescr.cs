﻿// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using A2v10.Pdf.Report.Utils;
using System;
using System.Globalization;

namespace A2v10.Pdf.Report.Utils;

internal abstract class CurrencyDescr
{
	protected abstract CurrencyDef _descr { get; }

	public String NameCeil(SpellType unit)
	{
		return _descr.Ceils[(Int32) unit];
	}

	public String NameFract(SpellType unit)
	{
		return _descr.Fracts[(Int32) unit];
	}

	public SpellGender CeilGender => _descr.CeilGender;
	public SpellGender FractGender => _descr.FractGender;

	public static CurrencyDescr FromCulture(CultureInfo culture, String currencyCode)
	{
		return culture.TwoLetterISOLanguageName switch
		{
			"uk" => new CurrencyDescrUA(currencyCode),
			_ => throw new InvalidOperationException($"Spell for '{culture.Name}' yet not supported")
		};
	}
}

internal struct CurrencyDef
{
	public String[] Ceils { get; init; }
	public String[] Fracts { get; init; }
	public SpellGender CeilGender { get; init; }
	public SpellGender FractGender { get; init; }
}

internal class CurrencyDescrUA : CurrencyDescr
{
	CurrencyDef _current;
	private static CurrencyDef _uah_ua = new CurrencyDef()
	{
		Ceils = "гривень|гривня|гривні".Split('|'),
		CeilGender = SpellGender.Female,
		Fracts = "копійок|копійка|копійки".Split('|'),
		FractGender = SpellGender.Female
	};

	private static CurrencyDef _uah_usd = new CurrencyDef()
	{
		Ceils = "доларів|долар|долари".Split('|'),
		CeilGender = SpellGender.Male,
		Fracts = "центів|цент|цента".Split('|'),
		FractGender = SpellGender.Male
	};

	private static CurrencyDef _uah_eur = new CurrencyDef()
	{
		Ceils = "євро|євро|євро".Split('|'),
		CeilGender = SpellGender.Male,
		Fracts = "центів|цент|цента".Split('|'),
		FractGender = SpellGender.Male
	};

	public CurrencyDescrUA(String currencyCode)
	{
		_current = currencyCode switch
		{
			"980" => _uah_ua,
			"978" => _uah_eur,
			"840" => _uah_usd,
			_ => throw new ArgumentOutOfRangeException($"Currency descriptor for '{currencyCode}' yet not implemented")
		};
	}
	protected override CurrencyDef _descr => _current;
}

