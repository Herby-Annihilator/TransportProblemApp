using System;
using System.Collections.Generic;
using System.Text;
using TransportProblemApp.Model.Extensions;

namespace TransportProblemApp.Model
{
	/// <summary>
	/// По горизонтали - потребители
	/// По вертикали - поставщики
	/// </summary>
	public class TransportTable : ICloneable
	{
		public Cell[][] TariffMatrix { get; set; }
		public Cell[] NeedsRow { get; set; }
		public Cell[] StocksColumn { get; set; }
		public int FakeRow { get; private set; } = -1;
		public int FakeColumn { get; private set; } = -1;
		internal TransportTable(Cell[][] tariff, Cell[] needsRow, Cell[] stocksColumn)
		{
			NeedsRow = (Cell[])needsRow.Clone();
			StocksColumn = (Cell[])stocksColumn.Clone();
			TariffMatrix = tariff.CloneMatrix();
		}

		internal TransportTable(double[][] workingMatrix, double[] workingNeeds, double[] workingStocks)
		{
			TariffMatrix = new Cell[workingMatrix.GetLength(0)][];
			for (int i = 0; i < TariffMatrix.GetLength(0); i++)
			{
				TariffMatrix[i] = new Cell[workingMatrix[i].Length];
				for (int j = 0; j < TariffMatrix[i].Length; j++)
				{
					TariffMatrix[i][j] = new Cell(workingMatrix[i][j], false, double.NaN);
				}
			}
			NeedsRow = new Cell[workingNeeds.Length];
			for (int i = 0; i < NeedsRow.Length; i++)
			{
				NeedsRow[i] = new Cell(0, false, workingNeeds[i]);
			}
			StocksColumn = new Cell[workingStocks.Length];
			for (int i = 0; i < StocksColumn.Length; i++)
			{
				StocksColumn[i] = new Cell(0, false, workingStocks[i]);
			}
		}

		public void SetSpecifiedRowAsFake(int rowIndex)
		{
			for (int i = 0; i < TariffMatrix[rowIndex].Length; i++)
			{
				TariffMatrix[rowIndex][i].IsFake = true;
			}
			StocksColumn[rowIndex].IsFake = true;
			FakeRow = rowIndex;
			FakeColumn = -1;
		}
		public void SetSpecifiedColumnAsFake(int colIndex)
		{
			for (int i = 0; i < TariffMatrix.GetLength(0); i++)
			{
				TariffMatrix[i][colIndex].IsFake = true;
			}
			NeedsRow[colIndex].IsFake = true;
			FakeColumn = colIndex;
			FakeRow = -1;
		}

		public object Clone()
		{
			return new TransportTable(TariffMatrix.CloneMatrix(), (Cell[])NeedsRow.Clone(), (Cell[])StocksColumn.Clone());
		}
	}

	public class Cell : ICloneable
	{
		public double Tariff { get; set; } = 0;
		public bool IsFake { get; set; } = false;
		public double Value { get; set; } = double.NaN;

		public object Clone()
		{
			return new Cell(Tariff, IsFake, Value);
		}

		public Cell(double tariff, bool isFake, double value)
		{
			Tariff = tariff;
			IsFake = isFake;
			Value = value;
		}
	}
}
