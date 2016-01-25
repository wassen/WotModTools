namespace WotModTools {
	partial class FormSetting {
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
			this.components = new System.ComponentModel.Container();
			this.WotVersionBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// wotVersionBox
			// 
			this.WotVersionBox.Location = new System.Drawing.Point(175, 25);
			this.WotVersionBox.Name = "wotVersionBox";
			this.WotVersionBox.Size = new System.Drawing.Size(144, 25);
			this.WotVersionBox.TabIndex = 0;
			this.toolTip1.SetToolTip(this.WotVersionBox, "asdff\\\\\\");
			this.WotVersionBox.TextChanged += new System.EventHandler(this.wotVersionBox_TextChanged);
			this.WotVersionBox.MouseEnter += new System.EventHandler(this.WotVersionBox_MouseEnter);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(40, 28);
			this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127, 18);
			this.label2.TabIndex = 5;
			this.label2.Text = "WOTのバージョン";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(122, 310);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(112, 32);
			this.button1.TabIndex = 6;
			this.button1.Text = "変更保存";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// toolTip1
			// 
			this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label1.Location = new System.Drawing.Point(14, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(328, 18);
			this.label1.TabIndex = 7;
			this.label1.Text = "注 Res_mods内のフォルダ名を入力して下さい";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// FormSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(353, 354);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.WotVersionBox);
			this.Name = "FormSetting";
			this.Text = "Form3";
			this.Load += new System.EventHandler(this.Form3_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox WotVersionBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label1;
	}
}