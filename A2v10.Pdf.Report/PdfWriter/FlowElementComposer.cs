﻿// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using QuestPDF.Infrastructure;

namespace A2v10.Pdf.Report;

internal abstract class FlowElementComposer
{
	internal abstract void Compose(IContainer container, Object? value = null);
}
