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

	public partial class AddModForm : Form {
		public string ToWhichFolder { get; set; }
		public string FromWhichFolder { get; set; }

		private IEnumerable<DirectoryInfo> droppedDirectoryInfos;
		public AddModForm(IEnumerable<DirectoryInfo> droppedDirectoryInfos) {
			this.droppedDirectoryInfos = droppedDirectoryInfos;
			InitializeComponent();

			this.ToWhichBrowserDialog.RootFolder = SpecialFolder.MyComputer;
			ToWhichTextBox.Text = this.ToWhichBrowserDialog.SelectedPath = Properties.Settings.Default.WotDir;

			IEnumerable<string> parent;
			if ((parent = droppedDirectoryInfos.Select(e => Path.GetDirectoryName(e.FullName)).Distinct()).Count() == 1) {
				ModNameTextBox.Text = Path.GetFileName(parent.First());
			}

			FromFoldersLabel.Text = String.Join(",", droppedDirectoryInfos.Select(e => e.Name).ToArray<string>()) + "を\nどのフォルダーに追加しますか";
		}

		public AddModForm() {
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e) {
		}

		private void ToWhichButton_Click(object sender, EventArgs e) {
			if (ToWhichBrowserDialog.ShowDialog() == DialogResult.OK) {

				if (isParentDir(Properties.Settings.Default.WotDir, ToWhichBrowserDialog.SelectedPath) ||
					isSamePath(Properties.Settings.Default.WotDir, ToWhichBrowserDialog.SelectedPath)) {
					this.ToWhichTextBox.Text = ToWhichBrowserDialog.SelectedPath;
				}
				else {
					ToWhichBrowserDialog.SelectedPath = this.ToWhichTextBox.Text;
					MessageBox.Show("WOTディレクトリより下のディレクトリを選択してください。WOTディレクトリは" + Properties.Settings.Default.WotDir + "です。この値は設定から変更できます。");
				}
			}
		}

		protected bool isSamePath(string argDir1, string argDir2) {
			return Program.removeLastBackSlash(argDir1) == Program.removeLastBackSlash(argDir2);
		}

		protected bool isParentDir(string target, string child) {
			target = Program.removeLastBackSlash(target);
			IEnumerable<string> parents = getParentDirectories(child);
			foreach (string parent in parents) {
				if (target == parent)
					return true;
			}
			return false;
		}



		protected IEnumerable<string> getParentDirectories(string argDir) {
			while (Path.GetPathRoot(argDir) != argDir) {
				argDir = Path.GetDirectoryName(argDir);
				yield return argDir;
			}
		}


		private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e) {

		}


		private void label3_Click(object sender, EventArgs e) {

		}

		//TODO ディレクトリが一つだったら、その直下を入れると考えたほうが良いのか？FromWhichやっぱりいる？ここが悩みどころだけど、今は修正せずにいこう。最終的にはres_modsかaudioあたりをはんべつすればいや
		protected void OKButton_Click(object sender, EventArgs e) {
			if (ToWhichTextBox.Text == "" || ModNameTextBox.Text == "") {
				MessageBox.Show("テキストボックスが空白です。", "エラー");
				return;
			}

			string confirmPath;
			confirmPath = Path.Combine(ToWhichTextBox.Text, Path.GetFileName(droppedDirectoryInfos.First().Name));
			//try {
			//	confirmPath = Path.Combine(ToWhichTextBox.Text, Path.GetFileName(droppedDirectoryInfos.First().Name));
			//}
			//catch (Exception) {
			//	MessageBox.Show("テキストボックスでパス関連のエラーが出ました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//	return;
			//}

			if (MessageBox.Show("Mod適用時に" + confirmPath + "...が作成されます。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes) {

				Action CopyModsToWorkspace = () => {
					string wotDir = Program.AddLastBackSlash(Properties.Settings.Default.WotDir);
					string toDir = Program.AddLastBackSlash(ToWhichTextBox.Text);
					string cutDir = toDir.Substring(wotDir.Length, toDir.Length - wotDir.Length);

					foreach (DirectoryInfo droppedDirectoryInfo in droppedDirectoryInfos) {
						Program.DirectoryCopy(
							Path.Combine(droppedDirectoryInfo.FullName),
							Path.Combine(Properties.Settings.Default.Mods, ModNameTextBox.Text, cutDir)
						);
					}
				};

				CopyModsToWorkspace();

				this.DialogResult = DialogResult.Yes;
			}
			else {
				return;
			}
			//関係ない愚痴 バツボタンの挙動Cancelも用意しないとバツボタンを押すと強制OKとなる。✕押してるのにOKとは。なんでわざわざ同じ機能を持つボタンを用意せにゃならんのだ。YesNoならバツボタンが消える模様。
			//Visual Studioの"コードが実行されているときは、変更が許可されません"がうざい






		}

		private void AddModForm_Load(object sender, EventArgs e) {

		}
	}
}
