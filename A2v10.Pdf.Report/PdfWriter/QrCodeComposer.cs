// Copyright © 2022-2024 Oleksandr Kukhtin. All rights reserved.

using System;

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using QRCoder;

using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

internal class QrCodeComposer : FlowElementComposer
{
	private readonly QrCode _code;
	private readonly RenderContext _context;

	public QrCodeComposer(QrCode code, RenderContext context)
	{
		_code = code;
		_context = context;
	}

	internal override void Compose(IContainer container, Object? value = null)
	{
		if (!_context.IsVisible(_code))
			return;
		container = container.ApplyDecoration(_code.RuntimeStyle);
		if (_context.IsVisible(_code))
		{
			var strCode = _context.GetValueAsString(_code, nameof(QrCode.Value));

			var gen = new QRCodeGenerator();
			var qrCodeData = gen.CreateQrCode(strCode, QRCodeGenerator.ECCLevel.Q);
			var pngCode = new PngByteQRCode(qrCodeData);
			var stream = pngCode.GetGraphic(20);

			if (_code.Size != null)
			{
				container = container.Width(_code.Size.Value, _code.Size.Unit.ToUnit());
				container = container.Height(_code.Size.Value, _code.Size.Unit.ToUnit());
			}
			container.Image(stream).FitArea();
		}
	}
}
