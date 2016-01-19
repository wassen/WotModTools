using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace WotModTools {
	static class Program {
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}

	public class Settings : ApplicationSettingsBase {
		[UserScopedSetting()]
		public string WotDir { get; set; }
		private string workspace;
		public string WOTVersion;
		public string Workspace
		{
			get { return workspace; }
			set
			{
				Directory.CreateDirectory(value);
				this.workspace = value;
			}
		}

		
		public Settings() {
			WotDir = @"C:\Games\World_of_Tanks\";
			this.Workspace = @"workspace\";
			WOTVersion = @"0.9.13";
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