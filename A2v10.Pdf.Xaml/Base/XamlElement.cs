// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;

namespace A2v10.Pdf.Xaml;

public class XamlElement : ISupportBinding
{
	BindImpl? _bindImpl;

	#region ISupportBinding
	public BindImpl BindImpl
	{
		get
		{
			if (_bindImpl == null)
				_bindImpl = new BindImpl();
			return _bindImpl;
		}
	}

	public Bind? GetBinding(String name)
	{
		return _bindImpl?.GetBinding(name);
	}
	#endregion
}
