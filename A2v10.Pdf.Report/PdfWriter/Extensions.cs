using A2v10.Pdf.Xaml;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2v10.Pdf
{
	internal static class Extensions
	{
		public static FlowElementComposer CreateComposer(this FlowElement elem, ExpandoObject data)
		{
			return elem switch
			{
				Table table => new TableComposer(table, data),
				_ => throw new InvalidOperationException($"There is no composer for {elem.GetType()}")
			};
		}
	}
}
