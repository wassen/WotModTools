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
using System.Text.RegularExpressions;

namespace WotModTools {

	public partial class InputModInfoForm : Form {
		public string ToWhichFolder { get; set; }
		//public string FromWhichFolder { get; set; }
		private string fromWhichFoldersName;

		public InputModInfoForm(string[] paths) {
			InitializeComponent();

			this.ToWhichBrowserDialog.RootFolder = SpecialFolder.MyComputer;
			ToWhichTextBox.Text = this.ToWhichBrowserDialog.SelectedPath = Properties.Settings.Default.WotDir;

			//親ディレクトリが同一なら親をデフォルトのMod名とする
			IEnumerable<string> parentDirectoryNames;
			ModNameTextBox.Text = (parentDirectoryNames = paths.Select(path => Path.GetDirectoryName(path)).Distinct()).Count() == 1 ? Path.GetFileName(parentDirectoryNames.First()) : "";

			fromWhichFoldersName = String.Join(",", paths.Select(e => Path.GetFileName(e)));
			FromPathsLabel.Text = fromWhichFoldersName + "を\nどのフォルダーに追加しますか";
		}

		//継承用
		public InputModInfoForm() {
			InitializeComponent();
		}

		private void ToWhichButton_Click(object sender, EventArgs e) {
			if (ToWhichBrowserDialog.ShowDialog() == DialogResult.OK) {
				if (Program.IsParentDir(Properties.Settings.Default.WotDir, ToWhichBrowserDialog.SelectedPath) ||
					Program.IsSamePath(Properties.Settings.Default.WotDir, ToWhichBrowserDialog.SelectedPath)) {
					this.ToWhichTextBox.Text = ToWhichBrowserDialog.SelectedPath;
				}
				else {
					ToWhichBrowserDialog.SelectedPath = this.ToWhichTextBox.Text;
					MessageBox.Show("WOTディレクトリより下のディレクトリを選択してください。WOTディレクトリは" + Properties.Settings.Default.WotDir + "です。","注意",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					//この値は設定から変更できます。としたい。
				}
			}
		}

		protected void OKButton_Click(object sender, EventArgs e) {
		
			Func<bool> isVoidTextBox = () => {
				if (ToWhichTextBox.Text == "" || ModNameTextBox.Text == "") {
					MessageBox.Show("テキストボックスが空白です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return true;
				}
				else {
					return false;
				}
			};

			Func<bool> existModName = () => {
				if (Directory.Exists(Path.Combine(Properties.Settings.Default.Mods, ModNameTextBox.Text))) {
					MessageBox.Show("既に" + ModNameTextBox.Text + "は存在します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return true;
				}
				else {
					return false;
				}
			};
			Func<bool> pathConfirmation = () => {
				string confirmPath = Path.Combine(ToWhichTextBox.Text, fromWhichFoldersName);
				return MessageBox.Show("Mod適用時に" + confirmPath + "...\nが作成されます。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes;
			};

			if (!isVoidTextBox() && !existModName() && pathConfirmation()) {
				string wotDir = Program.AddLastBackSlash(Properties.Settings.Default.WotDir);
				string toDir = Program.AddLastBackSlash(ToWhichTextBox.Text);
				//C:\Games\World_of_Tanks\res_mods\audio\ - C:\Games\World_of_Tanks\ = res_mods\audio\
				string diffDir = Program.deleteHeadPath(toDir,wotDir);
                this.ToWhichFolder = Path.Combine(Properties.Settings.Default.Mods, ModNameTextBox.Text, diffDir);

				this.DialogResult = DialogResult.Yes;
			}
		}

		private void AddModForm_Load(object sender, EventArgs e) {

		}
	}
}
//OKボタンのところ、エラーあるなら最初っから全部言えやって気になるし、途中で戻らず全て言ってくれる仕組みに変えたい。
//ファイルに使えない文字を警告したい。Fileクラスあたりが返してくれた気がする。