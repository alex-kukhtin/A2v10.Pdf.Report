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
			{ 152, "��� �������� ��" },
			{ 1000, "���� ������" },
			{ 1782, "���� ������ ����� �������� ��"},
			{ 1,    "����" },
			{ 2,    "��"},
			{ 219,  "���� ������������"},
			{ 10,   "������"},
			{ 400,  "���������"},
			{ 1782523, "���� ������ ����� �������� �� ������ ������ �������� ���"},
		};

		foreach (var item in dict)
		{
			Assert.AreEqual(item.Value, SpellString.Spell(item.Key));
		}
	}
}