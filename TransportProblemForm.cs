using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TransportProblemApp
{
	public partial class TransportProblemForm : Form
	{
		private int _providersCount;
		private int _consumersCount;
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
	}
}
