using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TransportProblemApp.Model
{
	public class TransportProblem
	{
		private TransportTable _table;
		public TransportTable Table { get => _table; set => _table = (TransportTable)value.Clone(); }
		public TransportProblem(TransportTable table)
		{
			Table = table;
		}
		public Answer GetSolution()
		{
			Answer answer = new Answer();
			FillTableWithTheMinimumElementMethod();
			answer.BasePlan = GetPlan();
			answer.BasePlanPrice = CalculatePrice(answer.BasePlan);
			answer.FakeColumn = Table.FakeColumn;
			answer.FakeRow = Table.FakeRow;
			OptimaizePlan();
			answer.OptimalPlan = GetPlan();
			answer.OptimalPlanPrice = CalculatePrice(answer.OptimalPlan);
			return answer;
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
						for (int j = 0; j < Table.NeedsRow.Length; j++)  // по столбцам
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
				}
				if (minTariff < double.MaxValue)
				{
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
						Table.StocksColumn[minTariffRowIndex].Value = 0;
						Table.NeedsRow[minTariffColIndex].Value = 0;
						if (!(bannedCols.Count + 1 == Table.NeedsRow.Length && bannedRows.Count + 1 == Table.StocksColumn.Length)) // если это не последний этап
						{
							bannedRows.Add(minTariffRowIndex);  // то баним строку
							continue; // и идем в начало цикла
						}

					}
					if (Table.NeedsRow[minTariffColIndex].Value <= double.Epsilon)
						bannedCols.Add(minTariffColIndex);
					if (Table.StocksColumn[minTariffRowIndex].Value <= double.Epsilon)
						bannedRows.Add(minTariffRowIndex);
				}
			}
		}

		private double[][] GetPlan()
		{
			double[][] result = new double[Table.TariffMatrix.GetLength(0)][];
			for (int i = 0; i < result.GetLength(0); i++)
			{
				result[i] = new double[Table.TariffMatrix[i].Length];
				for (int j = 0; j < result[i].Length; j++)
				{
					if (double.IsNaN(Table.TariffMatrix[i][j].Value))
						result[i][j] = 0;
					else
						result[i][j] = Table.TariffMatrix[i][j].Value;
				}
			}
			return result;
		}

		private void OptimaizePlan()
		{
			double[] consumerPotencials = new double[Table.NeedsRow.Length];
			double[] providerPotencials = new double[Table.StocksColumn.Length];
			Queue<int> rowsQueue = new Queue<int>();
			Queue<int> colsQueue = new Queue<int>();
			List<int> visitedRows = new List<int>();
			List<int> visitedCols = new List<int>();
			bool rowsQueueIsCurrent = true;
			rowsQueue.Enqueue(0);
			int currentRow = -1;
			int currentCol = -1;
			int fuckingRow = -1;
			int fuckingCol = -1;
			bool fuckingCellWasFound = true;
			while (fuckingCellWasFound)
			{
				fuckingCellWasFound = false;
				while (rowsQueue.Count > 0 || colsQueue.Count > 0) // расставляем потенциалы
				{
					if (rowsQueueIsCurrent)
					{
						currentCol = -1;
						currentRow = rowsQueue.Dequeue();
						visitedRows.Add(currentRow);
						if (rowsQueue.Count == 0)
							rowsQueueIsCurrent = false;
						for (int i = 0; i < Table.TariffMatrix[currentRow].Length; i++)
						{
							if (!visitedCols.Contains(i) && !double.IsNaN(Table.TariffMatrix[currentRow][i].Value)) // если в клетке не прочерк и тут еще не были
							{
								consumerPotencials[i] = Table.TariffMatrix[currentRow][i].Tariff - providerPotencials[currentRow];
								colsQueue.Enqueue(i);
							}
						}
					}
					else
					{
						currentRow = -1;
						currentCol = colsQueue.Dequeue();
						visitedCols.Add(currentCol);
						if (colsQueue.Count == 0)
							rowsQueueIsCurrent = true;
						for (int i = 0; i < Table.TariffMatrix.GetLength(0); i++)
						{
							if (!visitedRows.Contains(i) && !double.IsNaN(Table.TariffMatrix[i][currentCol].Value))// если в клетке не прочерк и в ней еще не были
							{
								providerPotencials[i] = Table.TariffMatrix[i][currentCol].Tariff - consumerPotencials[currentCol];
								rowsQueue.Enqueue(i);
							}
						}
					}
				}

				for (int i = 0; i < Table.StocksColumn.Length; i++)  // ищем ячейку, в которой не выполнено условие оптимальности плана
				{
					for (int j = 0; j < Table.NeedsRow.Length; j++)
					{
						if (double.IsNaN(Table.TariffMatrix[i][j].Value)) // в ячейке прочерк
						{
							if (providerPotencials[i] + consumerPotencials[j] > Table.TariffMatrix[i][j].Tariff)
							{
								fuckingRow = i;
								fuckingCol = j;
								fuckingCellWasFound = true;
								break;
							}
						}
					}
					if (fuckingCellWasFound)
						break;
				}

				if (fuckingCellWasFound)
				{
					List<Vertex> path = FindPath(fuckingRow, fuckingCol);
					double min = FindMinValueInPath(path);
					int sign;
					for (int i = 0; i < path.Count; i++)
					{
						if (i % 2 == 0)
							sign = 1;
						else
							sign = -1;
						if (double.IsNaN(Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value))
						{
							Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value = 0;
						}
						Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value += sign * min;
						if (Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value <= double.Epsilon && Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value >= 0) // Если получился ноль
						{
							if (i ==  path.Count - 1) // если это последний элемент пути, то нужно поставить прочерк, т.к. это выводит переменную из базиса
							{
								Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value = double.NaN;
							}
							else
							{
								Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value = 0;
							}
						}
					}
				}
			}			
		}

		private List<Vertex> FindPath(int fuckingRow, int fuckingCol)
		{
			List<Vertex> path = new List<Vertex>();
			int[] visitedRows = new int[Table.StocksColumn.Length];
			int[] visitedCols = new int[Table.NeedsRow.Length];
			Vertex currentVertex = new Vertex(fuckingRow, fuckingCol);
			Vertex nextVertex = null;
			List<Vertex> validRowVertexes;
			List<Vertex> validColVertexes;
			while(visitedCols[fuckingCol] < 2 || visitedRows[fuckingRow] < 2)
			{
				path.Add(currentVertex);
				visitedCols[currentVertex.ColIndex]++;
				visitedRows[currentVertex.RowIndex]++;
				nextVertex = null;
				if (visitedRows[currentVertex.RowIndex] < 2)
				{
					validRowVertexes = FindListOfValidVertexesInRow(currentVertex, visitedCols);
					if (validRowVertexes.Count > 0)
					{
						nextVertex = FindClosestVertexToSpecifiedInRow(currentVertex, validRowVertexes);
						currentVertex = nextVertex;
					}	
					else
					{
						visitedRows[currentVertex.RowIndex]++;
						visitedCols[currentVertex.ColIndex]--;
						// переход на предыдущий этап
						path.RemoveAt(path.Count - 1);
						currentVertex = path[path.Count - 1];
						path.RemoveAt(path.Count - 1);
						visitedRows[currentVertex.RowIndex]--;
						visitedCols[currentVertex.ColIndex]--;
					}
				}
				else if (visitedCols[currentVertex.ColIndex] < 2)
				{
					validColVertexes = FindListOfValidVertexesInCol(currentVertex, visitedRows);
					if (validColVertexes.Count > 0)
					{
						nextVertex = FindClosestVertexToSpecifiedInCol(currentVertex, validColVertexes);
						currentVertex = nextVertex;
					}
					else
					{
						visitedCols[currentVertex.ColIndex]++;
						visitedRows[currentVertex.RowIndex]--;
						// переход на предыдущий этап
						path.RemoveAt(path.Count - 1);
						currentVertex = path[path.Count - 1];
						path.RemoveAt(path.Count - 1);
						visitedRows[currentVertex.RowIndex]--;
						visitedCols[currentVertex.ColIndex]--;
					}
				}
			}
			return path;
		}

		private List<Vertex> FindListOfValidVertexesInRow(Vertex currentVertex, int[] visitedCols)
		{
			List<Vertex> result = new List<Vertex>();
			for (int i = 0; i < visitedCols.Length; i++)
			{
				if (!double.IsNaN(Table.TariffMatrix[currentVertex.RowIndex][i].Value) && visitedCols[i] < 2 && currentVertex.ColIndex != i)
				{
					result.Add(new Vertex(currentVertex.RowIndex, i));
				}
			}
			return result;
		}
		private Vertex FindClosestVertexToSpecifiedInRow(Vertex currentVertex, List<Vertex> validVertexes)
		{
			Vertex closestVertex = validVertexes[0];
			for (int i = 1; i < validVertexes.Count; i++)
			{
				if (Math.Abs(validVertexes[i].ColIndex - currentVertex.ColIndex) < Math.Abs(closestVertex.ColIndex - currentVertex.ColIndex)) // т.е. тут мы сравниваем расстояния между вершинами 
				{
					closestVertex = validVertexes[i];
				}
			}
			return closestVertex;
		}

		private List<Vertex> FindListOfValidVertexesInCol(Vertex currentVertex, int[] visitedRows)
		{
			List<Vertex> result = new List<Vertex>();
			for (int i = 0; i < visitedRows.Length; i++)
			{
				if (!double.IsNaN(Table.TariffMatrix[i][currentVertex.ColIndex].Value) && visitedRows[i] < 2 && currentVertex.RowIndex != i)
				{
					result.Add(new Vertex(i, currentVertex.ColIndex));
				}
			}
			return result;
		}
		private Vertex FindClosestVertexToSpecifiedInCol(Vertex currentVertex, List<Vertex> validVertexes)
		{
			Vertex closestVertex = validVertexes[0];
			for (int i = 1; i < validVertexes.Count; i++)
			{
				if (Math.Abs(validVertexes[i].RowIndex - currentVertex.RowIndex) < Math.Abs(closestVertex.RowIndex - currentVertex.RowIndex))
				{
					closestVertex = validVertexes[i];
				}
			}
			return closestVertex;
		}
		private double CalculatePrice(double[][] plan)
		{
			double result = 0;
			for (int i = 0; i < plan.GetLength(0); i++)
			{
				for (int j = 0; j < plan[i].Length; j++)
				{
					result += plan[i][j] * Table.TariffMatrix[i][j].Tariff;
				}
			}
			return result;
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
				workingMatrix[rowsCount] = new double[colsCount];
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

		private double FindMinValueInPath(List<Vertex> path)
		{
			double min = Table.TariffMatrix[path[1].RowIndex][path[1].ColIndex].Value; // не 0
			for (int i = 3; i < path.Count; i++)
			{
				if (Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value < min)
				{
					min = Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value;
				}
			}
			return min;
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

	public class Vertex
	{
		public Vertex(int rowIndex, int colIndex)
		{
			RowIndex = rowIndex;
			ColIndex = colIndex;
		}

		public int RowIndex { get; set; }
		public int ColIndex { get; set; }
		
	}
}
