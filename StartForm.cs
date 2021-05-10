using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

		}
	}
}
