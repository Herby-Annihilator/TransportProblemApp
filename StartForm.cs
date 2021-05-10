using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransportProblemApp
{
	public partial class StartForm : Form
	{
		private TransportProblemForm transportProblem;
		public StartForm()
		{
			InitializeComponent();
		}

		private void buttonCreateProblem_Click(object sender, EventArgs e)
		{
			try
			{
				int providersCount = Convert.ToInt32(textBoxProviders.Text);
				int consumersCount = Convert.ToInt32(textBoxConsumers.Text);
				transportProblem = new TransportProblemForm(providersCount, consumersCount);
				transportProblem.ShowDialog();
				statusStrip1.Text = "Делаю";
			}
			catch(Exception ee)
			{
				statusStrip1.Text = ee.Message;
			}			
		}

		private void buttonFromFile_Click(object sender, EventArgs e)
		{
			try
			{
				StreamReader reader = new StreamReader("input.dat");
				string[] rows = reader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
				string[] numbers;
				double[][] matrix = new double[rows.Length - 1][];
				double[] needsRow;
				double[] stockCol = new double[rows.Length - 1];
				int cols = 0;
				for (int i = 0; i < rows.Length - 1; i++)
				{
					numbers = rows[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
					cols = numbers.Length - 1;
					matrix[i] = new double[cols];
					for (int j = 0; j < cols; j++)
					{
						matrix[i][j] = Convert.ToDouble(numbers[j]);
					}
					stockCol[i] = Convert.ToDouble(numbers[cols]);
				}
				needsRow = new double[cols];
				numbers = rows[rows.Length - 1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < cols; i++)
				{
					needsRow[i] = Convert.ToDouble(numbers[i]);
				}
				reader.Close();
				transportProblem = new TransportProblemForm(new Model.TransportProblem(Model.TransportProblem.CreateTransportTable(matrix, stockCol, needsRow)));
				transportProblem.ShowDialog();
			}
			catch(Exception ee)
			{
				statusStrip1.Text = ee.Message;
			}
		}
	}
}
