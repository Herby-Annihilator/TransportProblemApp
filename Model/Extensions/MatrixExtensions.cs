using System;
using System.Collections.Generic;
using System.Text;

namespace TransportProblemApp.Model.Extensions
{
	public static class MatrixExtensions
	{
		public static T[][] CloneMatrix<T>(this T[][] matrix) where T : ICloneable
		{
			int size = matrix.GetLength(0);
			int columns;
			T[][] result = new T[size][];
			for (int i = 0; i < size; i++)
			{
				columns = matrix[i].Length;
				result[i] = new T[columns];
				for (int j = 0; j < columns; j++)
				{
					result[i][j] = (T)matrix[i][j].Clone();
				}
			}
			return result;
		}

		public static void FillBy<T>(this T[] arr, T obj)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = obj;
			}
		}
	}
}
