
namespace TransportProblemApp
{
	partial class TransportProblemForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.optimalSolution = new System.Windows.Forms.DataGridView();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tariffMatrix = new System.Windows.Forms.DataGridView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.stocks = new System.Windows.Forms.DataGridView();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.needs = new System.Windows.Forms.DataGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonSolveProblem = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.referenceSolution = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelBasePrice = new System.Windows.Forms.Label();
			this.labelOptimalPrice = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.optimalSolution)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tariffMatrix)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.stocks)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.needs)).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.referenceSolution)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(974, 466);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.labelOptimalPrice);
			this.groupBox5.Controls.Add(this.label2);
			this.groupBox5.Controls.Add(this.optimalSolution);
			this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox5.Location = new System.Drawing.Point(3, 352);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(968, 111);
			this.groupBox5.TabIndex = 2;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Оптимальное решение";
			// 
			// optimalSolution
			// 
			this.optimalSolution.AllowUserToAddRows = false;
			this.optimalSolution.AllowUserToDeleteRows = false;
			this.optimalSolution.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.optimalSolution.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.optimalSolution.Location = new System.Drawing.Point(3, 19);
			this.optimalSolution.Name = "optimalSolution";
			this.optimalSolution.RowTemplate.Height = 25;
			this.optimalSolution.Size = new System.Drawing.Size(607, 85);
			this.optimalSolution.TabIndex = 2;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.20403F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.79597F));
			this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.groupBox2, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.groupBox3, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(968, 227);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tariffMatrix);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(741, 152);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Матрица тарифов";
			// 
			// tariffMatrix
			// 
			this.tariffMatrix.AllowUserToAddRows = false;
			this.tariffMatrix.AllowUserToDeleteRows = false;
			this.tariffMatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.tariffMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tariffMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tariffMatrix.Location = new System.Drawing.Point(3, 19);
			this.tariffMatrix.Name = "tariffMatrix";
			this.tariffMatrix.RowTemplate.Height = 25;
			this.tariffMatrix.Size = new System.Drawing.Size(735, 130);
			this.tariffMatrix.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.stocks);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(750, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(215, 152);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Наличие";
			// 
			// stocks
			// 
			this.stocks.AllowUserToAddRows = false;
			this.stocks.AllowUserToDeleteRows = false;
			this.stocks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.stocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.stocks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stocks.Location = new System.Drawing.Point(3, 19);
			this.stocks.Name = "stocks";
			this.stocks.RowTemplate.Height = 25;
			this.stocks.Size = new System.Drawing.Size(209, 130);
			this.stocks.TabIndex = 1;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.needs);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(3, 161);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(741, 63);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Потребности";
			// 
			// needs
			// 
			this.needs.AllowUserToAddRows = false;
			this.needs.AllowUserToDeleteRows = false;
			this.needs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.needs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.needs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.needs.Location = new System.Drawing.Point(3, 19);
			this.needs.Name = "needs";
			this.needs.RowTemplate.Height = 25;
			this.needs.Size = new System.Drawing.Size(735, 41);
			this.needs.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonSolveProblem);
			this.panel1.Location = new System.Drawing.Point(750, 161);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(209, 60);
			this.panel1.TabIndex = 3;
			// 
			// buttonSolveProblem
			// 
			this.buttonSolveProblem.Location = new System.Drawing.Point(65, 19);
			this.buttonSolveProblem.Name = "buttonSolveProblem";
			this.buttonSolveProblem.Size = new System.Drawing.Size(102, 23);
			this.buttonSolveProblem.TabIndex = 0;
			this.buttonSolveProblem.Text = "Решить задачу";
			this.buttonSolveProblem.UseVisualStyleBackColor = true;
			this.buttonSolveProblem.Click += new System.EventHandler(this.buttonSolveProblem_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.labelBasePrice);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.referenceSolution);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Location = new System.Drawing.Point(3, 236);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(968, 110);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Опорное решение";
			// 
			// referenceSolution
			// 
			this.referenceSolution.AllowUserToAddRows = false;
			this.referenceSolution.AllowUserToDeleteRows = false;
			this.referenceSolution.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.referenceSolution.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.referenceSolution.Location = new System.Drawing.Point(3, 19);
			this.referenceSolution.Name = "referenceSolution";
			this.referenceSolution.RowTemplate.Height = 25;
			this.referenceSolution.Size = new System.Drawing.Size(607, 84);
			this.referenceSolution.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(619, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(189, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Стоимость опорного плана (у.е.)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(619, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(215, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Стоимость оптимального плана (у.е.)";
			// 
			// labelBasePrice
			// 
			this.labelBasePrice.AutoSize = true;
			this.labelBasePrice.Location = new System.Drawing.Point(815, 56);
			this.labelBasePrice.Name = "labelBasePrice";
			this.labelBasePrice.Size = new System.Drawing.Size(0, 15);
			this.labelBasePrice.TabIndex = 3;
			// 
			// labelOptimalPrice
			// 
			this.labelOptimalPrice.AutoSize = true;
			this.labelOptimalPrice.Location = new System.Drawing.Point(841, 55);
			this.labelOptimalPrice.Name = "labelOptimalPrice";
			this.labelOptimalPrice.Size = new System.Drawing.Size(0, 15);
			this.labelOptimalPrice.TabIndex = 4;
			// 
			// TransportProblemForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(974, 466);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TransportProblemForm";
			this.Text = "TransportProblemForm";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.optimalSolution)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tariffMatrix)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.stocks)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.needs)).EndInit();
			this.panel1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.referenceSolution)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView tariffMatrix;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonSolveProblem;
		private System.Windows.Forms.DataGridView stocks;
		private System.Windows.Forms.DataGridView needs;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.DataGridView optimalSolution;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.DataGridView referenceSolution;
		private System.Windows.Forms.Label labelOptimalPrice;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelBasePrice;
		private System.Windows.Forms.Label label1;
	}
}