using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;

using A2v10.Data;
using A2v10.Pdf.Report;

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
		var loc = new NullDataLocalizer();
		var prof = new NullDataProfiler();
		var config = new SimpleDataConfiguration();

		var dbContext = new SqlDbContext(prof, config, loc);

		var dm = dbContext.LoadModel(null, "doc.[Document.Stock.Report]", new ExpandoObject()
		{
			{ "UserId", 99 },
			{ "Id", 481 }
		});

		var localizer = new MockLocalizer();

		var path = "C:/Git/A2v10.Pdf.Report/TestApp/Invoice.xaml";

		var outPath = "sample.pdf";

		using (var stream = File.OpenRead(path))
		{
			var builder = new PdfBuilder(localizer, stream, dm.Root);

			DeleteFile(outPath);

			//builder.Read();
			using (var outFile = File.OpenWrite(outPath))
			{
				builder.Build(outFile);
			}
		}
		Process.Start("explorer.exe", outPath);
	}
}