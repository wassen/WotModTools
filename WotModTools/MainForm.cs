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
using Microsoft.VisualBasic.FileIO;
using System.Drawing;

namespace WotModTools {

	public partial class MainForm : Form {

		private List<Form> openingFormList = new List<Form>();
		private Properties.Settings settings = Properties.Settings.Default;
		private TimeSpan reloadClock;
		private Stopwatch sw = new Stopwatch();
		private List<Task> runTaskList = new List<Task>();
		private IList<string> modOrder = new List<string>();


		public MainForm() {
			InitializeComponent();
			modsFolderFileSystemWatcher.Path = settings.Mods;
			sw.Start();
			LoadModList();
			//サイズ変更の禁止
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
		}

		private void LoadModList() {
			//getAllModNameListはforeachの最中にyield returnしているため、途中でModChechedListBoxを変更するところでバグる。
			//Listのコンストラクタに突っ込んで初期化してるが、メソッド側をIListとかに集めてから返すように変更することも検討
			//修正済み
			IEnumerable<string> oldModsName = getAllModNameList();
			IEnumerable<string> newModsName = Program.getModFolderList();
			CheckedListBox oldModList = ModCheckedListBox;
			//求ム　IEnumerableからObjectCollectionへ変換
			foreach (string addedModName in newModsName.Except(oldModsName)) {
				ModCheckedListBox.Items.Add(addedModName);
			}
			foreach (string deleteModName in oldModsName.Except(newModsName)) {
				ModCheckedListBox.Items.Remove(oldModList.Items[oldModList.FindStringExact(deleteModName)]);
			}
		}

		private void ReloadModList() {
			if (!runTaskList.All(runTask => runTask.IsCompleted)) return;

			reloadClock = sw.Elapsed;
			if (reloadClock.Seconds > 1) {
				LoadModList();
				sw.Restart();
			}
		}

		private void GetModInfo(object sender, DragEventArgs e) {
			string[] paths = ((string[])e.Data.GetData(DataFormats.FileDrop, false));

			runTaskList.Add(STATask.Run(
				() => {
					var wfForm = new InputModInfoForm(paths);

					this.OpenForm(wfForm);
					if (wfForm.DialogResult != DialogResult.Yes) return;
					string toWhichFolder = wfForm.ToWhichFolder;
					this.CloseForm(wfForm);

					string[] droppedFilePaths = paths.Where(item => File.Exists(item)).ToArray<string>();
					string[] droppedDirectoryPaths = paths.Where(item => Directory.Exists(item)).ToArray<string>();

					foreach (string droppedFilePath in droppedFilePaths) {
						FileSystem.CopyFile(droppedFilePath, toWhichFolder, UIOption.AllDialogs);
					}
					foreach (string droppedDirectoryPath in droppedDirectoryPaths) {
						FileSystem.CopyDirectory(droppedDirectoryPath, Path.Combine(toWhichFolder, Path.GetFileName(droppedDirectoryPath)), UIOption.AllDialogs);
					}
					//プログレスバー表示のためデリゲート断念
					//CopyPaths(File.Copy, droppedFilePaths, toWhichFolder);
					//CopyPaths(Program.DirectoryCopy, droppedDirectoryPaths, toWhichFolder);
				})
			);
		}

		//private void CopyPaths(Action<string, string> copy, string[] sources, string dest) {
		//	foreach (string source in sources) {
		//		copy(source, Path.Combine(dest, Path.GetFileName(source)));
		//	}
		//}

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

		private void AudioApply() {
			string resAudioPath = Path.Combine(settings.WotDir, "res", "audio");
			string res_modsAudioPath = Path.Combine(settings.WotDir, "res_mods", settings.WotVersion, "audio");

			//HardLinks(res_modsAudioPath, resAudioPath);
			FileSystem.CopyDirectory(resAudioPath, Path.Combine(res_modsAudioPath, Path.GetFileName(resAudioPath)));
		}

		private void Notice(string str) {
			consoleListBox.Items.Insert(0, "Modの適用を中止しました");
			if (consoleListBox.Items.Count > 100) consoleListBox.Items.RemoveAt(100);
		}

		//string バージョンをどう扱うか・・・
		//Modlistは設定ファイルを作る？
		private void ApplyModButton_Click(object sender, EventArgs e) {

			try {
				if (!runTaskList.All(runTask => runTask.IsCompleted)) {
					Notice("コピーが完了していません");
					return;
				}
				runTaskList.Clear();

				if (MessageBox.Show("res_modsフォルダーを一旦削除します。", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) throw new Exception();
				FileSystem.DeleteDirectory(Path.Combine(settings.WotDir, "res_mods"), UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently, UICancelOption.ThrowException);

				DialogResult resaudio = MessageBox.Show(@"res\audioフォルダーの中身をres_mods\" + settings.WotVersion + @"audioに移します(ボイスModなどを適用する場合、「はい」を選択して下さい。)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
				if (resaudio == DialogResult.Cancel) throw new Exception();
				else if (resaudio == DialogResult.Yes) AudioApply();

				IEnumerable<string> modPaths = getCheckedModPathList();

				foreach (string modPath in modPaths) {
					Console.WriteLine(modPath);
					FileSystem.CopyDirectory(modPath, settings.WotDir);
				}
			}
			catch (Exception) {
				Notice("Modの適用を中止しました");
			}


			//競合解決部分のつくりかけ
			//IEnumerable<ModInfo> selectedModInfos = getCheckedModNameList().Select(selectedMod => new ModInfo(selectedMod));
			////Tools<ModInfo>.roopExcept1(selectedModInfos)では、途中の配列を取得できない。Delegeteとか？
			//foreach (var selectedModInfo in selectedModInfos.Select((v, i) => new { v, i })) {
			//	// すべての要素について、一つを除外した全ての要素でループ
			//	// {1,2,3,4,5} => {2,3,4,5,1,3,4,5,1,2,4,5,1,2,3,5,1,2,3,4}
			//	foreach (var otherModInfo in selectedModInfos.Skip(0).Take(selectedModInfo.i).Concat(selectedModInfos.Skip(selectedModInfo.i + 1).Take(selectedModInfos.Count() - selectedModInfo.i + 1))) {
			//		selectedModInfo.v.setConflictInfo(otherModInfo);
			//	}

			//	foreach (var a in selectedModInfo.v.conflictDict) {
			//		Console.WriteLine(a);
			//	}
			//}

			//中止無視を実装したいが、別のスレッドでタスクリストを監視してイベントをわたす？イベントを渡すってどうやればいいんだろう・・・



		}

		private IEnumerable<string> getAllModNameList() {
			CheckedListBox.ObjectCollection allMods = ModCheckedListBox.Items;
			IList<string> allModList = new List<string>();
			foreach (Object allModObj in allMods) {
				allModList.Add(ModCheckedListBox.GetItemText(allModObj));
			}
			return allModList;

		}

		private IEnumerable<string> getCheckedModNameList() {
			CheckedListBox.CheckedItemCollection checkcedMods = ModCheckedListBox.CheckedItems;
			IList<string> checkcedModList = new List<string>();
			foreach (Object checkedModObj in checkcedMods) {
				checkcedModList.Add(ModCheckedListBox.GetItemText(checkedModObj));
			}
			return checkcedModList;
		}

		private IEnumerable<string> getCheckedModPathList() {
			return getCheckedModNameList().Select(modName => Path.Combine(settings.Mods, modName));
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

			var fileList = new List<string>(Directory.GetFiles(targetDir, "*", System.IO.SearchOption.AllDirectories));
			var folderList = new List<string>(Directory.GetDirectories(targetDir, "*", System.IO.SearchOption.AllDirectories));

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
				IList<string> commandList = new List<string>();
				commandList.Add("mklink");
				commandList.Add("/h");
				commandList.Add(Path.Combine(linkDir, fullLink));
				commandList.Add(file);

				executeDosCmd(String.Join(" ", commandList));
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
		public void executeDosCmd(string command) {

			var p = new Process();
			p.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
			p.StartInfo.UseShellExecute = false;
			//p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = false;
			p.StartInfo.CreateNoWindow = true;
			//"/c"は実行後閉じるために必要
			p.StartInfo.Arguments = @"/c " + command;
			p.Start();
			p.WaitForExit();
			p.Close();
		}
		//何回もcloseするのは効率が悪い気もする　
		//ハードリンクのところで時間がかかるようなら、メソッド化せずに書いていちいちcloseしないことも検討
		//メモリ食いつぶしすぎて一瞬でPCがブルスク。あぶないあぶない


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

		private Point mouseDownPoint = Point.Empty;
		//http://www.kisoplus.com/sample2/sub/listbox.html 参考
		//http://dobon.net/vb/dotnet/control/draganddrop.html
		private void ModCheckedListBox_MouseDown(object sender, MouseEventArgs e) {

			//こちらのeはクライアント画面に対して
			int indexUnderDrag = ModCheckedListBox.IndexFromPoint(new Point(e.X, e.Y));
			if (indexUnderDrag > -1) {
				//第一引数のdataとしてintで渡したときにdataを渡した先で取り出す方法はない感じかか
				ModCheckedListBox.DoDragDrop(indexUnderDrag.ToString(), DragDropEffects.Copy);
			}
		}

		private void ModCheckedListBox_DragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void ModCheckedListBox_DragDrop(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				GetModInfo(sender, e);

			else if (e.Data.GetDataPresent(DataFormats.Text))
				replaceModList(sender, e);
		}

		private void replaceModList(object sender, DragEventArgs e) {
			CheckedListBox mclb = ModCheckedListBox;

			int indexUnderDrag = int.Parse((string)e.Data.GetData(DataFormats.Text));
			//こちらのeは画面の全体に対して
			int indexUnderDrop = mclb.IndexFromPoint(mclb.PointToClient(new Point(e.X, e.Y)));

			if (-1 < indexUnderDrop && indexUnderDrop < mclb.Items.Count) {
				string draggedModName = mclb.Items[indexUnderDrag] as string;
				string droppedModName = mclb.Items[indexUnderDrop] as string;
				mclb.Items[indexUnderDrag] = droppedModName;
				mclb.Items[indexUnderDrop] = draggedModName;

				CheckState draggedState = mclb.GetItemCheckState(indexUnderDrag);
				CheckState droppedState = mclb.GetItemCheckState(indexUnderDrop);
				mclb.SetItemCheckState(indexUnderDrag, droppedState);
				mclb.SetItemCheckState(indexUnderDrop, draggedState);
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			Console.WriteLine("Debugボタン");
		}

		private void ModDeleteButton_Click(object sender, EventArgs e) {
			foreach (string checkedModName in getCheckedModNameList()) {
				try {
					FileSystem.DeleteDirectory(Path.Combine(settings.Mods, ModCheckedListBox.GetItemText(checkedModName)), UIOption.AllDialogs, RecycleOption.DeletePermanently, UICancelOption.ThrowException);
				}
				catch (Exception) {
					continue;
				}
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