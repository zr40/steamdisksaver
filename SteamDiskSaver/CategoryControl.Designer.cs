namespace SteamDiskSaver
{
	internal partial class CategoryControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.NameLabel = new System.Windows.Forms.Label();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// NameLabel
			// 
			this.NameLabel.AutoSize = true;
			this.NameLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.NameLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.NameLabel.Location = new System.Drawing.Point(0, 0);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(38, 13);
			this.NameLabel.TabIndex = 0;
			this.NameLabel.Text = "Name";
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.AutoSize = true;
			this.DescriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DescriptionLabel.Location = new System.Drawing.Point(0, 13);
			this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.DescriptionLabel.MaximumSize = new System.Drawing.Size(530, 0);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(66, 13);
			this.DescriptionLabel.TabIndex = 1;
			this.DescriptionLabel.Text = "Description";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.checkBox1.Location = new System.Drawing.Point(0, 26);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Padding = new System.Windows.Forms.Padding(3);
			this.checkBox1.Size = new System.Drawing.Size(66, 23);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "Delete files matching this category";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// CategoryControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.NameLabel);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
			this.Name = "CategoryControl";
			this.Size = new System.Drawing.Size(66, 49);
			this.Load += new System.EventHandler(this.CategoryControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.CheckBox checkBox1;


	}
}
