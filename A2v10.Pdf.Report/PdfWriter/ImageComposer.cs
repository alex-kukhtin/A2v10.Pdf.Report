// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using A2v10.Xaml.Report;
using QuestPDF.Helpers;

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

	void ApplyRuntimeStyle(TextSpanDescriptor descr, ContentElement elem)
	{
	}

	internal override void Compose(IContainer container)
	{
		container.ApplyDecoration(_image.RuntimeStyle).Image(Placeholders.Image(100, 100), ImageScaling.FitArea);
	}
}
