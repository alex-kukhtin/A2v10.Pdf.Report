﻿// Copyright © 2022-2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Dynamic;

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using A2v10.Xaml.Report;
using SkiaSharp;

namespace A2v10.Pdf.Report;

internal class CheckboxComposer : FlowElementComposer
{
	private readonly Checkbox _checkbox;
	private readonly RenderContext _context;

	public CheckboxComposer(Checkbox checkbox, RenderContext context)
	{
		_checkbox = checkbox;
		_context = context;
	}

	internal override void Compose(IContainer container, Object? value = null)
	{
		if (!_context.IsVisible(_checkbox))
			return;
		// 1pt = 1.333(3) px
		/*
		Boolean val = GetCheckBoxValue(value);
		String checkMark = val ? "<polyline points='4,8 7,12 12,4' fill='none' stroke='black' stroke-width='2'/>" : String.Empty;
		String svgText = $"<svg viewBox='0 0 16 16'><rect x='0' y='0' width='16' height='16' fill='none' stroke-width='1.333' stroke='black'/>{checkMark}</svg>";
		var svgImage = SvgImage.FromText(svgText.Replace('\'', '"'));
		container.ApplyDecoration(_checkbox.RuntimeStyle)
			.Width(12, Unit.Point)
			.Height(12, Unit.Point)
			.Svg(svgImage);
		*/
		container.ApplyDecoration(_checkbox.RuntimeStyle)
			.Width(12, Unit.Point)
			.Height(12, Unit.Point)
		.Canvas((canvas, size) =>
		{
			using var borderPaint = new SKPaint()
			{
				Color = SKColors.Black,
				StrokeWidth = 1,
				IsStroke= true,
			};
			var rect = new SKRect(0, 0, size.Width, size.Height);
			canvas.DrawRect(rect, borderPaint);

			Boolean? val = GetCheckBoxValue(value);
			if (val != null && val.Value)
			{
				// draw mark
				using var markPaint = new SKPaint()
				{
					Color = SKColors.Black,
					StrokeWidth = 1.5F,
					IsStroke = true,
					StrokeMiter = 1,
					StrokeCap = SKStrokeCap.Round
				};
				rect.Inflate(-rect.Width / 4, -rect.Height / 4);
				SKPoint[] markPoints = new SKPoint[]
				{
					new (rect.Left, rect.Top + rect.Height / 2),
					new (rect.Left + rect.Width / 3, rect.Bottom),
					new (rect.Right, rect.Top)
				};

				canvas.DrawPoints(SKPointMode.Polygon, markPoints, markPaint);
			}
		});
	}

	Boolean GetCheckBoxValue(Object? scope)
	{
		var valBind = _checkbox.GetBindRuntime(nameof(_checkbox.Value));
		if (valBind != null && valBind.Expression != null)
		{
			var accessFunc = _context.Engine.CreateAccessFunction(valBind.Expression);
			if (accessFunc != null)
			{
				Object? res;
				if (scope is ExpandoObject eoScope)
					res = _context.Engine.Invoke(accessFunc, eoScope, valBind.Expression);
				else
					res = _context.Engine.EvaluateValue(valBind.Expression);
				if (res != null && res is Boolean resBool)
					return resBool;
			}
		}
		else if (_checkbox.Value != null)
		{
			return _checkbox.Value.Value;
		}
		return false;
	}
}
