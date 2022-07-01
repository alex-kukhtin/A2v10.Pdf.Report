// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Reflection;
using System.Windows.Markup;

namespace A2v10.Pdf.Xaml;

public class Bind : MarkupExtension, ISupportBinding
{
	private BindImpl? _bindImpl;

	public BindImpl BindImpl
	{
		get
		{
			if (_bindImpl == null)
				_bindImpl = new BindImpl();
			return _bindImpl;
		}
	}

	public Bind()
	{
	}

	public Bind(String path)
	{
		Path = path;	
	}

	public String? Path { get; init; }
	public String? Expression { get; init; }


	public override Object? ProvideValue(IServiceProvider serviceProvider)
	{
		if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget iTarget))
			return null;
		if (iTarget.TargetProperty is not PropertyInfo targetProp)
			return null;
		if (iTarget.TargetObject is not ISupportBinding targetObj)
			return null;
		targetObj.BindImpl.SetBinding(targetProp.Name, this);
		if (targetProp.PropertyType.IsValueType)
			return Activator.CreateInstance(targetProp.PropertyType);
		return null; // is object
	}

	public Bind? GetBinding(String name)
	{
		return _bindImpl?.GetBinding(name);
	}

	void SetBinding(String name, Bind bind)
	{
		BindImpl.SetBinding(name, bind);
	}
}
