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
			this.button3 = new System.Windows.Forms.Button();
			this.listBox3 = new System.Windows.Forms.ListBox();
			this.ModCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
			this.modsFolderFileSystemWatcher = new System.IO.FileSystemWatcher();
			this.consoleBox = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.modsFolderFileSystemWatcher)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.Location = new System.Drawing.Point(329, 321);
			this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(132, 38);
			this.button1.TabIndex = 0;
			this.button1.Text = "VoiceModの適用";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.AudioApplyButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(163, 92);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Voice Mod";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(547, 88);
			this.button3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(147, 28);
			this.button3.TabIndex = 10;
			this.button3.Text = "VoiceMod取り込み";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// listBox3
			// 
			this.listBox3.FormattingEnabled = true;
			this.listBox3.ItemHeight = 15;
			this.listBox3.Location = new System.Drawing.Point(237, 59);
			this.listBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.listBox3.Name = "listBox3";
			this.listBox3.Size = new System.Drawing.Size(303, 79);
			this.listBox3.TabIndex = 11;
			this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
			// 
			// ModCheckedListBox
			// 
			this.ModCheckedListBox.AllowDrop = true;
			this.ModCheckedListBox.FormattingEnabled = true;
			this.ModCheckedListBox.Location = new System.Drawing.Point(21, 129);
			this.ModCheckedListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ModCheckedListBox.Name = "ModCheckedListBox";
			this.ModCheckedListBox.Size = new System.Drawing.Size(179, 242);
			this.ModCheckedListBox.TabIndex = 12;
			this.ModCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.ModCheckedListBox_SelectedIndexChanged);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.Control;
			this.button2.BackgroundImage = global::WotModTools.Properties.Resources.setting_icon;
			this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button2.Location = new System.Drawing.Point(699, 10);
			this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(32, 32);
			this.button2.TabIndex = 13;
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.SettingButton_Click);
			// 
			// modsFolderFileSystemWatcher
			// 
			this.modsFolderFileSystemWatcher.EnableRaisingEvents = true;
			this.modsFolderFileSystemWatcher.IncludeSubdirectories = true;
			this.modsFolderFileSystemWatcher.SynchronizingObject = this;
			this.modsFolderFileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.modsFolderFileSystemWatcher_Changed);
			this.modsFolderFileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.modsFolderFileSystemWatcher_Created);
			this.modsFolderFileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.modsFolderFileSystemWatcher_Deleted);
			this.modsFolderFileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.modsFolderFileSystemWatcher_Renamed);
			// 
			// consoleBox
			// 
			this.consoleBox.FormattingEnabled = true;
			this.consoleBox.ItemHeight = 15;
			this.consoleBox.Location = new System.Drawing.Point(155, 428);
			this.consoleBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.consoleBox.Name = "consoleBox";
			this.consoleBox.Size = new System.Drawing.Size(569, 94);
			this.consoleBox.TabIndex = 7;
			this.consoleBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 465);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 15);
			this.label3.TabIndex = 8;
			this.label3.Text = "通知とかそんな感じ";
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(740, 552);
			this.Controls.Add(this.ModCheckedListBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.listBox3);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.consoleBox);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
			((System.ComponentModel.ISupportInitialize)(this.modsFolderFileSystemWatcher)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ListBox listBox3;
		private System.Windows.Forms.CheckedListBox ModCheckedListBox;
		private System.Windows.Forms.Button button2;
		private System.DirectoryServices.DirectoryEntry directoryEntry1;
		private System.IO.FileSystemWatcher modsFolderFileSystemWatcher;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox consoleBox;
	}
}

