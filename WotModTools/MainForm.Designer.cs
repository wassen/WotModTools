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
			this.ModCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
			this.modsFolderFileSystemWatcher = new System.IO.FileSystemWatcher();
			this.ApplyModButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.consoleListBox = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.modsFolderFileSystemWatcher)).BeginInit();
			this.SuspendLayout();
			// 
			// ModCheckedListBox
			// 
			this.ModCheckedListBox.AllowDrop = true;
			this.ModCheckedListBox.CheckOnClick = true;
			this.ModCheckedListBox.FormattingEnabled = true;
			this.ModCheckedListBox.HorizontalScrollbar = true;
			this.ModCheckedListBox.Location = new System.Drawing.Point(12, 51);
			this.ModCheckedListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ModCheckedListBox.Name = "ModCheckedListBox";
			this.ModCheckedListBox.Size = new System.Drawing.Size(421, 242);
			this.ModCheckedListBox.TabIndex = 12;
			this.ModCheckedListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModCheckedListBox_DragDrop);
			this.ModCheckedListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModCheckedListBox_DragEnter);
			this.ModCheckedListBox.DoubleClick += new System.EventHandler(this.ModCheckedListBox_DoubleClick);
			this.ModCheckedListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ModCheckedListBox_MouseDown);
			this.ModCheckedListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ModCheckedListBox_MouseUp);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.Control;
			this.button2.BackgroundImage = global::WotModTools.Properties.Resources.setting_icon;
			this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button2.Location = new System.Drawing.Point(528, 11);
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
			// ApplyModButton
			// 
			this.ApplyModButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
			this.ApplyModButton.Location = new System.Drawing.Point(245, 310);
			this.ApplyModButton.Name = "ApplyModButton";
			this.ApplyModButton.Size = new System.Drawing.Size(104, 62);
			this.ApplyModButton.TabIndex = 14;
			this.ApplyModButton.Text = "Mod適用";
			this.ApplyModButton.UseVisualStyleBackColor = true;
			this.ApplyModButton.Click += new System.EventHandler(this.ApplyModButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(439, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 15);
			this.label2.TabIndex = 16;
			this.label2.Text = "先に入れるMod";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 456);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 15);
			this.label3.TabIndex = 8;
			this.label3.Text = "通知領域";
			// 
			// consoleListBox
			// 
			this.consoleListBox.FormattingEnabled = true;
			this.consoleListBox.ItemHeight = 15;
			this.consoleListBox.Location = new System.Drawing.Point(91, 396);
			this.consoleListBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.consoleListBox.Name = "consoleListBox";
			this.consoleListBox.Size = new System.Drawing.Size(469, 139);
			this.consoleListBox.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(439, 278);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 15);
			this.label1.TabIndex = 16;
			this.label1.Text = "後に入れるMod";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(112, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(355, 15);
			this.label4.TabIndex = 16;
			this.label4.Text = "Modの中身(res_modsなど)をドラッグアンドドロップして下さい";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(430, 349);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 17;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 298);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 15);
			this.label5.TabIndex = 18;
			this.label5.Text = "ダブルクリックで削除";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(572, 546);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ApplyModButton);
			this.Controls.Add(this.ModCheckedListBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.consoleListBox);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.modsFolderFileSystemWatcher)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
		private System.Windows.Forms.Button button2;
		private System.DirectoryServices.DirectoryEntry directoryEntry1;
		private System.IO.FileSystemWatcher modsFolderFileSystemWatcher;
		private System.Windows.Forms.Button ApplyModButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox consoleListBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.CheckedListBox ModCheckedListBox;
	}
}

