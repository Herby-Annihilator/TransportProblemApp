using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TransportProblemApp.Model;

namespace TransportProblemApp
{
	public partial class TransportProblemForm : Form
	{
		private int _providersCount;
		private int _consumersCount;

		public TransportProblemForm(TransportProblem problem)
		{
			int providersCount = problem.Table.StocksColumn.Length;
			int consumersCount = problem.Table.NeedsRow.Length;
			InitializeComponent();
			_providersCount = providersCount;
			_consumersCount = consumersCount;
			CreateTariffTable(providersCount, consumersCount);
			for (int i = 0; i < providersCount; i++)
			{
				for (int j = 0; j < consumersCount; j++)
				{
					tariffMatrix.Rows[i].Cells[j].Value = problem.Table.TariffMatrix[i][j].Tariff;
				}
			}
			CreateStocksTable(providersCount);
			for (int i = 0; i < providersCount; i++)
			{
				stocks.Rows[i].Cells[0].Value = problem.Table.StocksColumn[i].Value;
			}
			CreateNeedsTable(consumersCount);
			for (int i = 0; i < consumersCount; i++)
			{
				needs.Rows[0].Cells[i].Value = problem.Table.NeedsRow[i].Value;
			}
		}

		public TransportProblemForm(int providersCount, int consumersCount)
		{
			InitializeComponent();
			_providersCount = providersCount;
			_consumersCount = consumersCount;
			CreateTariffTable(providersCount, consumersCount);
			CreateStocksTable(providersCount);
			CreateNeedsTable(consumersCount);
		}

		private void CreateTariffTable(int providers, int consumers)
		{

			tariffMatrix ??= new DataGridView();

			for (int i = 0; i < consumers; i++)
			{
				tariffMatrix.Columns.Add(new DataGridViewTextBoxColumn());
			}
			tariffMatrix.Rows.Add(providers);
		}
		private void CreateStocksTable(int providers)
		{
			stocks ??= new DataGridView();
			stocks.Columns.Add(new DataGridViewTextBoxColumn());
			stocks.Rows.Add(providers);
		}
		private void CreateNeedsTable(int consumers)
		{
			needs ??= new DataGridView();
			for (int i = 0; i < consumers; i++)
			{
				needs.Columns.Add(new DataGridViewTextBoxColumn());
			}
			needs.Rows.Add(1);
		}

		private void buttonSolveProblem_Click(object sender, EventArgs e)
		{
			try
			{
				labelOptimalPrice.Text = "";
				labelBasePrice.Text = "";
				double[][] tariffMatrixNumbers = new double[_providersCount][];
				for (int i = 0; i < _providersCount; i++)
				{
					tariffMatrixNumbers[i] = new double[_consumersCount];
					for (int j = 0; j < _consumersCount; j++)
					{
						tariffMatrixNumbers[i][j] = Convert.ToDouble(tariffMatrix.Rows[i].Cells[j].Value);
					}
				}
				double[] needsRow = new double[_consumersCount];
				for (int i = 0; i < _consumersCount; i++)
				{
					needsRow[i] = Convert.ToDouble(needs.Rows[0].Cells[i].Value);
				}
				double[] stocksRow = new double[_providersCount];
				for (int i = 0; i < _providersCount; i++)
				{
					stocksRow[i] = Convert.ToDouble(stocks.Rows[i].Cells[0].Value);
				}
				TransportProblem problem = new TransportProblem(TransportProblem.CreateTransportTable(tariffMatrixNumbers, stocksRow, needsRow));
				Answer answer = problem.GetSolution();
				CreateBasePlanTable(answer.BasePlan);
				CreateOptimalPlanTable(answer.OptimalPlan);
				labelBasePrice.Text = answer.BasePlanPrice.ToString();
				labelOptimalPrice.Text = answer.OptimalPlanPrice.ToString();
			}
			catch(Exception ee)
			{
				labelOptimalPrice.Text = $"Ошибка: {ee.Message}";
				labelBasePrice.Text = $"Ошибка: {ee.Message}";
			}
		}

		private void CreateBasePlanTable(double[][] basePlan)
		{
			int rowsCount = basePlan.GetLength(0);
			int colsCount = basePlan[0].Length;
			referenceSolution ??= new DataGridView();
			for (int i = 0; i < colsCount; i++)
			{
				referenceSolution.Columns.Add(new DataGridViewTextBoxColumn());
			}
			referenceSolution.Rows.Add(rowsCount);
			for (int i = 0; i < rowsCount; i++)
			{
				for (int j = 0; j < colsCount; j++)
				{
					referenceSolution.Rows[i].Cells[j].Value = basePlan[i][j];
				}
			}
		}

		private void CreateOptimalPlanTable(double[][] optimalPlan)
		{
			int rowsCount = optimalPlan.GetLength(0);
			int colsCount = optimalPlan[0].Length;
			optimalSolution ??= new DataGridView();
			for (int i = 0; i < colsCount; i++)
			{
				optimalSolution.Columns.Add(new DataGridViewTextBoxColumn());
			}
			optimalSolution.Rows.Add(rowsCount);
			for (int i = 0; i < rowsCount; i++)
			{
				for (int j = 0; j < colsCount; j++)
				{
					optimalSolution.Rows[i].Cells[j].Value = optimalPlan[i][j];
				}
			}
		}
	}
}
