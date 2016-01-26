namespace WotModTools {
	partial class AddModForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.FromFoldersLabel = new System.Windows.Forms.Label();
			this.OKButton = new System.Windows.Forms.Button();
			this.ToWhichTextBox = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.ToWhichBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.ModNameTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(54, 110);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "どのフォルダーに追加しますか";
			// 
			// FromFoldersLabel
			// 
			this.FromFoldersLabel.AutoSize = true;
			this.FromFoldersLabel.Location = new System.Drawing.Point(54, 98);
			this.FromFoldersLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.FromFoldersLabel.Name = "FromFoldersLabel";
			this.FromFoldersLabel.Size = new System.Drawing.Size(69, 12);
			this.FromFoldersLabel.TabIndex = 0;
			this.FromFoldersLabel.Text = "FromFolders";
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(160, 179);
			this.OKButton.Margin = new System.Windows.Forms.Padding(2);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(56, 18);
			this.OKButton.TabIndex = 1;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// ToWhichTextBox
			// 
			this.ToWhichTextBox.Location = new System.Drawing.Point(56, 124);
			this.ToWhichTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.ToWhichTextBox.Name = "ToWhichTextBox";
			this.ToWhichTextBox.Size = new System.Drawing.Size(232, 19);
			this.ToWhichTextBox.TabIndex = 2;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(292, 124);
			this.button3.Margin = new System.Windows.Forms.Padding(2);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(40, 19);
			this.button3.TabIndex = 1;
			this.button3.Text = "参照";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.ToWhichButton_Click);
			// 
			// ToWhichBrowserDialog
			// 
			this.ToWhichBrowserDialog.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
			// 
			// ModNameTextBox
			// 
			this.ModNameTextBox.HideSelection = false;
			this.ModNameTextBox.Location = new System.Drawing.Point(56, 28);
			this.ModNameTextBox.Name = "ModNameTextBox";
			this.ModNameTextBox.Size = new System.Drawing.Size(232, 19);
			this.ModNameTextBox.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(54, 13);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(129, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "Modに名前をつけてください";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// AddModForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 208);
			this.Controls.Add(this.ModNameTextBox);
			this.Controls.Add(this.ToWhichTextBox);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.FromFoldersLabel);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "AddModForm";
			this.Text = "Modを追加";
			this.Load += new System.EventHandler(this.AddModForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label3;
		protected System.Windows.Forms.TextBox ToWhichTextBox;
		protected System.Windows.Forms.TextBox ModNameTextBox;
		protected System.Windows.Forms.FolderBrowserDialog ToWhichBrowserDialog;
		protected System.Windows.Forms.Label FromFoldersLabel;
		private System.Windows.Forms.Button OKButton;
	}
}