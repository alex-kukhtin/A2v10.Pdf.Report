﻿
using System;
using System.Dynamic;
using System.Globalization;
using System.IO;
using A2v10.Infrastructure;
using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

internal class RenderContext
{
	private readonly IReportLocalizer _localizer;
	private readonly CultureInfo _formatProvider;
	private readonly String _rootPath;
	private readonly String _templatePath;
	public RenderContext(String rootPath, String templatePath, IReportLocalizer localizer, ExpandoObject model, String? code)
	{
		_localizer = localizer;
		_rootPath = rootPath;
		_templatePath = templatePath;
		DataModel = model;
		var  clone = _localizer.CurrentCulture.Clone();
		if (clone is CultureInfo cloneCI && cloneCI != null)
			_formatProvider = cloneCI;
		else
			throw new ArgumentNullException("Current culture");
		_formatProvider.NumberFormat.CurrencyGroupSeparator = "\u00A0";
		_formatProvider.NumberFormat.NumberGroupSeparator = "\u00A0";

		Engine = new ScriptEngine(model, _formatProvider, code);
	}

	public ScriptEngine Engine { get; }
	public ExpandoObject DataModel { get; }

	public String ValueToString(Object? value, DataType dataType = DataType.String, String? format = null)
	{
		if (value == null)
			return String.Empty;

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

	public Byte[]? GetFileAsByteArray(String? fileName)
	{
		if (String.IsNullOrEmpty(fileName))
			return null;
		if (Path.IsPathRooted(fileName))
			throw new InvalidDataException("Invalid path. The path must be relative");
		var templDir = Path.GetDirectoryName(_templatePath);
		var fullPath = Path.GetFullPath(Path.Combine(templDir, fileName));
		var pathDir = Path.GetDirectoryName(fullPath);
		if (!pathDir.StartsWith(_rootPath, StringComparison.InvariantCultureIgnoreCase))
			throw new InvalidDataException("Invalid path. You can place files in the application folder only.");
		return File.ReadAllBytes(fullPath);
	}

	public Byte[]? GetValueAsByteArray(Object value, String propertyName)
	{
		if (value == null)
			return null;
		if (value is not XamlElement xamlElem)
			return null;
		var bindRuntime = xamlElem.GetBindRuntime(propertyName);
		if (bindRuntime == null || bindRuntime.Expression == null)
			return null;
		var lastDot = bindRuntime.Expression.LastIndexOf('.');
		if (lastDot == -1)
			return null;
		var objVal = Engine.EvaluateValue(bindRuntime.Expression.Substring(0, lastDot));
		if (objVal == null || objVal is not ExpandoObject eoVal)
			return null;
		return eoVal.Get<Byte[]>(bindRuntime.Expression.Substring(lastDot + 1));
	}

	public String? GetValueAsString(Object value, String propertyName = "Content")
	{
		var x = _getValueAsString(value, propertyName);
		if (x == null)
			return x;
		return x.Replace("\\n", "\n");
	}

	private String? _getValueAsString(Object value, String propertyName)
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
				var val = Engine.EvaluateValue(contBind.Expression);
				if (val != null)
					return ValueToString(val, contBind.DataType, contBind.Format);
			}
			else if (contElem.Content != null)
				return ValueToString(contElem.Content);
		}
		else if (value is XamlElement xamlElem)
		{
			var contBind = xamlElem.GetBindRuntime(propertyName);
			if (contBind != null)
			{
				var val = Engine.EvaluateValue(contBind.Expression);
				if (val != null)
					return ValueToString(val, contBind.DataType, contBind.Format);
			}
		}
		return null;
	}

	public Boolean IsVisible(XamlElement elem)
	{
		var ifbind = elem.GetBindRuntime("If");
		if (ifbind == null)
		{
			if (elem.If != null && !elem.If.Value)
				return false;
			return true;
		}
		var val = Engine.EvaluateValue(ifbind.Expression);
		if (val is Boolean boolVal)
			return boolVal;
		return true;
	}

	public void ApplyTextStyle(TextStyle textStyle)
	{
	}
}
