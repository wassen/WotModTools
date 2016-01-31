using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace Test {
	class Program {
		static void Main(string[] args) {
			string path1 = @"C:\Users\Kazuki\Desktop\new";
			string path2 = @"C:\Users\Kazuki\Desktop\new2";

			FileSystem.CopyDirectory(@"C:\Users\Kazuki\Desktop\new2", @"C:\Users\Kazuki\Desktop\new", UIOption.AllDialogs);



			IList<int> a = new List<int> { 0, 1, 2, 3, 4 };

			foreach (var c in a.Select((v, i) => new { v, i })) {
				foreach (var b in a.Skip(0).Take(c.i).Concat(a.Skip(c.i+1).Take(a.Count()- c.i + 1))) {
					Console.WriteLine(b);
				}
			}
			Console.WriteLine();
			foreach (var c in a) {
				foreach (var b in a.Except(new int[]{ c })) {
					Console.WriteLine(b);
				}
			}
			Console.ReadKey();
		}

		public static string EncloseDoubleQuotes(string input) {
			var reg = new Regex(@"^""?|""?$");
			return reg.Replace(input, "\"", 2);
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
