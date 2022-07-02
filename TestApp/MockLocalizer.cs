
using System;
using System.Globalization;
using A2v10.Pdf.Report;

namespace TestApp
{
	internal class MockLocalizer : IReportLocalizer
	{
		public CultureInfo CurrentCulture => new CultureInfo("uk-UA");

		public String? Localize(String? content)
		{
			if (content != null && content.StartsWith("@["))
				return $"locailzed:{content}";
			return content;
		}
	}
}
