﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace WotModTools {
	public partial class MainForm : Form {

		private Properties.Settings settings;

		public MainForm() {

			InitializeComponent();

			this.settings = Properties.Settings.Default;

			fileSystemWatcher1.Path = settings.Workspace;

			ReloadModList();
		}

		private void ReloadModList() {

			ModList.Items.Clear();

			//workspace\audio or othersフォルダーから、Modの一覧を生成
			string[] typeList = { "audio", "others" };

			foreach (string type in typeList) {
				string typeFolder = Path.Combine(settings.Workspace, type);
				if (!Directory.Exists(typeFolder)) {
					continue;
				}

				IEnumerable<string> modNameList = Directory.GetDirectories(typeFolder).Select(e => Path.GetFileName(e));
				foreach (string name in modNameList) {
					ModList.Items.Add(name);
				}
			}
		}

		private void Form1_DragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void Form1_DragDrop(object sender, DragEventArgs e) {


			STATask.Run(() => {
				//TODO 抜本的に改革 Dictにする？なんにせよフォルダ名なりなんなりをInputBoxに表示できるようにしておく。
				//Modのあるフォルダーを選んでもらう？それか検知する？入力で作成するフォルダ名まで選んでもらえばよいか？
				//なんにせよ、ディレクトリ一つだけしか出来ない現状はまずい。
				//audioフォルダーの探索か。それがいいな。

				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

				IEnumerable<FileInfo> dFileEnum = files.Where(item => File.Exists(item)).Select(item => new FileInfo(item));
				IEnumerable<DirectoryInfo> dDirectoryEnum = files.Where(item => Directory.Exists(item)).Select(item => new DirectoryInfo(item));


				GetModFromFile(dFileEnum);
				GetModFromDirectory(dDirectoryEnum);

				/*
				TODO 2つのaudioフォルダーを両方
				他、2つ以上のModを優先度付きで管理

				流れ
				ドラッグアンドドロップ
				ファイルの内、audioファイルを探して作業ディレクトリにコピー （TODO zip拡張はまた今度）
				audioがハードリンクされてなければする
				audioファイル内にあるファイルのハードリンク削除
				注意 ハードリンクの削除は単純に削除して大丈夫？
				audioファイル内のものをリンク
				終了！
				*/

			});
		}

		private void GetModFromFile(IEnumerable<FileInfo> droppedFileEnum) {

			IEnumerable<string> parentDir = droppedFileEnum.Select(item => item.DirectoryName).Distinct();
			if (parentDir.Count() != 1) {//親ディレクトリが同一であるかどうか
				Console.WriteLine("ファイルではないか、同一フォルダー内に存在していません。");
				Console.WriteLine("同一フォルダー内に存在していないファイルを同時にドラッグアンドドロップする方法があるのかどうか知らないけどね");
				return;
			}

			string input = ShowModForm(Path.GetFileName(parentDir.ElementAt(0)));
			if (input == null || input == "") {
				Console.WriteLine("空白のためキャンセルしました。");
				return;
			}

			string copyFolder = Path.Combine(settings.Workspace, "audio", input);
			if (Directory.Exists(copyFolder)) {
				Console.WriteLine("もう" + input + "はあるで");
				return;
			}

			foreach (FileInfo info in droppedFileEnum) {
				info.CopyTo(copyFolder);
			}
		}

		private void GetModFromDirectory(IEnumerable<DirectoryInfo> droppedDirectoryEnum) {

			foreach (DirectoryInfo droppedDirectoryInfo in droppedDirectoryEnum) {

				//フォルダー内のModっぽいフォルダーを探索、なければ、ツリーを表示してどれがModか、どのディレクトリに入れるのかを聞く
				//enumでエラーを返すか？voidにしてるが

				IList<DirectoryInfo> modDirectories = droppedDirectoryInfo.GetDirectories("*", SearchOption.AllDirectories).ToList();
				modDirectories.Add(droppedDirectoryInfo);

				//audio,res_modsフォルダーに対する特殊処理は・・・こんどでいいや


				var wfForm = new WhichFolderForm(droppedDirectoryInfo) ;
				wfForm.ShowDialog();
				string from = wfForm.FromWhichFolder;
				string to = wfForm.ToWhichFolder;

				return;

				switch (modDirectories.Count(e => e.Name == "res_mods")) {
					case 0:




						string input;

						break;
					case 1:

						break;
					default:
						MessageBox.Show("一つのディレクトリに複数のModが含まれているため、Modを追加できませんでした。");
						continue;
				}
				switch (modDirectories.Where(e => e.Name == "audio").Count()) {
					case 0:
						MessageBox.Show("audioフォルダーが見つかりませんでした。");
						break;
					case 1:
						break;
					default:
						MessageBox.Show("audioフォルダー2つ以上あるんだが？");
						continue;
				}



				//foreach (DirectoryInfo audiodInfo in audioDirectories) {
				//	string input = ShowModForm("modify!!!");
				//	if (input == null) {
				//		return;
				//	}
				//	string copyFolder = Path.Combine(settings.Workspace, "audio", input);
				//	DirectoryCopy(audiodInfo.FullName, copyFolder);
				//}

			}
		}



		//プログラム書いている時に、時々、そもそもこの処理は必要なのか、何を実現したかったのか、メソッド分けずにやったほうが簡潔ではとかね？を思い返すことが大事
		private string ShowModForm(string modName) {
			using (var modForm = new ModForm(modName)) {
				modForm.ShowDialog();
				if (modForm.DialogResult == DialogResult.OK) {
					string input = modForm.TextBox;
					return input;
				}
				else {
					return null;
				}
			}
		}

		//TODO 複数Modを優先度付きで同時に適用したいが、まあ今度で
		private void AudioApplyButton_Click(object sender, EventArgs e) {
			string resAudioPath = Path.Combine(settings.WotDir, "res", "audio");
			string res_modsAudioPath = Path.Combine(settings.WotDir, "res_mods", settings.WotVersion, "audio");
			string workspaceAudioPath = Path.Combine(settings.Workspace, "audio", "Mako");
			//TODO 暫定でMako確定。後で選択処理を入れる。

			HardLinks(res_modsAudioPath, resAudioPath);
			HardLinks(res_modsAudioPath, workspaceAudioPath);
		}

		/**
			<summary>
			targetDir直下にあるファイル全てをディレクトリの構造を保ったままlinkDirにハードリンクします
			</summary>
			<param name="linkDir">リンクを作成するディレクトリ</param>
			<param name="targetDir">リンクの参照元となるディレクトリ</param>
		*/
		public void HardLinks(string linkDir, string targetDir) {

			if (!Directory.Exists(targetDir)) {
				Console.WriteLine("リンク元のディレクトリが存在しません");
				return;
			}
			Directory.CreateDirectory(linkDir);

			var fileList = new List<string>(Directory.GetFiles(targetDir, "*", SearchOption.AllDirectories));
			var folderList = new List<string>(Directory.GetDirectories(targetDir, "*", SearchOption.AllDirectories));

			//targetDir\B1F\B2F\fileName - targetDir = B1F\B2F\fileName を作成
			//行末に\をつけて統一
			string targetDir_bs = Regex.Replace(targetDir, @"\\?$", "");
			//\を\\に
			targetDir_bs = Regex.Replace(targetDir_bs, @"\\|$", @"\\");
			var targetReg = new Regex(targetDir_bs);

			foreach (string folder in folderList) {
				//最初に現れたtargetDirをfolderから''に置換
				string folderName = targetReg.Replace(folder, @"", 1);
				Directory.CreateDirectory(Path.Combine(linkDir, folderName));
			}

			foreach (string file in fileList) {
				//最初に現れたtargetDirをfolderから''に置換
				string fileName = targetReg.Replace(file, @"", 1);
				string fullLink = Path.Combine(linkDir, fileName);
				if (File.Exists(fullLink)) {
					//ハードリンクを比較するうまい方法が思いつかないため問答無用で削除して再リンク（一旦閉じてからもう一つも開けばいいだけだけど）
					File.Delete(fullLink);
					Console.WriteLine("sakujo");
				}
				//存在してる時にMklinkしても何も起こらないハズ
				Mklink("/H", Path.Combine(linkDir, fullLink), file);
			}
		}
		/*
			XXX ディレクトリがバックスラッシュじゃなくてスラッシュだったらどうしよう・・・入出力の段階で全て統一したい。
			Console.WriteLine(Path.AltDirectorySeparatorChar); Console.WriteLine(Path.DirectorySeparatorChar);
			RegExpじゃなくてSubstringでやったほうが早かった気もする
			Path.ComBineはファイル名の先頭に\が付いてるのを許さない模様（今までひっつけたディレクトリを消して、新しくディレクトリの開始として認識してしまうらしい）
		*/

		/**
			<summary>
			DOSコマンドのmklinkを呼び出します。
			</summary>
			<param name="type">"/H" ハードリンク "/J" ジャンクション "/S" シンボリックリンク </param>
			<param name="linkDir">リンクを作成するディレクトリ</param>
			<param name="targetDir">リンクの参照元となるディレクトリ</param>
			TODO フォルダ名に';'が含まれてるとリンクが作られなかったため要調査
		*/
		public void Mklink(string type, string linkDir, string targetDir) {
			Process p = new System.Diagnostics.Process();
			p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
			//出力を読み取れるようにする
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = false;
			//ウィンドウを表示しないようにする
			p.StartInfo.CreateNoWindow = true;
			//コマンドラインを指定（"/c"は実行後閉じるために必要
			p.StartInfo.Arguments = @"/c mklink " + type + " " + linkDir + " " + targetDir;
			p.Start();
			//出力を読み取る
			string results = p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			p.Close();
			Console.WriteLine(@"/c mklink " + type + @" " + linkDir + @" " + targetDir);
			Console.WriteLine(results);
		}
		//何回もcloseするのは効率が悪い気もする
		//ハードリンクのところで時間がかかるようなら、メソッド化せずに書いていちいちcloseしないことも検討







		//丸パクリなのでちゃんとチェックして修正。
		public static void DirectoryCopy(string sourcePath, string destinationPath) {
			DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
			DirectoryInfo destinationDirectory = new DirectoryInfo(destinationPath);

			//コピー先のディレクトリがなければ作成する
			if (destinationDirectory.Exists == false) {
				destinationDirectory.Create();
				destinationDirectory.Attributes = sourceDirectory.Attributes;
			}

			//ファイルのコピー
			foreach (FileInfo fileInfo in sourceDirectory.GetFiles()) {
				//同じファイルが存在していたら、常に上書きする
				fileInfo.CopyTo(destinationDirectory.FullName + @"\" + fileInfo.Name, true);
			}

			//ディレクトリのコピー（再帰を使用）
			foreach (System.IO.DirectoryInfo directoryInfo in sourceDirectory.GetDirectories()) {
				DirectoryCopy(directoryInfo.FullName, destinationDirectory.FullName + @"\" + directoryInfo.Name);
			}
		}

		private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e) {
		}

		private void SettingButton_Click(object sender, EventArgs e) {
			using (var fs = new FormSetting()) {
				fs.ShowDialog();
			}
		}

		private void textBox2_TextChanged(object sender, EventArgs e) {
		}
		private void label1_Click(object sender, EventArgs e) {
		}
		private void label2_Click(object sender, EventArgs e) {
		}
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
		}
		private void listBox3_SelectedIndexChanged(object sender, EventArgs e) {
		}
		private void Form1_Load(object sender, EventArgs e) {
		}
		private void textBox1_TextChanged(object sender, EventArgs e) {
		}
		private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
		}
		private void button3_Click(object sender, EventArgs e) {
		}

		private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e) {
			ReloadModList();
		}
	}
}



/*
	TODO ドラッグアンドドロップで、一つのファイルに複数のaudioがあった時の処理
	TODO 知っているディレクトリを検索して、見つからなかった場合選択してもらう。
	TODO D&D以外でフォルダ選択、参照したいんだが？
*/
//TODO 取り込んだModの削除も実装したい