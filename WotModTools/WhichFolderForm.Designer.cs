namespace WotModTools {
	partial class WhichFolderForm {
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
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.ToWhichTextBox = new System.Windows.Forms.TextBox();
			this.FromWhichTextBox = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.ToWhichBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.FromWhichBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(42, 34);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "どのフォルダーに追加しますか";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(42, 109);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(140, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "どのフォルダーを追加しますか";
			this.label2.Click += new System.EventHandler(this.label1_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(158, 179);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(56, 18);
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(306, 124);
			this.button2.Margin = new System.Windows.Forms.Padding(2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(40, 19);
			this.button2.TabIndex = 1;
			this.button2.Text = "参照";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.FromWhichButton_Click);
			// 
			// ToWhichTextBox
			// 
			this.ToWhichTextBox.Location = new System.Drawing.Point(56, 48);
			this.ToWhichTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.ToWhichTextBox.Name = "ToWhichTextBox";
			this.ToWhichTextBox.Size = new System.Drawing.Size(232, 19);
			this.ToWhichTextBox.TabIndex = 2;
			// 
			// FromWhichTextBox
			// 
			this.FromWhichTextBox.Location = new System.Drawing.Point(56, 124);
			this.FromWhichTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.FromWhichTextBox.Name = "FromWhichTextBox";
			this.FromWhichTextBox.Size = new System.Drawing.Size(232, 19);
			this.FromWhichTextBox.TabIndex = 2;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(306, 49);
			this.button3.Margin = new System.Windows.Forms.Padding(2);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(40, 19);
			this.button3.TabIndex = 1;
			this.button3.Text = "参照";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.ToWhichButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(61, 12);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 12);
			this.label3.TabIndex = 3;
			this.label3.Text = "label3";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// ToWhichBrowserDialog
			// 
			this.ToWhichBrowserDialog.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.SystemColors.Control;
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(33, 85);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 12);
			this.label4.TabIndex = 4;
			this.label4.Text = "正しく設定してね";
			// 
			// WhichFolderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 208);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.FromWhichTextBox);
			this.Controls.Add(this.ToWhichTextBox);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "WhichFolderForm";
			this.Text = "WhichFolderForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox ToWhichTextBox;
		private System.Windows.Forms.TextBox FromWhichTextBox;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.FolderBrowserDialog ToWhichBrowserDialog;
		private System.Windows.Forms.FolderBrowserDialog FromWhichBrowserDialog;
		private System.Windows.Forms.Label label4;
	}
}