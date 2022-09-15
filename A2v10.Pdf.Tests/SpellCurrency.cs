
using A2v10.Pdf.Report;
using System;
using System.Collections.Generic;

using A2v10.Pdf.Report.Utils;
using System.Globalization;

namespace A2v10.Pdf.Tests;

[TestClass]
[TestCategory("Spell Currency (UA)")]
public class SpellCurrency
{
	[TestMethod]
	public void SimpleUAH()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");

		var dict = new Dictionary<Decimal, String>()
		{
			{ 152,      "��� �������� �� ������ 00 ������" },
			{ 1000.01M, "���� ������ ������� 01 ������" },
			{ 1782.7M,  "���� ������ ����� �������� �� ������ 70 ������"},
			{ 1.02M,    "���� ������ 02 ������" },
			{ 2,        "�� ������ 00 ������"},
			{ 0.71M,    "���� ������� 71 ������"},
			{ 219,      "���� ������������ ������� 00 ������"},
			{ 10.05M,   "������ ������� 05 ������"},
			{ 18,   "³��������� ������� 00 ������"},
			{ 400,  "��������� ������� 00 ������"},
			{ 401,  "��������� ���� ������ 00 ������"},
			{ 402,  "��������� �� ������ 00 ������"},
			{ 2000000,  "��� ������� ������� 00 ������"},
			{ 1000000000,  "���� ������ ������� 00 ������"},
			{ 7000000,  "ѳ� �������� ������� 00 ������"},
			{ 1782529, "���� ������ ����� �������� �� ������ ������ �������� ������ ������� 00 ������"},
			{ 1782524, "���� ������ ����� �������� �� ������ ������ �������� ������ ������ 00 ������"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.SpellCurrency(item.Key, culture, "980"));
		}
	}

	[TestMethod]
	public void SimpleUSD()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");

		var dict = new Dictionary<Decimal, String>()
		{
			{ 152,      "��� �������� ��� ������ 00 �����" },
			{ 1000.01M, "���� ������ ������ 01 ����" },
			{ 1782.7M,  "���� ������ ����� �������� ��� ������ 70 �����"},
			{ 1.02M,    "���� ����� 02 �����" },
			{ 2,        "��� ������ 00 �����"},
			{ 0.71M,    "���� ������ 71 ����"},
			{ 219,      "���� ������������ ������ 00 �����"},
			{ 10.05M,   "������ ������ 05 �����"},
			{ 18,   "³��������� ������ 00 �����"},
			{ 400,  "��������� ������ 00 �����"},
			{ 401,  "��������� ���� ����� 00 �����"},
			{ 402,  "��������� ��� ������ 00 �����"},
			{ 2000000,  "��� ������� ������ 00 �����"},
			{ 1000000000,  "���� ������ ������ 00 �����"},
			{ 7000000,  "ѳ� �������� ������ 00 �����"},
			{ 1782529, "���� ������ ����� �������� �� ������ ������ �������� ������ ������ 00 �����"},
			{ 1782524, "���� ������ ����� �������� �� ������ ������ �������� ������ ������ 00 �����"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.SpellCurrency(item.Key, culture, "840"));
		}
	}

	[TestMethod]
	public void SimpleEUR()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");

		var dict = new Dictionary<Decimal, String>()
		{
			{ 152,      "��� �������� ��� ���� 00 �����" },
			{ 1000.01M, "���� ������ ���� 01 ����" },
			{ 1782.7M,  "���� ������ ����� �������� ��� ���� 70 �����"},
			{ 1.02M,    "���� ���� 02 �����" },
			{ 2,        "��� ���� 00 �����"},
			{ 0.71M,    "���� ���� 71 ����"},
			{ 219,      "���� ������������ ���� 00 �����"},
			{ 10.05M,   "������ ���� 05 �����"},
			{ 18,   "³��������� ���� 00 �����"},
			{ 400,  "��������� ���� 00 �����"},
			{ 401,  "��������� ���� ���� 00 �����"},
			{ 402,  "��������� ��� ���� 00 �����"},
			{ 2000000,  "��� ������� ���� 00 �����"},
			{ 1000000000,  "���� ������ ���� 00 �����"},
			{ 7000000,  "ѳ� �������� ���� 00 �����"},
			{ 1782529, "���� ������ ����� �������� �� ������ ������ �������� ������ ���� 00 �����"},
			{ 1782524, "���� ������ ����� �������� �� ������ ������ �������� ������ ���� 00 �����"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.SpellCurrency(item.Key, culture, "978"));
		}
	}

	[TestMethod]
	public void Test1()
	{
		var culture = CultureInfo.CreateSpecificCulture("uk-UA");
		Assert.AreEqual("��������� ������ 00 �����", SpellString.SpellCurrency(400, culture, "840"));
	}
}