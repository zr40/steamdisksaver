namespace SteamDiskSaver
{
	internal partial class MainForm
	{
		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.ColumnHeader columnHeader2;
			System.Windows.Forms.ColumnHeader columnHeader1;
			System.Windows.Forms.ColumnHeader columnHeader3;
			System.Windows.Forms.ColumnHeader columnHeader4;
			System.Windows.Forms.ColumnHeader columnHeader5;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Button button2;
			System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
			System.Windows.Forms.ColumnHeader columnHeader6;
			this.button1 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			label5 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			label2 = new System.Windows.Forms.Label();
			button2 = new System.Windows.Forms.Button();
			flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			flowLayoutPanel2.SuspendLayout();
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
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(3, 67);
			label5.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(140, 26);
			label5.TabIndex = 6;
			label5.Text = "Deletes files according to the selected categories.";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 134);
			label1.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(143, 39);
			label1.TabIndex = 10;
			label1.Text = "Tells Steam to redownload deleted files for the highlighted game.";
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
			columnHeader4.Text = "Deletable";
			columnHeader4.Width = 95;
			// 
			// columnHeader5
			// 
			columnHeader5.Text = "Not selected";
			columnHeader5.Width = 85;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(3, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(144, 26);
			label2.TabIndex = 13;
			label2.Text = "Select which categories of files to delete.";
			// 
			// button2
			// 
			flowLayoutPanel2.SetFlowBreak(button2, true);
			button2.Location = new System.Drawing.Point(3, 29);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(142, 23);
			button2.TabIndex = 12;
			button2.Text = "Category selection";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// flowLayoutPanel2
			// 
			flowLayoutPanel2.Controls.Add(label2);
			flowLayoutPanel2.Controls.Add(button2);
			flowLayoutPanel2.Controls.Add(label5);
			flowLayoutPanel2.Controls.Add(this.button1);
			flowLayoutPanel2.Controls.Add(label1);
			flowLayoutPanel2.Controls.Add(this.button6);
			flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
			flowLayoutPanel2.Location = new System.Drawing.Point(636, 0);
			flowLayoutPanel2.Name = "flowLayoutPanel2";
			flowLayoutPanel2.Size = new System.Drawing.Size(150, 566);
			flowLayoutPanel2.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.Enabled = false;
			flowLayoutPanel2.SetFlowBreak(this.button1, true);
			this.button1.Location = new System.Drawing.Point(3, 96);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(142, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Delete!";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.DeleteFilesClicked);
			// 
			// button6
			// 
			this.button6.Enabled = false;
			flowLayoutPanel2.SetFlowBreak(this.button6, true);
			this.button6.Location = new System.Drawing.Point(3, 176);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(142, 23);
			this.button6.TabIndex = 11;
			this.button6.Text = "Restore deleted files";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.ValidateClicked);
			// 
			// columnHeader6
			// 
			columnHeader6.Text = "Saved";
			columnHeader6.Width = 85;
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3,
            columnHeader4,
            columnHeader5,
            columnHeader6});
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(786, 566);
			this.Controls.Add(this.listView1);
			this.Controls.Add(flowLayoutPanel2);
			this.Controls.Add(flowLayoutPanel1);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Steam Disk Saver";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			flowLayoutPanel2.ResumeLayout(false);
			flowLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button1;
	}
}

