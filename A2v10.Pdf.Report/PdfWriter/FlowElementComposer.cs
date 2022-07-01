using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2v10.Pdf;

internal abstract class FlowElementComposer
{
	internal abstract void Compose(IContainer container);
}
