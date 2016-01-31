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
		private Point mouseDownPoint = Point.Empty;

		public MainForm() {
			InitializeComponent();
			modsFolderFileSystemWatcher.Path = settings.Mods;
			sw.Start();
			LoadModList();
			//サイズ変更の禁止
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
		}

		private void LoadModList() {

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
					string toWhichFolder;
					var wfForm = new InputModInfoForm(paths);
					try {
						this.OpenForm(wfForm);

						if (wfForm.DialogResult != DialogResult.Yes) {
							Notice("キャンセルされました。");
							return;
						}
						toWhichFolder = wfForm.ToWhichFolder;
						this.CloseForm(wfForm);
					}
					catch (Exception) {
						Notice("Modの追加中、意図せぬエラーが発生しました。");
						return;
					}
					string[] droppedFilePaths = paths.Where(item => File.Exists(item)).ToArray<string>();
					string[] droppedDirectoryPaths = paths.Where(item => Directory.Exists(item)).ToArray<string>();
					try {
						foreach (string droppedFilePath in droppedFilePaths) {
							FileSystem.CopyFile(droppedFilePath, toWhichFolder, UIOption.AllDialogs);
						}
						foreach (string droppedDirectoryPath in droppedDirectoryPaths) {
							FileSystem.CopyDirectory(droppedDirectoryPath, Path.Combine(toWhichFolder, Path.GetFileName(droppedDirectoryPath)), UIOption.AllDialogs);
						}
						Notice("コピーが完了しました。");
					}
					catch (Exception) {
						Notice("コピーがキャンセルされました");
						return;
					}
					//プログレスバー表示のためデリゲートでやるの断念
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

		private void AudioApply() {
			string resAudioPath = Path.Combine(settings.WotDir, "res", "audio");
			string res_modsAudioPath = Path.Combine(settings.WotDir, "res_mods", settings.WotVersion, "audio");

			//速度が遅すぎるためハードリンクは断念
			//HardLinks(res_modsAudioPath, resAudioPath);
			FileSystem.CopyDirectory(resAudioPath, res_modsAudioPath, UIOption.AllDialogs);
		}

		private void Notice(string str) {
			consoleListBox.Items.Insert(0, str);
			if (consoleListBox.Items.Count > 100) consoleListBox.Items.RemoveAt(100);
		}

		private void ApplyModButton_Click(object sender, EventArgs e) {
			try {
				if (ModCheckedListBox.Items.Count == 0) {
					Notice("Modにチェックを入れて下さい。");
					return;
				}
				if (!runTaskList.All(runTask => runTask.IsCompleted)) {
					Notice("コピーが完了していません");
					return;
				}
				runTaskList.Clear();

				if (Directory.Exists(Path.Combine(settings.WotDir, "res_mods"))) {
					if (MessageBox.Show("res_modsフォルダーを一旦削除します。", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) throw new Exception();
					FileSystem.DeleteDirectory(Path.Combine(settings.WotDir, "res_mods"), UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently, UICancelOption.ThrowException);
				}
				DialogResult resaudio = MessageBox.Show(@"res\audioフォルダーの中身をres_mods\" + settings.WotVersion + @"audioに移します(ボイスModなどを適用する場合、「はい」を選択して下さい。)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
				if (resaudio == DialogResult.Cancel) throw new Exception();
				else if (resaudio == DialogResult.Yes) AudioApply();


				IEnumerable<string> modPaths = getCheckedModPathList();

				foreach (string modPath in modPaths) {
					FileSystem.CopyDirectory(modPath, settings.WotDir, true);
					Notice(Path.GetFileName(modPath) + "の適用が完了しました。");
				}
			}
			catch (Exception) {
				Notice("Modの適用を中止しました");
			}
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

		private void SettingButton_Click(object sender, EventArgs e) {
			var fs = new SettingForm();
			OpenForm(fs);
			CloseForm(fs);
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
		//CloseのOverrideするべきか。すればusingが使えるはず
		private void CloseForm(Form form) {
			form.Close();
			openingFormList.Remove(form);
		}
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			openingFormList.ForEach(form => form.Close());
		}

		//http://www.kisoplus.com/sample2/sub/listbox.html 参考
		//http://dobon.net/vb/dotnet/control/draganddrop.html
		private void ModCheckedListBox_MouseDown(object sender, MouseEventArgs e) {
			//左ボタン限定
			if (e.Button == MouseButtons.Left && ModCheckedListBox.IndexFromPoint(e.X, e.Y) >= 0) {
				mouseDownPoint = new Point(e.X, e.Y);
			}
			else
				mouseDownPoint = Point.Empty;
		}

		private void ModCheckedListBox_MouseMove(object sender, MouseEventArgs e) {
			if (mouseDownPoint != Point.Empty) {
				Rectangle moveRect = new Rectangle(
					mouseDownPoint.X - SystemInformation.DragSize.Width / 2,
					mouseDownPoint.Y - SystemInformation.DragSize.Height / 2,
					SystemInformation.DragSize.Width,
					SystemInformation.DragSize.Height);

				if (!moveRect.Contains(e.X, e.Y)) {
					//こちらのeはクライアント画面に対して
					int indexUnderDrag = ModCheckedListBox.IndexFromPoint(mouseDownPoint);
					if (indexUnderDrag > -1) {
						//第一引数のdataとしてintで渡したときにdataを渡した先で取り出す方法はない？
						ModCheckedListBox.DoDragDrop(indexUnderDrag.ToString(), DragDropEffects.Copy);
					}
					mouseDownPoint = Point.Empty;
				}
			}
		}

		private void ModCheckedListBox_MouseUp(object sender, MouseEventArgs e) {
			mouseDownPoint = Point.Empty;
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

		private void ModDeleteButton_Click(object sender, EventArgs e) {

		}

		private void ModCheckedListBox_DoubleClick(object sender, EventArgs e) {
			try {
				if (ModCheckedListBox.Items.Count == 0) {
					return;
				}
				FileSystem.DeleteDirectory(Path.Combine(settings.Mods, ModCheckedListBox.GetItemText(ModCheckedListBox.SelectedItem)), UIOption.AllDialogs, RecycleOption.DeletePermanently, UICancelOption.ThrowException);
			}
			catch (Exception) {
				Notice("削除を中止しました。");
			}
		}

		private void MainForm_Load(object sender, EventArgs e) {
		}
	}
}


/*メモ			
	getAllModNameListはforeachの最中にyield returnしているため、途中でModChechedListBoxを変更するところでバグる。
	Listのコンストラクタに突っ込んで初期化したりしてたが、メソッド側をIListとかに集めてから返すように変更した。
	
	PathCombine使いすぎ問題
	特定のフォルダーは設定として定義した方が良い？
	
	Enumでエラーを返す？
	
	
	未実装 
	TODO フォルダー内のModっぽいフォルダーを探索、D&Dしたフォルダの中でどのフォルダがModかの選択
	=>単純にしたほうがわかりやすい気がしたので実装後削除

	TODO 既にD&DでModが入っている時
	=>重要度低い

	TODO HardLinksの速度遅すぎ問題、解決できたら、Hardlinksのプログレスバー
	copyとhardlinkで引数の扱いが違うのも気になる
	
	TODO Modの一部削除(被りの記録)
	=>だいぶ有用かと

	TODO D&D以外でフォルダ選択、参照したいんだが？

	TODO バージョンが上がった時に同じModを適用できるようなメソッドを作りたい。フォルダ名のReplace

	TODO 次回起動時にModListが保持されるようにしたい
	
	TODO コピーが完了していませんではなく、"完了するまでお待ち下さい、"中止無視再試行"　を実装したい

	TODO Modの情報を変更するフォームを継承して作りたい。

	TODO Settingの充実

	//競合解決部分のつくりかけ
	//IEnumerable<ModInfo> selectedModInfos = getCheckedModNameList().Select(selectedMod => new ModInfo(selectedMod));
	////Tools<ModInfo>.roopExcept1(selectedModInfos)では、途中の配列を取得できない。Delegeteとか？
	//foreach (var selectedModInfo in selectedModInfos.Select((v, i) => new { v, i })) {
	//	// すべての要素について、一つを除外した全ての要素でループ
	exceptはうまく働かない。オブジェクトIDは違うのか？
	//	// {1,2,3,4,5} => {2,3,4,5,1,3,4,5,1,2,4,5,1,2,3,5,1,2,3,4}
	//	foreach (var otherModInfo in selectedModInfos.Skip(0).Take(selectedModInfo.i).Concat(selectedModInfos.Skip(selectedModInfo.i + 1).Take(selectedModInfos.Count() - selectedModInfo.i + 1))) {
	//		selectedModInfo.v.setConflictInfo(otherModInfo);
	//	}
	
	//	foreach (var a in selectedModInfo.v.conflictDict) {
	//		Console.WriteLine(a);
	//	}
	//}

*/
