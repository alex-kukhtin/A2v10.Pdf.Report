
using System;
using System.Globalization;

namespace A2v10.Pdf.Report;

public interface IReportLocalizer
{
	String? Localize(String? content);
	CultureInfo CurrentCulture { get; }
}
