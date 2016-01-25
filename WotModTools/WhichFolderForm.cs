using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using static System.Environment;
using System.IO;

namespace WotModTools {


	public partial class WhichFolderForm : Form {
		public string ToWhichFolder { get; set; }
		public string FromWhichFolder { get; set; }
		private DirectoryInfo droppedDirectoryInfo;
		public WhichFolderForm(DirectoryInfo droppedDirectoryInfo) {
			this.droppedDirectoryInfo = droppedDirectoryInfo;
			InitializeComponent();
			;

			this.ToWhichBrowserDialog.RootFolder = SpecialFolder.MyComputer;
			this.ToWhichBrowserDialog.SelectedPath = Path.Combine(Properties.Settings.Default.WotDir, "res_mods");

			this.FromWhichBrowserDialog.RootFolder = SpecialFolder.MyComputer;
			this.FromWhichBrowserDialog.SelectedPath = droppedDirectoryInfo.FullName;

			label3.Text = droppedDirectoryInfo.Name;
		}

		private void label1_Click(object sender, EventArgs e) {

		}

		private void ToWhichButton_Click(object sender, EventArgs e) {
			if (ToWhichBrowserDialog.ShowDialog() == DialogResult.OK) {
				this.ToWhichTextBox.Text = ToWhichBrowserDialog.SelectedPath;
			}
		}

		private void FromWhichButton_Click(object sender, EventArgs e) {
			if (FromWhichBrowserDialog.ShowDialog() == DialogResult.OK) {
				this.FromWhichTextBox.Text = FromWhichBrowserDialog.SelectedPath;
			}
		}

		private void label3_Click(object sender, EventArgs e) {

		}

		private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e) {

		}

		private void OKButton_Click(object sender, EventArgs e) {
			//FuckinSubstring切り取り
			//romWhichTextBox.Text);
			//Console.WriteLine(ToWhichTextBox.Text);
			Console.WriteLine(Path.GetPathRoot(FromWhichTextBox.Text));

			//バツボタンの挙動Cancelも用意しないとバツボタンを押すと強制お=OKとなる。あほか。なんでわざわざ同じ機能を持つボタンを用意せにゃならんのだ。YesNoならバツボタンが消える模様。
			//Visual StudioのVisual Studio "コードが実行されているときは、変更が許可されません"がうざすぎる
			if (MessageBox.Show("いけます？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes) {
				this.DialogResult = DialogResult.Yes;
			}
			else {
			}

		}
	}
}
