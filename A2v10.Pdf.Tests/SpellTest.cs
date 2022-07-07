
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
			{ 152, "��� �������� ��" },
			{ 1000, "���� ������" },
			{ 1782, "���� ������ ����� �������� ��"},
			{ 1,    "����" },
			{ 2,    "��"},
			{ 0,    "����"},
			{ 219,  "���� ������������"},
			{ 10,   "������"},
			{ 18,   "����������"},
			{ 400,  "���������"},
			{ 1000000000,  "���� ������"},
			{ 2000000,  "��� �������"},
			{ 7000000,  "�� �������"},
			{ 1782529, "���� ������ ����� �������� �� ������ ������ �������� ������"},
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
		Assert.AreEqual("���������", SpellString.Spell(400, culture, SpellGender.Female));
	}

	[TestMethod]
	public void SimpleFemale()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");

		var dict = new Dictionary<Decimal, String>()
		{
			{ 142,  "��� ����� ��"},
			{ 1_002_001,  "���� ������ �� ������ ����"},
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
			{ 1,  "����"},
			{ 142,  "��� ����� ���"},
			{ 1_002_001,  "���� ������ �� ������ ����"},
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
			{ 1,  "����"},
			{ 142,  "��� ����� ���"},
			{ 1_002_001,  "���� ������ �� ������ ����"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.Spell(item.Key, culture, SpellGender.Neutral));
		}
	}
}