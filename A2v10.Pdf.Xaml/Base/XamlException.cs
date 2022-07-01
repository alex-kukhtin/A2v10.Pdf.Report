// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;

namespace A2v10.Pdf.Xaml;

public sealed class XamlException : Exception
{
	public XamlException(String msg)
		: base(msg)
	{
	}
}
