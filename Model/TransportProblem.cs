using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TransportProblemApp.Model
{
	public class TransportProblem
	{
		private TransportTable _table;
		public TransportTable Table { private get => _table; set => _table = (TransportTable)value.Clone(); }
		public TransportProblem(TransportTable table)
		{
			Table = table;
		}
		public Answer GetSolution()
		{
			
		}

		private void FillTableWithTheMinimumElementMethod()
		{
			List<int> bannedRows = new List<int>();
			List<int> bannedCols = new List<int>();
			bool fakeRow = false, fakeCol = false;
			if (Table.FakeRow > -1)
			{
				bannedRows.Add(Table.FakeRow);
				fakeRow = true;
			}				
			else if (Table.FakeColumn > -1)
			{
				bannedCols.Add(Table.FakeColumn);
				fakeCol = true;
			}
			int minTariffRowIndex = -1, minTariffColIndex = -1;
			double minTariff;
			while (bannedRows.Count < Table.StocksColumn.Length || bannedCols.Count < Table.NeedsRow.Length)
			{
				if (fakeRow && bannedRows.Count == Table.StocksColumn.Length)
					bannedRows.Remove(Table.FakeRow);
				else if (fakeCol && bannedCols.Count == Table.NeedsRow.Length)
					bannedCols.Remove(Table.FakeColumn);
				minTariff = double.MaxValue;
				for (int i = 0; i < Table.StocksColumn.Length; i++) // по строкам
				{
					if (!bannedRows.Contains(i))
					{
						for (int j = 0; j < Table.NeedsRow.Length; j++)
						{
							if (!bannedCols.Contains(j))
							{
								if (Table.TariffMatrix[i][j].Tariff < minTariff)
								{
									minTariff = Table.TariffMatrix[i][j].Tariff;
									minTariffColIndex = j;
									minTariffRowIndex = i;
								}
							}
						}
					}

					if (Table.NeedsRow[minTariffColIndex].Value < Table.StocksColumn[minTariffRowIndex].Value)
					{
						Table.TariffMatrix[minTariffRowIndex][minTariffColIndex].Value = Table.NeedsRow[minTariffColIndex].Value;
						Table.StocksColumn[minTariffRowIndex].Value -= Table.NeedsRow[minTariffColIndex].Value;
						Table.NeedsRow[minTariffColIndex].Value = 0;						
					}
					else if (Table.NeedsRow[minTariffColIndex].Value > Table.StocksColumn[minTariffRowIndex].Value)
					{
						Table.TariffMatrix[minTariffRowIndex][minTariffColIndex].Value = Table.StocksColumn[minTariffRowIndex].Value;
						Table.NeedsRow[minTariffColIndex].Value -= Table.StocksColumn[minTariffRowIndex].Value;
						Table.StocksColumn[minTariffRowIndex].Value = 0;
					}
					else //  когда одновременное равенство по наличию и по потребностям
					{
						Table.TariffMatrix[minTariffRowIndex][minTariffColIndex].Value = Table.StocksColumn[minTariffRowIndex].Value;
						bannedRows.Add(minTariffRowIndex);  // то баним строку
						continue; // и идем в начало цикла
					}
					if (Table.NeedsRow[minTariffColIndex].Value <= double.Epsilon)
						bannedCols.Add(minTariffColIndex);
					if (Table.StocksColumn[minTariffRowIndex].Value <= double.Epsilon)
						bannedRows.Add(minTariffRowIndex);
				}
			}
		}

		private bool IsOptimal()
		{

		}

		public static TransportTable CreateTransportTable(double[][] tariffMatrix, double[] stocks, double[] needs)
		{
			CheckArguments(tariffMatrix, stocks, needs);
			int rowsCount = tariffMatrix.GetLength(0);
			int colsCount = tariffMatrix[0].Length;
			bool fakeConsumerAdded = false, fakeProviderAdded = false;
			double[][] workingMatrix = tariffMatrix;
			double[] workingNeeds = needs;
			double[] workingStocks = stocks;
			double needsSum = needs.Sum();
			double stocksSum = stocks.Sum();
			if (needsSum < stocksSum)
			{
				fakeConsumerAdded = true;
				workingMatrix = new double[rowsCount][];
				for (int i = 0; i < rowsCount; i++)
				{
					workingMatrix[i] = new double[colsCount + 1];
					tariffMatrix[i].CopyTo(workingMatrix[i], 0);
				}
				workingNeeds = new double[colsCount + 1];
				needs.CopyTo(workingNeeds, 0);
				workingNeeds[colsCount] = stocksSum - needsSum;
			}
			else if (needsSum > stocksSum)
			{
				fakeProviderAdded = true;
				workingMatrix = new double[rowsCount + 1][];
				for (int i = 0; i < rowsCount; i++)
				{
					workingMatrix[i] = new double[colsCount];
					tariffMatrix[i].CopyTo(workingMatrix[i], 0);
				}
				workingStocks = new double[rowsCount + 1];
				stocks.CopyTo(workingStocks, 0);
				workingStocks[rowsCount] = needsSum - stocksSum;
			}
			TransportTable table = new TransportTable(workingMatrix, workingNeeds, workingStocks);
			if (fakeProviderAdded)
			{
				table.SetSpecifiedRowAsFake(workingStocks.Length - 1);
			}
			else if (fakeConsumerAdded)
			{
				table.SetSpecifiedColumnAsFake(workingNeeds.Length - 1);
			}
			return table;
		}

		private static void CheckArguments(double[][] tariffMatrix, double[] stocks, double[] needs)
		{
			int rowsCount = tariffMatrix.GetLength(0);
			int colsCount = tariffMatrix[0].Length;
			if (needs.Length != colsCount)
				throw new ArgumentException("Неверное число элементов в строке 'Потребности'");
			if (stocks.Length != rowsCount)
				throw new ArgumentException("Неверное число элементов в столбце 'Запасы/Наличие'");
			for (int i = 0; i < rowsCount; i++)
			{
				if (tariffMatrix[i].Length != colsCount)
					throw new ArgumentException($"Различное число элементов в строках матрицы тарифов. Проблемная строка {i}");
			}
		}
	}
}
