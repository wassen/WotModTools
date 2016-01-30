using System;
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

		private List<Form> openingFormList;
		private Properties.Settings settings;

		public MainForm() {
			InitializeComponent();
			this.settings = Properties.Settings.Default;
			this.openingFormList = new List<Form>();
			modsFolderFileSystemWatcher.Path = settings.Mods;
			AudioApplyButton.Text = @"res\audio=>res_mods\" + settings.WotVersion + @"\audio";
			ReloadModList();

		}

		private void ReloadModList() {
			ModCheckedListBox.Items.Clear();

			//求ム　IEnumerableからObjectCollectionへ変換
			foreach (string modName in Program.getModNameList()) {
				ModCheckedListBox.Items.Add(modName);
			}
		}

		private void Form1_DragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void Form1_DragDrop(object sender, DragEventArgs e) {
			string[] paths = ((string[])e.Data.GetData(DataFormats.FileDrop, false));

			STATask.Run(() => {
				var wfForm = new InputModInfoForm(paths);

				this.OpenForm(wfForm);
				if (wfForm.DialogResult != DialogResult.Yes) return;
				string toWhichFolder = wfForm.ToWhichFolder;
				this.CloseForm(wfForm);

				string[] droppedFilePaths = paths.Where(item => File.Exists(item)).ToArray<string>();
				string[] droppedDirectoryPaths = paths.Where(item => Directory.Exists(item)).ToArray<string>();

				CopyPaths(File.Copy, droppedFilePaths, toWhichFolder);
				CopyPaths(Program.DirectoryCopy, droppedDirectoryPaths, toWhichFolder);
			});
		}

		private void CopyPaths(Action<string, string> copy, string[] sources, string dest) {
			foreach (string source in sources) {
				copy(source, dest);
			}
		}

		//private void GetModFromDirectoryInfo(IEnumerable<DirectoryInfo> droppedDirectoryInfoEnum) {
		//	/*
		//	switch (modDirectories.Count(e => e.Name == "res_mods")) {
		//		case 0:




		//			string input;

		//			break;
		//		case 1:

		//			break;
		//		default:
		//			MessageBox.Show("一つのディレクトリに複数のModが含まれているため、Modを追加できませんでした。");
		//			continue;
		//	}
		//	switch (modDirectories.Where(e => e.Name == "audio").Count()) {
		//		case 0:
		//			MessageBox.Show("audioフォルダーが見つかりませんでした。");
		//			break;
		//		case 1:
		//			break;
		//		default:
		//			MessageBox.Show("audioフォルダー2つ以上あるんだが？");
		//			continue;
		//	}*/



		//	//foreach (DirectoryInfo audiodInfo in audioDirectories) {
		//	//	string input = ShowModForm("modify!!!");
		//	//	if (input == null) {
		//	//		return;
		//	//	}
		//	//	string copyFolder = Path.Combine(settings.Workspace, "audio", input);
		//	//	DirectoryCopy(audiodInfo.FullName, copyFolder);
		//	//}

		//}



		//プログラム書いている時に、時々、そもそもこの処理は必要なのか、何を実現したかったのか、メソッド分けずにやったほうが簡潔ではとかね？を思い返すことが大事
		//private string ShowModForm(string modName) {
		//	using (var modForm = new ModForm(modName)) {
		//		modForm.ShowDialog();
		//		if (modForm.DialogResult == DialogResult.OK) {
		//			string input = modForm.TextBox;
		//			return input;
		//		}
		//		else {
		//			return null;
		//		}
		//	}
		//}

		//TODO 複数Modを優先度付きで同時に適用したいが、まあ今度で

		private void AudioApplyButton_Click(object sender, EventArgs e) {
			string resAudioPath = Path.Combine(settings.WotDir, "res", "audio");
			string res_modsAudioPath = Path.Combine(settings.WotDir, "res_mods", settings.WotVersion, "audio");

			HardLinks(res_modsAudioPath, resAudioPath);
		}


		//string バージョンをどう扱うか・・・
		//Modlistは設定ファイルを作る？
		private void ApplyModButton_Click(object sender, EventArgs e) {
			IEnumerable<string> modPaths = getxheckedModPathList();

			foreach (string modPath in modPaths) {
				HardLinks(settings.WotDir, modPath);
			}
		}

		private IEnumerable<string> getSelectedModNameList() {
			CheckedListBox.CheckedItemCollection checkcedMods = ModCheckedListBox.CheckedItems;
			IList<string> checkedModNameList = new List<string>();
			foreach (Object checkedMod in checkcedMods) {
				checkedModNameList.Add(checkedMod as string);
			}
			if (checkedModNameList.Contains(null)) {
				throw new Exception();
			}
			return checkedModNameList;
		}

		private IEnumerable<string> getcheckedModPathList() {
			foreach (string modName in getSelectedModNameList()) {
				yield return Path.Combine(settings.Mods, modName);
			}
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

			Process dosCmd = OpenDosCmdProcess();
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
				IList<string> commandList = new List<string>();
				commandList.Add("mklink");
				commandList.Add("/h");
				commandList.Add(Path.Combine(linkDir, fullLink));
				commandList.Add(file);

				executeDosCmd(dosCmd, String.Join(" ", commandList));
			}
			dosCmd.WaitForExit();
			dosCmd.Close();
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
		public void executeDosCmd(Process p, string command) {
			//"/c"は実行後閉じるために必要
			p.StartInfo.Arguments = @"/c " + command;
			p.Start();
		}
		public void DosCmd(Process p, IEnumerable<string> commandList) {
			//"/c"は実行後閉じるために必要
			p.StartInfo.Arguments = @"/c " + String.Join(" ", commandList);
			p.Start();
		}

		//何回もcloseするのは効率が悪い気もする
		//ハードリンクのところで時間がかかるようなら、メソッド化せずに書いていちいちcloseしないことも検討

		private static Process OpenDosCmdProcess() {
			var p = new Process();
			p.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
			p.StartInfo.UseShellExecute = false;
			//p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = false;
			p.StartInfo.CreateNoWindow = true;
			return p;
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


		private void modsFolderFileSystemWatcher_Changed(object sender, FileSystemEventArgs e) {
			ReloadModList();
		}
		private void modsFolderFileSystemWatcher_Created(object sender, FileSystemEventArgs e) {
			ReloadModList();
		}
		private void modsFolderFileSystemWatcher_Deleted(object sender, FileSystemEventArgs e) {
			ReloadModList();
		}
		private void modsFolderFileSystemWatcher_Renamed(object sender, RenamedEventArgs e) {
			ReloadModList();
		}

		//メインを閉じたら全部閉じる
		private void OpenForm(Form form) {
			openingFormList.Add(form);
			form.ShowDialog();
		}
		private void CloseForm(Form form) {
			form.Close();
		}
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			openingFormList.ForEach(form => form.Close());
		}


		private void button1_Click(object sender, EventArgs e) {
			IEnumerable<ModInfo> selectedModInfos = getSelectedModNameList().Select(selectedMod => new ModInfo(selectedMod));

			foreach (ModInfo selectedModInfo in selectedModInfos) {
				//自分を除外した全ての要素でループ



				foreach (ModInfo otherModInfo in selectedModInfos.Except(new ModInfo[] { selectedModInfo })) {
					selectedModInfo.setConflictInfo(otherModInfo);
				}
				Console.WriteLine(selectedModInfo.conflictDict);
			}


		}
	}
}


/*
	TODO ドラッグアンドドロップで、一つのファイルに複数のaudioがあった時の処理
	TODO 知っているディレクトリを検索して、見つからなかった場合選択してもらう。
	TODO D&D以外でフォルダ選択、参照したいんだが？
*/
//TODO 取り込んだModの削除も実装したい
//フォルダーとかは最初に初期化してしまったほうがいいか。modsとかで乗り切るのはよろしくない気がする。
//enumでエラーを返す
//フォルダー内のModっぽいフォルダーを探索、なければ、ツリーを表示してどれがModか、どのディレクトリに入れるのかを聞く
//audio,res_modsフォルダーに対する特殊処理は・・・こんどでいいや

//既にModがあるとき、それを避けてできる？
//progressbarの実装
//copyとhardlink
//Modの一部削除(被りの記録)