namespace WotModTools
{
    partial class MainForm
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
			this.ModList = new System.Windows.Forms.CheckedListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
			this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.Location = new System.Drawing.Point(247, 257);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(99, 30);
			this.button1.TabIndex = 0;
			this.button1.Text = "VoiceModの適用";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.AudioApplyButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(122, 74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "Voice Mod";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// consoleBox
			// 
			this.consoleBox.FormattingEnabled = true;
			this.consoleBox.ItemHeight = 12;
			this.consoleBox.Location = new System.Drawing.Point(116, 342);
			this.consoleBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.consoleBox.Name = "consoleBox";
			this.consoleBox.Size = new System.Drawing.Size(428, 76);
			this.consoleBox.TabIndex = 7;
			this.consoleBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 372);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(97, 12);
			this.label3.TabIndex = 8;
			this.label3.Text = "通知とかそんな感じ";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(410, 70);
			this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(110, 22);
			this.button3.TabIndex = 10;
			this.button3.Text = "VoiceMod取り込み";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// listBox3
			// 
			this.listBox3.FormattingEnabled = true;
			this.listBox3.ItemHeight = 12;
			this.listBox3.Location = new System.Drawing.Point(178, 47);
			this.listBox3.Margin = new System.Windows.Forms.Padding(2);
			this.listBox3.Name = "listBox3";
			this.listBox3.Size = new System.Drawing.Size(228, 64);
			this.listBox3.TabIndex = 11;
			this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
			// 
			// ModList
			// 
			this.ModList.AllowDrop = true;
			this.ModList.FormattingEnabled = true;
			this.ModList.Location = new System.Drawing.Point(16, 103);
			this.ModList.Margin = new System.Windows.Forms.Padding(2);
			this.ModList.Name = "ModList";
			this.ModList.Size = new System.Drawing.Size(135, 200);
			this.ModList.TabIndex = 12;
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.Control;
			this.button2.BackgroundImage = global::WotModTools.Properties.Resources.setting_icon;
			this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button2.Location = new System.Drawing.Point(524, 8);
			this.button2.Margin = new System.Windows.Forms.Padding(2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(24, 26);
			this.button2.TabIndex = 13;
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.SettingButton_Click);
			// 
			// fileSystemWatcher1
			// 
			this.fileSystemWatcher1.EnableRaisingEvents = true;
			this.fileSystemWatcher1.IncludeSubdirectories = true;
			this.fileSystemWatcher1.SynchronizingObject = this;
			this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(555, 442);
			this.Controls.Add(this.ModList);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.listBox3);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.consoleBox);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
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
		private System.Windows.Forms.CheckedListBox ModList;
		private System.Windows.Forms.Button button2;
		private System.DirectoryServices.DirectoryEntry directoryEntry1;
		private System.IO.FileSystemWatcher fileSystemWatcher1;
	}
}

