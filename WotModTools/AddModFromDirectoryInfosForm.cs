using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Environment;

namespace WotModTools {
	public partial class AddModFromDirectoryInfosForm : AddModForm {
		private IEnumerable<DirectoryInfo> droppedDirectoryInfos;
		public AddModFromDirectoryInfosForm(IEnumerable<DirectoryInfo> droppedDirectoryInfos) {
			this.droppedDirectoryInfos = droppedDirectoryInfos;
			InitializeComponent();

			this.ToWhichBrowserDialog.RootFolder = SpecialFolder.MyComputer;
			ToWhichTextBox.Text = this.ToWhichBrowserDialog.SelectedPath = Properties.Settings.Default.WotDir;

			IEnumerable<string> parent;
			if (droppedDirectoryInfos.Count() == 1) {
				ModNameTextBox.Text = droppedDirectoryInfos.First().Name;
			}
			else if ((parent = droppedDirectoryInfos.Select(e => Path.GetDirectoryName(e.FullName)).Distinct()).Count() == 1) {
				ModNameTextBox.Text = Path.GetFileName(parent.First());
			}

			FromFoldersLabel.Text = String.Join(",", droppedDirectoryInfos.Select(e => e.Name).ToArray<string>()) + "を\nどのフォルダーに追加しますか";
		}

		private void AddModFromDirectoryInfosForm_Load(object sender, EventArgs e) {

		}


		protected override void OKButton_Click(object sender, EventArgs e) {
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
				this.DialogResult = DialogResult.Yes;
			}
			else {
				return;
			}
			//関係ない愚痴 バツボタンの挙動Cancelも用意しないとバツボタンを押すと強制OKとなる。✕押してるのにOKとは。なんでわざわざ同じ機能を持つボタンを用意せにゃならんのだ。YesNoならバツボタンが消える模様。
			//Visual Studioの"コードが実行されているときは、変更が許可されません"がうざい

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


		}





	}
}