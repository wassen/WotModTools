using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace Test {
	class Program {
		static void Main(string[] args) {
			string a = @"""C:\Users\Kazuki\Desktop\geton_1.02 - コピー""";
			string b = @"""C:\Users\Kazuki\Desktop\新しいフォルダー (2)\asdf""";

			var p = new Process();

			p.StartInfo.FileName = "mklink";            // コマンド名
			p.StartInfo.Arguments = @"/h ""C: \Users\Kazuki\Desktop\新しいフォルダー\asdf.jpg"" ""C: \Users\Kazuki\Desktop\50870432_p0.jpg""";               // 引数
			p.StartInfo.CreateNoWindow = true;            // DOSプロンプトの黒い画面を非表示
			p.StartInfo.UseShellExecute = true;          // プロセスを新しいウィンドウで起動するか否か
			Stopwatch sw = new Stopwatch();
			sw.Start();
				
			p.Start();
			Console.WriteLine(sw.Elapsed);

			Console.ReadKey();
		}

		public static string EncloseDoubleQuotes(string input) {
			var reg = new Regex(@"^""?|""?$");
			return reg.Replace(input, "\"", 2);
		}

		public static string DOSCmd(string command) {
			Process p = OpenProcess(command);
			p.Start();
			string results = p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			p.Close();
			return results;
		}

		private static Process OpenProcess(string command) {
			var p = new Process();
			p.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
			//出力を読み取れるようにする
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = false;
			p.StartInfo.CreateNoWindow = true;
			//"/c"は実行後閉じるために必要
			p.StartInfo.Arguments = @"/c " + command;
			return p;
		}
	}

}

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
