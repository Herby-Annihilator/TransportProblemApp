using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TransportProblemApp.Model.Extensions;

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
							bannedCols.Add(minTariffColIndex);
							AddZeroToUnallocatedColumnInRow(minTariffRowIndex); // в этой строке нужно добавить ноль в незанятый столбец
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

		private void AddZeroToUnallocatedColumnInRow(int rowIndex)
		{
			for (int i = 0; i < Table.TariffMatrix[rowIndex].Length; i++)
			{
				if (!IsThisColumnOccupied(i))
				{
					if (double.IsNaN(Table.TariffMatrix[rowIndex][i].Value))
					{
						Table.TariffMatrix[rowIndex][i].Value = 0;
						break;
					}
				}				
			}
			return;
		}
		/// <summary>
		/// Указывает, является ли заданный столбец занятым
		/// </summary>
		/// <param name="colIndex"></param>
		/// <returns></returns>
		private bool IsThisColumnOccupied(int colIndex) // столбец занят
		{
			if (Table.NeedsRow[colIndex].Value == 0)  // если потребителю ничего не нужно
			{
				for (int i = 0; i < Table.StocksColumn.Length; i++) // и при этом
				{
					if (!double.IsNaN(Table.TariffMatrix[i][colIndex].Value)) // в столбце должна 
						return true;  // находиться хотябы одна клетка плана
				}
			}
			return false;
		}

		private double[][] GetPlan()
		{
			double[][] result = new double[Table.TariffMatrix.GetLength(0)][];
			for (int i = 0; i < result.GetLength(0); i++)
			{
				result[i] = new double[Table.TariffMatrix[i].Length];
				for (int j = 0; j < result[i].Length; j++)
				{
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
			int currentRow = -1;
			int currentCol = -1;
			Vertex fuckingCell;
			do
			{
				rowsQueue.Enqueue(0);
				bool rowsQueueIsCurrent = true;
				visitedRows.Clear();
				visitedCols.Clear();
				consumerPotencials.FillBy(0);
				providerPotencials.FillBy(0);
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
				fuckingCell = FindFuckingCell(providerPotencials, consumerPotencials);
				if (fuckingCell != null)
				{
					List<Vertex> path = FindPath(fuckingCell.RowIndex, fuckingCell.ColIndex);
					Vertex vertex = FindVertexWithMinValueInPath(path);
					int sign;
					Table.TariffMatrix[path[0].RowIndex][path[0].ColIndex].Value = 0; // добро пожаловать в базис
					Table.TariffMatrix[vertex.RowIndex][vertex.ColIndex].Value = double.NaN; // вон из базиса
					for (int i = 0; i < path.Count; i++)
					{
						if (path[i].ColIndex != vertex.ColIndex || path[i].RowIndex != vertex.RowIndex)
						{
							if (i % 2 == 0)
								sign = 1;
							else
								sign = -1;

							Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value += sign * vertex.Value;
						}
					}
				}
			} while (fuckingCell != null); 			
		}

		private List<Vertex> FindPath(int fuckingRow, int fuckingCol)
		{
			var list = DeleteFromTableUnusefulCellsAndReturnTheList(new Vertex(fuckingRow, fuckingCol));

			List<Vertex> path = new List<Vertex>();
			int[] visitedRows = new int[Table.StocksColumn.Length];
			int[] visitedCols = new int[Table.NeedsRow.Length];
			Vertex currentVertex = new Vertex(fuckingRow, fuckingCol);
			List<Vertex> validRowVertexes;
			List<Vertex> validColVertexes;

			while(visitedCols[fuckingCol] < 2 || visitedRows[fuckingRow] < 2)
			{
				path.Add(currentVertex);
				visitedCols[currentVertex.ColIndex]++;
				visitedRows[currentVertex.RowIndex]++;
				if (visitedRows[currentVertex.RowIndex] < 2)
				{
					validRowVertexes = FindListOfValidVertexesInRow(currentVertex, visitedCols);
					if (validRowVertexes.Count > 0)
					{
						currentVertex = FindClosestVertexToSpecifiedInRow(currentVertex, validRowVertexes);
					}	
					//else
					//{
					//	visitedRows[currentVertex.RowIndex]++;
					//	visitedCols[currentVertex.ColIndex]--;
					//	// переход на предыдущий этап
					//	path.RemoveAt(path.Count - 1);
					//	currentVertex = path[path.Count - 1];
					//	path.RemoveAt(path.Count - 1);
					//	visitedRows[currentVertex.RowIndex]--;
					//	visitedCols[currentVertex.ColIndex]--;
					//}
				}
				else if (visitedCols[currentVertex.ColIndex] < 2)
				{
					validColVertexes = FindListOfValidVertexesInCol(currentVertex, visitedRows);
					if (validColVertexes.Count > 0)
					{
						currentVertex = FindClosestVertexToSpecifiedInCol(currentVertex, validColVertexes);
					}
					//else
					//{
					//	visitedCols[currentVertex.ColIndex]++;
					//	visitedRows[currentVertex.RowIndex]--;
					//	// переход на предыдущий этап
					//	path.RemoveAt(path.Count - 1);
					//	currentVertex = path[path.Count - 1];
					//	path.RemoveAt(path.Count - 1);
					//	visitedRows[currentVertex.RowIndex]--;
					//	visitedCols[currentVertex.ColIndex]--;
					//}
				}
			}

			RestoreTableDeletedCells(list);
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
					if (!double.IsNaN(plan[i][j]))
						result += plan[i][j] * Table.TariffMatrix[i][j].Tariff;
				}
			}
			return result;
		}

		private List<Vertex> DeleteFromTableUnusefulCellsAndReturnTheList(Vertex startCyclePoint)
		{
			List<Vertex> unusefuls = new List<Vertex>();
			Table.TariffMatrix[startCyclePoint.RowIndex][startCyclePoint.ColIndex].Value = 1;
			List<Vertex> currentUnusefuls = new List<Vertex>();
			do
			{
				currentUnusefuls.Clear();
				DeleteCells(0, 0, currentUnusefuls);
				unusefuls.AddRange(currentUnusefuls);
			} while (currentUnusefuls.Count > 0);
			Table.TariffMatrix[startCyclePoint.RowIndex][startCyclePoint.ColIndex].Value =  double.NaN;
			return unusefuls;
		}

		private void DeleteCells(int rowIndex, int colIndex, List<Vertex> unusefuls)
		{
			if (colIndex >= Table.NeedsRow.Length)
			{
				return;
			}
			if (rowIndex >= Table.StocksColumn.Length)
			{
				return;
			}
			DeleteCells(rowIndex, colIndex + 1, unusefuls);
			DeleteCells(rowIndex + 1, colIndex, unusefuls);
			if (!double.IsNaN(Table.TariffMatrix[rowIndex][colIndex].Value))
			{
				if (IsThisCellAloneInRow(rowIndex, colIndex) || IsThisCellAloneInColumn(rowIndex, colIndex))
				{
					unusefuls.Add(new Vertex(rowIndex, colIndex, Table.TariffMatrix[rowIndex][colIndex].Value));
					Table.TariffMatrix[rowIndex][colIndex].Value = double.NaN;
				}
			}
		}

		private bool IsThisCellAloneInRow(int rowIndex, int colIndex)
		{
			int count = 0;
			for (int i = 0; i < Table.NeedsRow.Length; i++)
			{
				if (i != colIndex)
					if (!double.IsNaN(Table.TariffMatrix[rowIndex][i].Value))
						count++;
			}
			return count == 0;
		}

		private bool IsThisCellAloneInColumn(int rowIndex, int colIndex)
		{
			int count = 0;
			for (int i = 0; i < Table.StocksColumn.Length; i++)
			{
				if (i != rowIndex)
					if (!double.IsNaN(Table.TariffMatrix[i][colIndex].Value))
						count++;
			}
			return count == 0;
		}

		private void RestoreTableDeletedCells(List<Vertex> unusefuls)
		{
			foreach (Vertex vertex in unusefuls)
			{
				Table.TariffMatrix[vertex.RowIndex][vertex.ColIndex].Value = vertex.Value;
			}
		}

		private Vertex FindFuckingCell(double[] providersPotencials, double[] consumersPotencials)
		{
			Vertex minCell = null;
			double[][] matrix = new double[Table.StocksColumn.Length][];
			for (int i = 0; i < Table.StocksColumn.Length; i++)
			{
				matrix[i] = new double[Table.NeedsRow.Length];
				for (int j = 0; j < Table.NeedsRow.Length; j++)
				{
					matrix[i][j] = providersPotencials[i] + consumersPotencials[j] - Table.TariffMatrix[i][j].Tariff;
				}
			}
			double min = 0;
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix[i].Length; j++)
				{
					if (matrix[i][j] > min)
					{
						min = matrix[i][j];
						minCell = new Vertex(i, j, min);
					}
				}
			}
			return minCell;
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

		private Vertex FindVertexWithMinValueInPath(List<Vertex> path)
		{
			Vertex vertex = path[1];
			double min = Table.TariffMatrix[path[1].RowIndex][path[1].ColIndex].Value; // не 0
			vertex.Value = min;
			for (int i = 3; i < path.Count; i++)
			{
				if (Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value < min)
				{
					vertex = path[i];
					min = Table.TariffMatrix[path[i].RowIndex][path[i].ColIndex].Value;
					vertex.Value = min;
				}
			}
			return vertex;
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
		public Vertex(int rowIndex, int colIndex, double value = double.NaN)
		{
			RowIndex = rowIndex;
			ColIndex = colIndex;
			Value = value;
		}

		public int RowIndex { get; set; }
		public int ColIndex { get; set; }
		public double Value { get; set; }
	}
}
