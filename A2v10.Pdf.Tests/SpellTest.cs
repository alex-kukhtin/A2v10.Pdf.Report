using A2v10.Pdf.Report;
using System;
using System.Collections.Generic;

namespace A2v10.Pdf.Tests;

[TestClass]
[TestCategory("Spell Money (UA)")]
public class SpellTest
{
	[TestMethod]
	public void Simple()
	{
		var dict = new Dictionary<Decimal, String>()
		{
			{ 152, "сто пТ€тдес€т дв≥" },
			{ 1000, "одна тис€ча" },
			{ 1782, "одна тис€ча с≥мсот в≥с≥мдес€т дв≥"},
			{ 1,    "одна" },
			{ 2,    "дв≥"},
			{ 219,  "дв≥ст≥ девТ€тнадц€ть"},
			{ 10,   "дес€ть"},
			{ 400,  "чотириста"},
			{ 1782523, "один м≥льйон с≥мсот в≥с≥мдес€т дв≥ тис€ч≥ пТ€тсот двадц€ть три"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.Spell(item.Key));
		}
	}
}