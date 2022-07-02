﻿// Copyright © 2022 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Dynamic;
using System.Collections.Generic;

using Jint.Native;

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Elements.Table;

using A2v10.Xaml.Report;

namespace A2v10.Pdf.Report;

internal enum CellKind
{
	Body, 
	Header,
	Footer
}

internal class TableComposer : FlowElementComposer
{
	private readonly Table _table;
	private readonly ScriptEngine _engine;

	private readonly Dictionary<TableCell, JsValue> _accessFuncs = new();

	public TableComposer(Table table, ScriptEngine engine)
	{
		_table = table;
		_engine = engine;
	}

	internal override void Compose(IContainer container)
	{
		container.Table(tblDescr =>
		{
			tblDescr.ColumnsDefinition(columns =>
			{
				if (_table.Columns.Count == 0)
					columns.RelativeColumn();
				else
					foreach (var cx in _table.Columns)
						columns.RelativeColumn();
			});

			if (_table.Header.Count != 0)
				tblDescr.Header(ComposeHeader);

			var isbind = _table.GetBindRuntime("ItemsSource");
			if (isbind != null && isbind.Path != null)
			{
				CreateAccessFunc(_table.Body);
				var coll = _engine.EvaluateCollection(isbind.Path);
				if (coll != null)
					foreach (var elem in coll)
						ComposeRowCollection(CellKind.Body, tblDescr, _table.Body, elem);
			}
			else
				ComposeRowCollection(CellKind.Body, tblDescr, _table.Body);

			// not footer! inside body
			ComposeRowCollection(CellKind.Footer, tblDescr, _table.Footer);
		});
	}


	void ComposeHeader(TableCellDescriptor header)
	{
		foreach (var cell in _table.Header.Cells())
			ComposeCell(CellKind.Header, cell, () => header.Cell());
	}

	private void CreateAccessFunc(TableRowCollection body)
	{
		foreach (var row in body)
		{
			foreach (var cell in row.Cells)
			{
				var cont = cell.GetBindRuntime("Content");
				if (cont != null && cont.Path != null)
				{
					var func = _engine.CreateAccessFunction(cont.Path);
					_accessFuncs.Add(cell, func);
				}
			}
		}
	}

	private void ComposeCell(CellKind kind, TableCell cell, Func<ITableCellContainer> createCell, ExpandoObject? data = null)
	{
		var cellCont = createCell();
		if (cell.RowSpan > 1)
			cellCont = cellCont.RowSpan(cell.RowSpan);
		if (cell.ColSpan > 1)
			cellCont = cellCont.ColumnSpan(cell.ColSpan);

		// TODO: style here
		var ci = cellCont.Border(.2F).Padding(2F);

		if (_accessFuncs.TryGetValue(cell, out var contentFunc))
		{
			var value = _engine.Invoke(contentFunc, data);
			ci.Text(value.ToString());
			return;
		}

		var cont = cell.GetBindRuntime("Content");
		if (cont != null)
		{
			var val = _engine.EvaluateValue(cont.Path);
			if (val != null)
				ci.Text(val.ToString());
		} 
		else if (cell.Content is FlowElement flowElem)
			flowElem.CreateComposer(_engine).Compose(ci);
		else if (cell.Content != null)
			ci.Text(cell.Content.ToString());
	}

	private void ComposeRowCollection(CellKind kind, TableDescriptor tbl, TableRowCollection body, ExpandoObject? data = null)
	{
		foreach (var cell in body.Cells())
			ComposeCell(kind, cell, () => tbl.Cell(), data);
	}
}
