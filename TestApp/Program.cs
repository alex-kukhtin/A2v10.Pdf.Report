﻿using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Reflection;
using A2v10.Data;
using A2v10.Pdf.Report;
using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;

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

	static void Main()
	{
		Settings.License = LicenseType.Community;

		var loc = new NullDataLocalizer();
		var prof = new NullDataProfiler();
		var config = new SimpleDataConfiguration();

		var dbContext = new SqlDbContext(prof, config, loc);

		var dm = dbContext.LoadModel(null, "doc.[Document.Stock.Report]", new ExpandoObject()
		//var dm = dbContext.LoadModel(null, "doc.[Offer.Report]", new ExpandoObject()
		{
			{ "UserId", 99 },
			//{ "Id", 354 }
			{ "Id", 328 }
		});

		var localizer = new MockLocalizer();

		//var path = "C:/A2v10_Net48/A2v10.Pdf.Report/TestApp/InvoiceImage.xaml";
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
			using var outFile = File.OpenWrite(outPath);

			Stopwatch sw = Stopwatch.StartNew();
			builder.Build(outFile);
			outFile.Close();

			Console.WriteLine($"Total time: {sw.ElapsedMilliseconds} ms");
		}
		using (var stream = File.OpenRead(path))
		{
			var builder = new PdfBuilder(Path.GetDirectoryName(path), localizer, path, stream, dm.Root);

			DeleteFile(outPath);

			//builder.Read();
			using var outFile = File.OpenWrite(outPath);

			Stopwatch sw = Stopwatch.StartNew();
			builder.Build(outFile);
			outFile.Close();

			Console.WriteLine($"Total time: {sw.ElapsedMilliseconds} ms");
		}
		Process.Start("explorer.exe", outPath);
	}
}