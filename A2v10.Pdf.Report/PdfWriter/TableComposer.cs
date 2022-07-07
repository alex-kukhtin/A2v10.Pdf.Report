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
	private readonly RenderContext _context;

	private readonly Dictionary<TableCell, JsValue> _accessFuncs = new();

	public TableComposer(Table table, RenderContext context)
	{
		_table = table;
		_context = context;
	}

	internal override void Compose(IContainer container)
	{
		if (!_context.IsVisible(_table))
			return;
		container.ApplyDecoration(_table.RuntimeStyle).Table(tblDescr =>
		{
			tblDescr.ColumnsDefinition(columns =>
			{
				if (_table.Columns.Count == 0)
					columns.RelativeColumn();
				else
					foreach (var cx in _table.Columns)
						columns.TableColumn(cx);
			});

			if (_table.Header.Count != 0)
				tblDescr.Header(ComposeHeader);

			var isbind = _table.GetBindRuntime("ItemsSource");
			if (isbind != null && isbind.Expression != null)
			{
				CreateAccessFunc(_table.Body);
				var coll = _context.Engine.EvaluateCollection(isbind.Expression);
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
				if (cont != null && cont.Expression != null)
				{
					var func = _context.Engine.CreateAccessFunction(cont.Expression);
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

		DataType cellDataType = DataType.String;
		String? cellFormat = null;
		var bind = cell.GetBindRuntime("Content");
		if (bind != null)
		{
			cellDataType = bind.DataType;
			cellFormat = bind.Format;
		}

		var ci = cellCont.ApplyCellDecoration(cell.RuntimeStyle);

		if (!_context.IsVisible(cell))
			return;

		// TODO: style here
		// var ci = cellCont.Background("#f5f5f5").Border(.2F).Padding(2F);

		if (_accessFuncs.TryGetValue(cell, out var contentFunc))
		{
			var value = _context.Engine.Invoke(contentFunc, data);
			if (value != null)
				ci.Text(_context.ValueToString(value, cellDataType, cellFormat)).ApplyText(cell.RuntimeStyle);
			return;
		}

		if (cell.Content is FlowElement flowElem)
			flowElem.CreateComposer(_context).Compose(ci);
		else
		{
			var val = _context.GetValueAsString(cell);
			if (val != null)
			{
				ci.Text(val).ApplyText(cell.RuntimeStyle);
			}
		}
	}

	private void ComposeRowCollection(CellKind kind, TableDescriptor tbl, TableRowCollection body, ExpandoObject? data = null)
	{
		foreach (var row in body)
		{
			if (!_context.IsVisible(row))
				continue;
			foreach (var cell in row.Cells)
				ComposeCell(kind, cell, () => tbl.Cell(), data);
		}
	}
}
