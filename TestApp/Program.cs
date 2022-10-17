using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Reflection;
using A2v10.Data;
using A2v10.Pdf.Report;
using QuestPDF.Drawing;

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
			{ "Id", 328 }
		});

		var localizer = new MockLocalizer();

		var path = "C:/A2v10_Net48/A2v10.Pdf.Report/TestApp/Invoice.xaml";

		var outPath = "c:\\temp\\sample.pdf";


		/*
		 * in report.pdf dll
		//var resName = "A2v10.Pdf.Report.Resources.OpenSansEmbed.ttf";
		//using Stream stx = Assembly.GetExecutingAssembly().GetManifestResourceStream(resName);
		//FontManager.RegisterFontWithCustomName("Roboto Flex", stx);
		//FontManager.RegisterFont(stx);
		*/

		//using Stream stx = Assembly.GetExecutingAssembly().GetManifestResourceStream("OpenSansEmbed");

		//FontManager.RegisterFontFromEmbeddedResource("OpenSansEmbed");
		//FontManager.RegisterFontFromEmbeddedResource("Lato");
		using (var stream = File.OpenRead(path))
		{
			var builder = new PdfBuilder(Path.GetDirectoryName(path), localizer, path, stream, dm.Root);

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