// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;

namespace A2v10.Xaml.Report;


public class RuntimeStyle
{
	public Single? FontSize { get; set; }
	public Single? PaddingVertical { get; set; }
	public Single? PaddingHorizontal { get; set; }
	public Single? Padding { get; set; }
	public Single? Border { get; set; }
	public TextAlign? Align { get; set; }
	public VertAlign? VAlign { get; set; }
	public String? Background {get; set;}
	public Boolean? Bold { get; set; }

	public RuntimeStyle Clone()
	{
		return new RuntimeStyle()
		{
			FontSize = this.FontSize,
			PaddingVertical = this.PaddingVertical,
			PaddingHorizontal = this.PaddingHorizontal,
			Padding = this.Padding,
			Align = this.Align,
			VAlign = this.VAlign,
			Border = this.Border,
			Background = this.Background,
			Bold = this.Bold
		};
	}
}
