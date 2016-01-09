using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace WotModTool {
	public partial class Form1 : Form {

		Setting setting;
		const string settingFilename = @"settings.wmt";

		public Form1() {

			setting = new Setting();

			//設定ファイルの読み込み
			if (File.Exists(settingFilename)) {
				var ser = new XmlSerializer(typeof(Setting));
				StreamReader sr = new StreamReader(settingFilename, new UTF8Encoding(false));
				setting = (Setting)ser.Deserialize(sr);
				sr.Close();
			}

			//設定ファイルの書き込み
			//using (var sw = new StreamWriter(fileName, false, new UTF8Encoding(false))) {
			//	ser.Serialize(sw, setting);
			//}

			InitializeComponent();

			//workspace\audioフォルダーから、AudioModの一覧を生成
			string audioFolder = Path.Combine(setting.Workspace, "audio");
			if (Directory.Exists(audioFolder)) {
				var audioDirList1 = new List<string>(Directory.GetDirectories(audioFolder));
				IEnumerable<string> audioNameList = audioDirList1.Select(e => Path.GetFileName(e));
				foreach (string name in audioNameList) {
					listBox2.Items.Add(name);
				}
			}


		}

		private void textBox1_TextChanged(object sender, EventArgs e) {

		}

		private void button1_Click(object sender, EventArgs e) {

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

			HardLinks(Path.Combine(setting.WotDir, "res_mods", setting.WOTVersion, "audio_test"), Path.Combine(setting.Workspace, "audio"));
		}

		//targetDir直下にあるファイル全てをlinkdirにハードリンクします
		public void HardLinks(string linkDir, string targetDir) {

			//ディレクトリ存在チェック
			if (!Directory.Exists(linkDir) || !Directory.Exists(targetDir)) {
				Console.WriteLine("ディレクトリが存在しません");
				return;
			}

			string targetDir2 = Regex.Replace(targetDir, "\\?$", "\\");
			var tdReg = new Regex(targetDir2);

			var fileList = new List<string>(Directory.GetFiles(targetDir, "*", SearchOption.AllDirectories));
			var folderList = new List<string>(Directory.GetDirectories(targetDir, "*", SearchOption.AllDirectories));

			foreach (string folder in folderList) {
				//targetDir\B1F\B2F\fileName - targetDir = B1F\B2F\fileName を作成
				//subStringでやってたけどなんか長くて気持ち悪かったので正規表現で
				//Path.ComBineはファイル名の先頭に\が付いてるのを許さないため
				string folderName = tdReg.Replace(folder, "", 1);

				Directory.CreateDirectory(Path.Combine(linkDir, folderName));
			}

			foreach (string file in fileList) {
				string fileName = tdReg.Replace(file, "", 1);

				//TODO Folderに';'が含まれてるとリンクが作られなかったため要調査
				Mklink(@"/H", Path.Combine(linkDir, fileName), file);
			}
		}

		//"D"irectory "H"ardlink "J"unction
		public void Mklink(string type, string link, string target) {
			Process p = new System.Diagnostics.Process();
			p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
			//出力を読み取れるようにする
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = false;
			//ウィンドウを表示しないようにする
			p.StartInfo.CreateNoWindow = true;
			//コマンドラインを指定（"/c"は実行後閉じるために必要
			p.StartInfo.Arguments = @"/c mklink " + type + @" " + link + @" " + target;
			p.Start();
			//出力を読み取る
			string results = p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			//何回もcloseするのは効率が悪いのか？
			p.Close();
			Console.WriteLine(@"/c mklink " + type + @" " + link + @" " + target);
			Console.WriteLine(results);
		}

		private void Form1_DragEnter(object sender, DragEventArgs e) {
			//コントロール内にドラッグされたとき実行される
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				//ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
				e.Effect = DragDropEffects.Copy;
			else
				//ファイル以外は受け付けない
				e.Effect = DragDropEffects.None;
		}

		private void Form1_DragDrop(object sender, DragEventArgs e) {

			//コントロール内にドロップされたとき実行される
			//ドロップされたすべてのファイル名を取得する
			string[] folderName =
				(string[])e.Data.GetData(DataFormats.FileDrop, false);
			//TextBoxに追加する
			//Listにしてあげたい。2つ以上の処理。
			//一つのファイルに複数のaudioがあった時も未実装
			textBox1.Text = folderName[0];



		}
		private string InputModName() {
			var inputBox = new Form2();
			inputBox.ShowDialog();

			if (inputBox.DialogResult == DialogResult.OK) {
				string input = inputBox.TextBox;
				inputBox.Dispose();

				return input;
			}
			else {
				Console.WriteLine("きゃんせりんぐー");
				return null;
			}

		}

		private void Form1_Load(object sender, EventArgs e) {

		}

		private void textBox2_TextChanged(object sender, EventArgs e) {

		}

		private void label1_Click(object sender, EventArgs e) {

		}

		private void label2_Click(object sender, EventArgs e) {

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void button2_Click(object sender, EventArgs e) {
		}

		private void echo(string str) {
			Console.WriteLine(str);
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void button3_Click(object sender, EventArgs e) {
			if (!Directory.Exists(textBox1.Text)) {
				listBox1.Items.Add("フォルダーじゃない気がする");
				return;
			}

			bool audioFolderFlag = false;

			DirectoryInfo diInfo = new DirectoryInfo(textBox1.Text);
			var finfos = new List<FileInfo>();
			finfos.AddRange(diInfo.GetFiles("*.FEV", SearchOption.AllDirectories));//個人的メモ .FEVって書いてるけど、.fevも認識できる様。
			finfos.AddRange(diInfo.GetFiles("*.FSB", SearchOption.AllDirectories));

			//親ディレクトリの重複確認
			var dirNameList = new List<string>();
			foreach (FileInfo info in finfos) {
				dirNameList.Add(info.DirectoryName);
			}
			IEnumerable<string> unique = dirNameList.Distinct();
			if (unique.Count() != 1) {
				listBox1.Items.Add("fev、fsbファイルが存在しないか、複数のフォルダに点在しています。");
				return;
			}

			//modに名前付けてもらう。デフォルトで突っ込んだディレクトリの名前を出してもいいか？
			string input = InputModName();
			if (input == null) {
				listBox1.Items.Add("キャンセル！！！");
				return;
			}

			string workFolder = Path.Combine(setting.Workspace, "audio", input);

			if (Directory.Exists(workFolder)) {
				listBox1.Items.Add("もう" + input + "はあるで");
				return;
			}

			Directory.CreateDirectory(workFolder);

			foreach (FileInfo info in finfos) {
				info.CopyTo(Path.Combine(workFolder, info.Name));
			}

			//常にListBoxに足して、逆にsettingの方に要所要所で同期していく感じで。というか、フォルダーから取得したほうが確実か。

			listBox2.Items.Add(input);

			//Directory.GetDirectories();


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
		}

		private void listBox2_SelectedIndexChanged(object sender, EventArgs e) {

		}
	}
}
