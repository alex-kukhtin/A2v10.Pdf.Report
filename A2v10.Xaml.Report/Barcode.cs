// Copyright © 2024-2026 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Markup;

namespace A2v10.Xaml.Report;

public enum BarcodeType
{
    Ean13,
    Ean8
}

[ContentProperty("Value")]
public class Barcode : FlowElement
{
    public Object? Value { get; set; }
    public Length? Width { get; set; }
    public Int32 Height { get; set; }
    public BarcodeType Type { get; set; }
}
