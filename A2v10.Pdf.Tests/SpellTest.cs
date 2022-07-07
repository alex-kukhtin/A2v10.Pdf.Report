
using A2v10.Pdf.Report;
using System;
using System.Collections.Generic;

using A2v10.Pdf.Report.Utils;
using System.Globalization;

namespace A2v10.Pdf.Tests;

[TestClass]
[TestCategory("Spell Simple (UA)")]
public class SpellTest
{
	[TestMethod]
	public void Simple()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");

		var dict = new Dictionary<Decimal, String>()
		{
			{ 152, "сто пТ€тдес€т дв≥" },
			{ 1000, "одна тис€ча" },
			{ 1782, "одна тис€ча с≥мсот в≥с≥мдес€т дв≥"},
			{ 1,    "одна" },
			{ 2,    "дв≥"},
			{ 0,    "нуль"},
			{ 219,  "дв≥ст≥ девТ€тнадц€ть"},
			{ 10,   "дес€ть"},
			{ 18,   "в≥с≥мнадц€ть"},
			{ 400,  "чотириста"},
			{ 1000000000,  "один м≥ль€рд"},
			{ 2000000,  "два м≥льйона"},
			{ 7000000,  "с≥м м≥льйон≥в"},
			{ 1782529, "один м≥льйон с≥мсот в≥с≥мдес€т дв≥ тис€ч≥ пТ€тсот двадц€ть девТ€ть"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.Spell(item.Key, culture, SpellGender.Female));
		}
	}

	[TestMethod]
	public void Test1()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");
		Assert.AreEqual("чотириста", SpellString.Spell(400, culture, SpellGender.Female));
	}

	[TestMethod]
	public void SimpleFemale()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");

		var dict = new Dictionary<Decimal, String>()
		{
			{ 142,  "сто сорок дв≥"},
			{ 1_002_001,  "один м≥льйон дв≥ тис€ч≥ одна"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.Spell(item.Key, culture, SpellGender.Female));
		}
	}

	[TestMethod]
	public void SimpleMale()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");

		var dict = new Dictionary<Decimal, String>()
		{
			{ 1,  "один"},
			{ 142,  "сто сорок два"},
			{ 1_002_001,  "один м≥льйон дв≥ тис€ч≥ один"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.Spell(item.Key, culture, SpellGender.Male));
		}
	}

	[TestMethod]
	public void SimpleNeutral()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");

		var dict = new Dictionary<Decimal, String>()
		{
			{ 1,  "одне"},
			{ 142,  "сто сорок два"},
			{ 1_002_001,  "один м≥льйон дв≥ тис€ч≥ одне"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.Spell(item.Key, culture, SpellGender.Neutral));
		}
	}
}