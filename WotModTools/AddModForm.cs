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

			IEnumerable<string> parentDirectoryNames;
			ModNameTextBox.Text = (parentDirectoryNames = paths.Select(path => Path.GetDirectoryName(path)).Distinct()).Count() == 1 ? Path.GetFileName(parentDirectoryNames.First()) : "";

			fromWhichFoldersName = String.Join(",", paths.Select(e => Path.GetFileName(e)));
			FromPathsLabel.Text = fromWhichFoldersName + "を\nどのフォルダーに追加しますか";
		}

		public InputModInfoForm() {
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e) {
		}

		private void ToWhichButton_Click(object sender, EventArgs e) {
			if (ToWhichBrowserDialog.ShowDialog() == DialogResult.OK) {
				if (Program.IsParentDir(Properties.Settings.Default.WotDir, ToWhichBrowserDialog.SelectedPath) ||
					Program.IsSamePath(Properties.Settings.Default.WotDir, ToWhichBrowserDialog.SelectedPath)) {
					this.ToWhichTextBox.Text = ToWhichBrowserDialog.SelectedPath;
				}
				else {
					ToWhichBrowserDialog.SelectedPath = this.ToWhichTextBox.Text;
					MessageBox.Show("WOTディレクトリより下のディレクトリを選択してください。WOTディレクトリは" + Properties.Settings.Default.WotDir + "です。この値は設定から変更できます。");
				}
			}
		}



		private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e) {

		}


		private void label3_Click(object sender, EventArgs e) {

		}

		//TODO ディレクトリが一つだったら、その直下を入れると考えたほうが良いのか？FromWhichやっぱりいる？ここが悩みどころだけど、今は修正せずにいこう。最終的にはres_modsかaudioあたりをはんべつすればいや
		//try {
		//	confirmPath = Path.Combine(ToWhichTextBox.Text, Path.GetFileName(droppedDirectoryInfos.First().Name));
		//}
		//catch (Exception) {
		//	MessageBox.Show("テキストボックスでパス関連のエラーが出ました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	return;
		//}
		protected void OKButton_Click(object sender, EventArgs e) {
			//エラーあるなら最初っから全部言えやって気にならない？どうなんでしょう。
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
				//C:\Games\World_of_Tanks\res_mods\audio\ - C:\Games\World_of_Tanks\ = res_mods\audio\
				string wotDir = Program.AddLastBackSlash(Properties.Settings.Default.WotDir);
				string toDir = Program.AddLastBackSlash(ToWhichTextBox.Text);
				string diffDir = toDir.Substring(wotDir.Length, toDir.Length - wotDir.Length);

				this.ToWhichFolder = Path.Combine(Properties.Settings.Default.Mods, ModNameTextBox.Text, diffDir);

				this.DialogResult = DialogResult.Yes;
			}
		}
		//関係ない愚痴 バツボタンの挙動Cancelも用意しないとバツボタンを押すと強制OKとなる。✕押してるのにOKとは。なんでわざわざ同じ機能を持つボタンを用意せにゃならんのだ。YesNoならバツボタンが消える模様。
		//Visual Studioの"コードが実行されているときは、変更が許可されません"がうざい


		private void AddModForm_Load(object sender, EventArgs e) {

		}
	}
}
