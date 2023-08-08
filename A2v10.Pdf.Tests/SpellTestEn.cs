
using A2v10.Pdf.Report;
using System;
using System.Collections.Generic;

using A2v10.Pdf.Report.Utils;
using System.Globalization;

namespace A2v10.Pdf.Tests;

[TestClass]
[TestCategory("Spell Simple (EN)")]
public class SpellTestEn
{
	[TestMethod]
	public void Simple()
	{
		var dict = new Dictionary<Decimal, String>()
		{
			{ 152,  "one hundred fifty-two" },
			{ 1000, "one thousand" },
			{ 1782, "one thousand seven hundred eighty-two"},
			{ 1,    "one" },
			{ 2,    "two"},
			{ 0,    "zero"},
			{ 219,  "two hundred nineteen"},
			{ 10,   "ten"},
			{ 18,   "eighteen"},
			{ 400,  "four hundred"},
            { 3437,  "three thousand four hundred thirty-seven"},
            { 83432,  "eighty-three thousand four hundred thirty-two"},
            { 1000000000,  "one billion"},
            { 1231200000002,  "one trillion two hundred thirty-one billion two hundred million two"},
            { 2000000,  "two million"},
			{ 7000000,  "seven million"},
			{ 1782529, "one million seven hundred eighty-two thousand five hundred twenty-nine"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.SpellEn(item.Key));
		}
	}

}