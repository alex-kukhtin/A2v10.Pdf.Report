
using System;
using System.Dynamic;
using System.Globalization;
using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

internal class RenderContext
{
	private readonly IReportLocalizer _localizer;
	private readonly CultureInfo _formatProvider;
	public RenderContext(IReportLocalizer localizer, ExpandoObject model)
	{
		_localizer = localizer;
		DataModel = model;
		Engine = new ScriptEngine(model);
		var  clone = _localizer.CurrentCulture.Clone();
		if (clone is CultureInfo cloneCI && cloneCI != null)
			_formatProvider = cloneCI;
		else
			throw new ArgumentNullException("Current culture");
		_formatProvider.NumberFormat.CurrencyGroupSeparator = "\u00A0";
		_formatProvider.NumberFormat.NumberGroupSeparator = "\u00A0";
	}

	public ScriptEngine Engine { get; }
	public ExpandoObject DataModel { get; }

	public String ValueToString(Object value, DataType dataType = DataType.String, String? format = null)
	{
		var ni = _localizer.CurrentCulture.Clone();

		if (!String.IsNullOrEmpty(format))
			return String.Format(_formatProvider, $"{{0:{format}}}", value);

		switch (dataType)
		{
			case DataType.Currency:
				return String.Format(_formatProvider, "{0:#,##0.00##}", value);
			case DataType.Number:
				return String.Format(_formatProvider, "{0:#,##0.########}", value);
			case DataType.Time:
				return String.Format(_formatProvider, "{0:T}", value);
			case DataType.Date:
				return String.Format(_formatProvider, "{0:d}", value);
			case DataType.DateTime:
				return String.Format(_formatProvider, "{0:g}", value);
		}
		return _localizer.Localize(value.ToString()) ?? String.Empty;
	}

	public String? GetValueAsString(Object value)
	{
		if (value == null)
			return null;
		if (value is String strElem)
			return _localizer.Localize(strElem);
		if (value is ContentElement contElem)
		{
			var contBind = contElem.GetBindRuntime("Content");
			if (contBind != null)
			{
				var val = Engine.EvaluateValue(contBind.Path);
				if (val != null)
					return ValueToString(val, contBind.DataType, contBind.Format);
			}
			else if (contElem.Content != null)
				return ValueToString(contElem.Content);
		}
		return null;
	}

	public void ApplyTextStyle(TextStyle textStyle)
	{
	}
}
