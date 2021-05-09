﻿
namespace TransportProblemApp
{
	partial class StartForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonCreateProblem = new System.Windows.Forms.Button();
			this.buttonFromFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 41);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(124, 23);
			this.textBox1.TabIndex = 0;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(142, 41);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(119, 23);
			this.textBox2.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Число поставщиков";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(142, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Число потребителей";
			// 
			// buttonCreateProblem
			// 
			this.buttonCreateProblem.Location = new System.Drawing.Point(124, 72);
			this.buttonCreateProblem.Name = "buttonCreateProblem";
			this.buttonCreateProblem.Size = new System.Drawing.Size(140, 23);
			this.buttonCreateProblem.TabIndex = 4;
			this.buttonCreateProblem.Text = "Сформировать задачу";
			this.buttonCreateProblem.UseVisualStyleBackColor = true;
			this.buttonCreateProblem.Click += new System.EventHandler(this.buttonCreateProblem_Click);
			// 
			// buttonFromFile
			// 
			this.buttonFromFile.Location = new System.Drawing.Point(13, 71);
			this.buttonFromFile.Name = "buttonFromFile";
			this.buttonFromFile.Size = new System.Drawing.Size(105, 23);
			this.buttonFromFile.TabIndex = 5;
			this.buttonFromFile.Text = "Из файла";
			this.buttonFromFile.UseVisualStyleBackColor = true;
			this.buttonFromFile.Click += new System.EventHandler(this.buttonFromFile_Click);
			// 
			// StartForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(272, 107);
			this.Controls.Add(this.buttonFromFile);
			this.Controls.Add(this.buttonCreateProblem);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "StartForm";
			this.Text = "StartForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonCreateProblem;
		private System.Windows.Forms.Button buttonFromFile;
	}
}

