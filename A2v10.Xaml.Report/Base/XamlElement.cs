// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;

namespace A2v10.Xaml.Report;

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

	public BindRuntime? GetBindRuntime(String name)
	{
		return _bindImpl?.GetBindRuntime(name);
	}
	#endregion
}
