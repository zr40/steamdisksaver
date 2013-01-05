namespace SteamDiskSaver
{
	internal partial class MainForm
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
			System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
			System.Windows.Forms.Button button1;
			System.Windows.Forms.Button button2;
			System.Windows.Forms.Button button3;
			System.Windows.Forms.Button button4;
			System.Windows.Forms.Button button5;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label6;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label7;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.ColumnHeader columnHeader2;
			System.Windows.Forms.ColumnHeader columnHeader1;
			System.Windows.Forms.ColumnHeader columnHeader3;
			System.Windows.Forms.ColumnHeader columnHeader4;
			System.Windows.Forms.ColumnHeader columnHeader7;
			System.Windows.Forms.ColumnHeader columnHeader5;
			System.Windows.Forms.ColumnHeader columnHeader6;
			this.button6 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			button1 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			button3 = new System.Windows.Forms.Button();
			button4 = new System.Windows.Forms.Button();
			button5 = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.flowLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.AutoSize = true;
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(786, 0);
			flowLayoutPanel1.TabIndex = 1;
			// 
			// button1
			// 
			this.flowLayoutPanel2.SetFlowBreak(button1, true);
			button1.Location = new System.Drawing.Point(3, 29);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(142, 23);
			button1.TabIndex = 0;
			button1.Text = "Delete redistributables";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(this.DeleteRedistsClicked);
			// 
			// button2
			// 
			this.flowLayoutPanel2.SetFlowBreak(button2, true);
			button2.Location = new System.Drawing.Point(3, 109);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(142, 23);
			button2.TabIndex = 1;
			button2.Text = "Delete other unrelated";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(this.DeleteOthersClicked);
			// 
			// button3
			// 
			this.flowLayoutPanel2.SetFlowBreak(button3, true);
			button3.Location = new System.Drawing.Point(3, 202);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(142, 23);
			button3.TabIndex = 3;
			button3.Text = "Delete intros";
			button3.UseVisualStyleBackColor = true;
			button3.Click += new System.EventHandler(this.DeleteIntrosClicked);
			// 
			// button4
			// 
			this.flowLayoutPanel2.SetFlowBreak(button4, true);
			button4.Location = new System.Drawing.Point(3, 308);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(142, 23);
			button4.TabIndex = 5;
			button4.Text = "Delete language files";
			button4.UseVisualStyleBackColor = true;
			button4.Click += new System.EventHandler(this.DeleteNonEnglishClicked);
			// 
			// button5
			// 
			this.flowLayoutPanel2.SetFlowBreak(button5, true);
			button5.Location = new System.Drawing.Point(3, 375);
			button5.Name = "button5";
			button5.Size = new System.Drawing.Size(142, 23);
			button5.TabIndex = 9;
			button5.Text = "Delete all of these";
			button5.UseVisualStyleBackColor = true;
			button5.Click += new System.EventHandler(this.DeleteAllClicked);
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 147);
			label3.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(143, 52);
			label3.TabIndex = 2;
			label3.Text = "Deletes startup movies related to the game. Don\'t delete intros if you want to se" +
    "e them!";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(3, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(125, 26);
			label5.TabIndex = 6;
			label5.Text = "Deletes space-wasting redistributables.";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(3, 67);
			label6.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(135, 39);
			label6.TabIndex = 7;
			label6.Text = "Deletes files and startup movies unrelated to gameplay.";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(3, 240);
			label4.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(143, 65);
			label4.TabIndex = 4;
			label4.Text = "Deletes non-English language files. Don\'t delete language files if you use anothe" +
    "r language in-game!";
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(3, 346);
			label7.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(131, 26);
			label7.TabIndex = 8;
			label7.Text = "Deletes ALL THE THINGS mentioned above!";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 413);
			label1.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(143, 26);
			label1.TabIndex = 10;
			label1.Text = "Tells Steam to redownload deleted files.";
			// 
			// columnHeader2
			// 
			columnHeader2.Text = "AppId";
			// 
			// columnHeader1
			// 
			columnHeader1.Text = "Game name";
			columnHeader1.Width = 200;
			// 
			// columnHeader3
			// 
			columnHeader3.Text = "Size";
			// 
			// columnHeader4
			// 
			columnHeader4.Text = "Redistributables";
			columnHeader4.Width = 100;
			// 
			// columnHeader7
			// 
			columnHeader7.Text = "Other";
			// 
			// columnHeader5
			// 
			columnHeader5.Text = "Intros";
			// 
			// columnHeader6
			// 
			columnHeader6.Text = "Languages";
			columnHeader6.Width = 70;
			// 
			// button6
			// 
			this.flowLayoutPanel2.SetFlowBreak(this.button6, true);
			this.button6.Location = new System.Drawing.Point(3, 442);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(142, 23);
			this.button6.TabIndex = 11;
			this.button6.Text = "Restore deleted files";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.ValidateClicked);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3,
            columnHeader4,
            columnHeader5,
            columnHeader6,
            columnHeader7});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(636, 566);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.listView1.TabIndex = 4;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.HeaderClicked);
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.ItemSelected);
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(label5);
			this.flowLayoutPanel2.Controls.Add(button1);
			this.flowLayoutPanel2.Controls.Add(label6);
			this.flowLayoutPanel2.Controls.Add(button2);
			this.flowLayoutPanel2.Controls.Add(label3);
			this.flowLayoutPanel2.Controls.Add(button3);
			this.flowLayoutPanel2.Controls.Add(label4);
			this.flowLayoutPanel2.Controls.Add(button4);
			this.flowLayoutPanel2.Controls.Add(label7);
			this.flowLayoutPanel2.Controls.Add(button5);
			this.flowLayoutPanel2.Controls.Add(label1);
			this.flowLayoutPanel2.Controls.Add(this.button6);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel2.Enabled = false;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(636, 0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(150, 566);
			this.flowLayoutPanel2.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(786, 566);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.flowLayoutPanel2);
			this.Controls.Add(flowLayoutPanel1);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Steam Disk Saver";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button button6;
	}
}

