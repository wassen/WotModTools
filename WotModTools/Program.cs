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


		/**
			<summary>
			ディレクトリのコピー
			</summary>
		*/
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
		public static IEnumerable<string> getModNameList() {
			foreach (string modName in Directory.GetDirectories(Properties.Settings.Default.Mods){
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
		public static IEnumerable<T> roopExcept1(IEnumerable<T> ts) {
			foreach (var t in ts.Select((v, i) => new { v, i })) {
				foreach (var b in ts.Skip(0).Take(t.i).Concat(ts.Skip(t.i + 1).Take(ts.Count() - t.i + 1))) {
					yield return b;
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
			IEnumerable<string> modNameList = Program.getModNameList();
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



//TODO audioファイルが入ってないプログラムはダメなので修正
//TODO FEVとFSBだけしかファイルがないと仮定してもいいのか？
//Zipじゃなくても出来るようにしたい。if文分岐かな。
/*
zipファイル内のaudioディレクトリを検知
ハードリンクをaudioの全てのファイルについて張る(audioディレクトリ内を除く)
audioファイル内をリンク張る
*/

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

//hardDirLink
//深すぎフォルダ問題

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
