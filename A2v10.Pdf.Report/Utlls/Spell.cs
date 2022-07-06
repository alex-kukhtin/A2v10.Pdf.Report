// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;

using System.Globalization;
using System.Text;

namespace A2v10.Pdf.Report;


public static class SpellString
{
	private static String[] _hundreds = new String[10] {
		String.Empty,
		"сто ",
		"двісті ",
		"триста ",
		"чотириста ",
		"п’ятсот ",
		"шістсот ",
		"сімсот ",
		"вісімсот ",
		"дев’ятсот ",
	};

	private static String[] _tens = new string[10] {
		String.Empty,
		String.Empty,
		"двадцять ",
		"тридцять ",
		"сорок ",
		"п’ятдесят ",
		"шістдесят ",
		"сімдесят ",
		"вісімдесят ",
		"дев’яносто "
	};

	private static String[] _untisW = new String[3] {
		String.Empty,
		"одна ",
		"дві "
	};

	private static String[] _units = new String[20]
	{
		"нуль",
		"один ",
		"два ",
		"три ",
		"чотири ",
		"п’ять ",
		"шість ",
		"сім ",
		"вісім ",
		"девять ",
		"десять ",
		"одинадцять ",
		"двaнадцять ",
		"тринадцять ",
		"чотирнадцять ",
		"п’ятнадцять ",
		"шістнадцять ",
		"сімнадцять ",
		"вісімнадцять ",
		"дев’ятнадцять "
	};


	private static String[] _names = new String[15]
	{
		String.Empty,
		"тисяча ",
		"мільйон ",
		"мільяард ",
		"трильйон ",
		String.Empty,
		"тисячі ",
		"мільйона ",
		"мільярда ",
		"трильйона ",
		String.Empty,
		"тисяч ",
		"мільйонів ",
		"мільярдів ",
		"трильйонів "
	};

	private enum _SpellType {
		zero,
		one,
		two
	}

	static _SpellType[] _intTypes = new _SpellType[5] 
	{
		_SpellType.zero ,_SpellType.one,_SpellType.two,_SpellType.two,_SpellType.two,
	};


	public static String Spell(Decimal val)
	{
		var strPresentation = val.ToString("F2", CultureInfo.InvariantCulture);
		var vals = strPresentation.Split('.');
		String intPart = strPresentation;
		String fractPart = String.Empty;
		if (vals.Length == 2)
		{
			intPart = vals[0];
			fractPart = vals[1];
		}
		_SpellType type;
		return _spellNumber(intPart, out type);
	}

	private static String _spellNumber(String number, out _SpellType type)
	{
		type = _SpellType.zero;
		if (String.IsNullOrEmpty(number) || number == "0" || number == "00")
			return _units[0];

		Boolean woman = true;
		Int32 len = number.Length;

		StringBuilder sb = new StringBuilder();

		var cha = number.ToCharArray();
		Array.Reverse(cha);
		number = new String(cha);

		Int32 k = len / 3;
		switch (len % 3)
		{
			case 0:
				k--;
				break;
			case 1:
				number += "00";
				break;
			case 2:
				number += "0";
				break;
		}

		String suffix = String.Empty;
		for (int i = k; i >= 0; i--)
		{
			var three = number.Substring(i * 3, 3);
			if (three == "000")
				continue;
			int hundred = three[2] - '0';
			int ten = three[1] - '0';
			int unit = three[0] - '0';

			sb.Append(_hundreds[hundred]);
			if (ten >= 2)
				sb.Append(_tens[ten]);
			else if (ten == 1)
				unit += 10;
			suffix = _units[unit];
			if (i == 1 || (i == 0 && woman))
				if (unit < 3)
					suffix = _untisW[unit];
			if (unit < 5)
				type = _intTypes[unit];
			else
				type = _SpellType.zero;
			if (i == 1)
			{ // thouthands
				if (unit == 1)
					suffix = _untisW[1];
				else if (unit == 2)
					suffix = _untisW[2];
				type = 0; // 1000 (2000, 3000 еtc) UAH
			}
			sb.Append(suffix);

			switch (unit)
			{
				case 1:
					sb.Append(_names[i]);
					break;
				case 2:
				case 3:
				case 4:
					sb.Append(_names[i + 5]);
					break;
				default:
					sb.Append(_names[i + 10]);
					break;
			}
		}
		// remove last space
		if (sb[sb.Length - 1] == ' ')
			sb.Remove(sb.Length - 1, 1);
		return sb.ToString();
	}
}
