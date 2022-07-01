using A2v10.Pdf.ReportBuilder;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;

namespace TestApp;

internal static class Program
{

	static void DeleteFile(String path)
	{
		try
		{
			File.Delete(path);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	static void Main(string[] args)
	{
		var eo = new ExpandoObject();
		var path = "C:/Git/A2v10.Pdf.Report/TestApp/Invoice.xaml";
		var builder = new PdfBuilder(path, eo);

		var outPath = "sample.pdf";
		DeleteFile(outPath);

		using (var outFile = File.OpenWrite(outPath))
		{
			builder.Build(outFile);
		}
		Process.Start("explorer.exe", outPath);
	}
}