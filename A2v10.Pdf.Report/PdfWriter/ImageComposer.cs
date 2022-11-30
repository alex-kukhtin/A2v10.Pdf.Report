﻿// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using A2v10.Xaml.Report;
using System.Dynamic;

namespace A2v10.Pdf.Report;

internal class ImageComposer : FlowElementComposer
{
	private readonly Image _image;
	private readonly RenderContext _context;

	public ImageComposer(Image image, RenderContext context)
	{
		_image = image;
		_context = context;
	}

	internal override void Compose(IContainer container, Object? value = null)
	{
		if (!_context.IsVisible(_image))
			return;
		container = container.ApplyDecoration(_image.RuntimeStyle);
		if (_context.IsVisible(_image))
		{
			Byte[]? stream = null;
			var rtBind = _image.GetBindRuntime("FileName");
			if (rtBind != null && rtBind.Expression != null)
			{
				String? fileName = null;
				var accessFunc = _context.Engine.CreateAccessFunction(rtBind.Expression);
				if (accessFunc != null && value is ExpandoObject eoValue)
					fileName = _context.Engine.Invoke(accessFunc, eoValue, rtBind?.Expression)?.ToString();
				else 
					fileName = _context.Engine.EvaluateValue(rtBind.Expression)?.ToString();
				if (fileName != null)
					stream = _context.GetFileAsByteArray(fileName);
			}
			else if (!String.IsNullOrEmpty(_image.FileName))
				stream = _context.GetFileAsByteArray(_image.FileName);
			else
				stream = _context.GetValueAsByteArray(_image, "Source");
			if (stream == null)
				return;
			if (_image.Width != null)
				container = container.Width(_image.Width.Value, _image.Width.Unit.ToUnit());
			if (_image.Height != null)
				container = container.Width(_image.Height.Value, _image.Height.Unit.ToUnit());
			container.Image(stream, ImageScaling.FitArea);
		}
	}
}
