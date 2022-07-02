
using System;
using System.Dynamic;
using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

internal class RenderContext
{
	private readonly IReportLocalizer _localizer;
	public RenderContext(IReportLocalizer localizer, ExpandoObject model)
	{
		_localizer = localizer;
		DataModel = model;
		Engine = new ScriptEngine(model);
	}

	public ScriptEngine Engine { get; }
	public ExpandoObject DataModel { get; }

	public String ValueToString(Object value, String? format = null)
	{
		if (format != null)
		{
			if (value is DateTime dt)
			{
				return dt.ToString(format, _localizer.CurrentCulture.DateTimeFormat);
			}
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
					return ValueToString(val, contBind.Format);
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
