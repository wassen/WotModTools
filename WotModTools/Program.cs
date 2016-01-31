using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Text.RegularExpressions;

namespace WotModTools {



	static class Program {
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}





		public static string AddLastBackSlash(string path) {
			var reg = new Regex(@"\\?$");
			return reg.Replace(path, @"\", 1);
		}
		public static string RemoveLastBackSlash(string path) {
			return Regex.Replace(path, @"\\$", @"");
		}

		public static bool IsSamePath(string argDir1, string argDir2) {
			return Program.AddLastBackSlash(argDir1) == Program.AddLastBackSlash(argDir2);
		}

		public static bool IsParentDir(string target, string child) {
			IEnumerable<string> parents = GetParentDirectories(child);
			foreach (string parent in parents) {
				if (IsSamePath(target, parent)) return true;
			}
			return false;
		}

		public static IEnumerable<string> GetParentDirectories(string argDir) {
			while (Path.GetPathRoot(argDir) != argDir) {
				argDir = Path.GetDirectoryName(argDir);
				yield return argDir;
			}
		}

		public static string deleteHeadPath(string longPath, string shortPath) {
			if (longPath.Length < shortPath.Length) {
				return null;
			}
			longPath = Program.AddLastBackSlash(longPath);
			shortPath = Program.AddLastBackSlash(shortPath);
			return longPath.Substring(shortPath.Length, longPath.Length - shortPath.Length);
		}

		//staticでproperties使って大丈夫？
		internal static IEnumerable<string> getModFolderList() {
			foreach (string modName in Directory.GetDirectories(Properties.Settings.Default.Mods)) {
				yield return Path.GetFileName(modName);
			}
		}


	}





	//http://qiita.com/hugo-sb/items/f3afc94e7133e9c641a7
	class STATask {
		public static Task Run<T>(Func<T> func) {
			var tcs = new TaskCompletionSource<T>();
			var thread = new Thread(() => {
				try {
					tcs.SetResult(func());
				}
				catch (Exception e) {
					tcs.SetException(e);
				}
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			return tcs.Task;
		}

		public static Task Run(Action act) {
			return Run(() => {
				act();
				return true;
			});
		}



	}
	static class Tools<T> {
		/// <summary>
		/// すべての要素について、一つを除外した全ての要素でループ
		/// {1,2,3,4,5} => {2,3,4,5,1,3,4,5,1,2,4,5,1,2,3,5,1,2,3,4}
		/// </summary>
		/// <param name="elements"></param>
		/// <returns></returns>
		public static IEnumerable<T> roopExcept1(IEnumerable<T> elements) {
			foreach (var element in elements.Select((v, i) => new { v, i })) {
				foreach (var except1 in elements.Skip(0).Take(element.i).Concat(elements.Skip(element.i + 1).Take(elements.Count() - element.i + 1))) {
					yield return except1;
				}
			}
		}
	}

	class ModInfo {
		private string modName;

		public IList<string> fullFilePaths { get; set; }
		public IList<string> filePathsInWot { get; set; }
		public IDictionary<string, IEnumerable<string>> conflictDict { get; set; }

		public ModInfo(string modName) {
			this.modName = modName;
			IEnumerable<string> modNameList = Program.getModFolderList();
			conflictDict = new Dictionary<string, IEnumerable<string>>();
			string modPath = Path.Combine(Properties.Settings.Default.Mods, modName);
			fullFilePaths = Directory.GetFiles(modPath, "*", SearchOption.AllDirectories);
			filePathsInWot = fullFilePaths.Select(fullFilePath => Program.deleteHeadPath(fullFilePath, modPath)).ToList();

			if (!modNameList.Contains(modName)) {
				throw new Exception();
			}
			if (filePathsInWot.Distinct().Count() != filePathsInWot.Count()) {
				throw new Exception();
			}


		}

		public override bool Equals(object obj) {
			ModInfo arg = obj as ModInfo;
			return !(arg == null || arg.modName != this.modName);
		}

		public void setConflictInfo(ModInfo otherModInfo) {
			if (filePathsInWot.Distinct().Count() != filePathsInWot.Count()) {
				Console.WriteLine("eroroeroeoroeeeeooo");
			}
			conflictDict.Add(otherModInfo.modName, this.filePathsInWot.Intersect(otherModInfo.filePathsInWot));
		}



	}
}

//のこしたプログラム集

//使いドコロがなくなってしまった
/*
public static string EncloseDoubleQuotes(string input) {
	var reg = new Regex(@"^""?|""?$");
	return reg.Replace(input, "\"", 2);
}
*/

//Zipファイル
//string zipFile = textBox1.Text;
//if (!File.Exists(zipFile)) {
//	MessageBox.Show("ファイル名:" + zipFile + "が存在しないか、ディレクトリである可能性があります。");
//	return;
//}
//else if (Path.GetExtension(zipFile) != ".zip") {
//	MessageBox.Show("zipファイルではないようですね。");
//	return;
//}

//ZipArchive za = ZipFile.OpenRead(zipFile);

//Directory.GetFiles(workPath);

//ハードリンクは同時に開けないため断念したファイル比較
/*
//ハードリンクされた物体は片方が開かれるともう一つも開かれてることになるのか。盲点
// This method accepts two strings the represent two files to 
// compare. A return value of 0 indicates that the contents of the files
// are the same. A return value of any other value indicates that the 
// files are not the same.
private bool FileCompare(string file1, string file2) {
int file1byte;
int file2byte;
FileStream fs1;
FileStream fs2;

// Determine if the same file was referenced two times.
if (file1 == file2) {
	// Return true to indicate that the files are the same.
	return true;
}

// Open the two files.
fs1 = new FileStream(file1, FileMode.Open);
fs2 = new FileStream(file2, FileMode.Open);

// Check the file sizes. If they are not the same, the files 
// are not the same.
if (fs1.Length != fs2.Length) {
	// Close the file
	fs1.Close();
	fs2.Close();

	// Return false to indicate files are different
	return false;
}

// Read and compare a byte from each file until either a
// non-matching set of bytes is found or until the end of
// file1 is reached.
do {
	// Read one byte from each file.
	file1byte = fs1.ReadByte();
	file2byte = fs2.ReadByte();
}
while ((file1byte == file2byte) && (file1byte != -1));

// Close the files.
fs1.Close();
fs2.Close();

// Return the success of the comparison. "file1byte" is 
// equal to "file2byte" at this point only if the files are 
// the same.
return ((file1byte - file2byte) == 0);
}
*/

//FileSystem.CopyDirectoryがあった
/**
<summary>
ディレクトリのコピー
</summary>
*//*
public static void DirectoryCopy(string sourcePath, string destPath) {
	DirectoryCopy(sourcePath, destPath, true);
}

public static void DirectoryCopy(string sourcePath, string destPath, bool underDestination) {
	var sourceDirectory = new DirectoryInfo(sourcePath);
	DirectoryInfo destDirectory = Directory.CreateDirectory(underDestination ? Path.Combine(destPath, sourceDirectory.Name) : destPath);

	destDirectory.Attributes = sourceDirectory.Attributes;

	foreach (FileInfo fi in sourceDirectory.GetFiles()) {
		//常に上書き
		fi.CopyTo(Path.Combine(destDirectory.FullName, fi.Name), true);
	}
	foreach (DirectoryInfo di in sourceDirectory.GetDirectories()) {
		DirectoryCopy(di.FullName, destDirectory.FullName, true);
	}
}*/

/**
<summary>
targetDir直下にあるファイル全てをディレクトリの構造を保ったままlinkDirにハードリンクします
</summary>
<param name="linkDir">リンクを作成するディレクトリ</param>
<param name="targetDir">リンクの参照元となるディレクトリ</param>
*/
/*
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

}*/
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
/*
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
*/
//何回もcloseするのは効率が悪い気もする　
//ハードリンクのところで時間がかかるようなら、メソッド化せずに書いていちいちcloseしないことも検討
//メモリ食いつぶしすぎて一瞬でPCがブルスク。あぶないあぶない

/*メモ
一旦図に書いてみる
使っててぎこちないところとかをメモっていく。
設定ボタン右上に歯車マーク
ドラッグアンドドロップして、テキスト変更して、テキスト変更時の処理でボックスを出す、という流れならエクスプローらー固まらないかな。

ModをVoiceとその他に分ける必要性とは？
どのフォルダーに入れますか？とか聞いたらいいんじゃね？

設定ファイルに全modの情報を格納するorディレクトリ構造だけで理解する。
*/
//ディレクトリBの子ディレクトリにAが存在するかの判別方法（既に用意されてそうなもんだが・・・）
//ディレクトリBから配下を取得してAを検索 => B以下の構造が複雑で膨大なファイルがあった場合無駄な計算が発生する。親から子にたどるって直感的じゃないし。
//Replace(Stringでも、RegExでも)して文字が減ったかどうか => C:\のパターンはかぶらない気がするし、別にいいとは思うが・・・。
//charのリストにして前から空になるまで比較し続けるってのもいいか。これらのやり方は、C:\abc\hogeとC:\aとかでもまっちしてしまうな・・・
//\でsplitしてつなげる、前から切ってyield returnするなどで親ディレクトリのパターンを生成してマッチするか見る
//再帰的に親ディレクトリ辿る => これだ。

//定期的にやっちゃうアホコード
//if (target == child)
//	return true;
//else {
//	return false;
//}
