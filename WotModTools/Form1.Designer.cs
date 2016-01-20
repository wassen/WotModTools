namespace WotModTools
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.consoleBox = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.listBox3 = new System.Windows.Forms.ListBox();
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.Location = new System.Drawing.Point(411, 385);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(165, 44);
			this.button1.TabIndex = 0;
			this.button1.Text = "VoiceModの適用";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.AudioApplyButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(202, 111);
			this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 18);
			this.label1.TabIndex = 3;
			this.label1.Text = "Voice Mod";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// consoleBox
			// 
			this.consoleBox.FormattingEnabled = true;
			this.consoleBox.ItemHeight = 18;
			this.consoleBox.Location = new System.Drawing.Point(192, 512);
			this.consoleBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.consoleBox.Name = "consoleBox";
			this.consoleBox.Size = new System.Drawing.Size(711, 112);
			this.consoleBox.TabIndex = 7;
			this.consoleBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 558);
			this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 18);
			this.label3.TabIndex = 8;
			this.label3.Text = "通知とかそんな感じ";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(682, 104);
			this.button3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(183, 34);
			this.button3.TabIndex = 10;
			this.button3.Text = "VoiceMod取り込み";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// listBox3
			// 
			this.listBox3.FormattingEnabled = true;
			this.listBox3.ItemHeight = 18;
			this.listBox3.Location = new System.Drawing.Point(296, 71);
			this.listBox3.Name = "listBox3";
			this.listBox3.Size = new System.Drawing.Size(378, 94);
			this.listBox3.TabIndex = 11;
			this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
			// 
			// checkedListBox1
			// 
			this.checkedListBox1.AllowDrop = true;
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new System.Drawing.Point(27, 155);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(261, 324);
			this.checkedListBox1.TabIndex = 12;
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.Control;
			this.button2.BackgroundImage = global::WotModTools.Properties.Resources.setting_icon;
			this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button2.Location = new System.Drawing.Point(873, 12);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(40, 40);
			this.button2.TabIndex = 13;
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.SettingButton_Click);
			// 
			// Form1
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(925, 662);
			this.Controls.Add(this.checkedListBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.listBox3);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.consoleBox);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox consoleBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ListBox listBox3;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
		private System.Windows.Forms.Button button2;
		private System.DirectoryServices.DirectoryEntry directoryEntry1;
	}
}

