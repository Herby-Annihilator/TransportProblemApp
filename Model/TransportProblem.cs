using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TransportProblemApp.Model
{
	public class TransportProblem
	{
		public double[][] GetSolution()
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
