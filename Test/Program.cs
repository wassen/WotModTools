﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace Test {
	class Program {
		static void Main(string[] args) {
			string a = @"C:\Users\Kazuki\Desktop\new";
			string b = @"C:\Users\Kazuki\Desktop\new2";

			DirectoryCopy(b, a);

			IList<string> c = new List<string>();
			c.Add(null);
			Console.WriteLine(c.Contains(null));

			Console.ReadKey();
		}

		public static string EncloseDoubleQuotes(string input) {
			var reg = new Regex(@"^""?|""?$");
			return reg.Replace(input, "\"", 2);
		}

		/**
			<summary>
			ディレクトリのコピー
			</summary>
		*/
		public static void DirectoryCopy(string sourcePath, string destPath) {
			DirectoryCopy(sourcePath, destPath, false);
		}
		
		public static void DirectoryCopy(string sourcePath, string destPath, bool underDestination) {

			DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
			DirectoryInfo destDirectory = Directory.CreateDirectory(underDestination ? Path.Combine(destPath, sourceDirectory.Name) : destPath);

			destDirectory.Attributes = sourceDirectory.Attributes;

			foreach (FileInfo fi in sourceDirectory.GetFiles()) {
				//常に上書き
				fi.CopyTo(Path.Combine(destDirectory.FullName, fi.Name), true);
			}

			foreach (DirectoryInfo di in sourceDirectory.GetDirectories()) {
				DirectoryCopy(di.FullName, destDirectory.FullName + @"\" + di.Name);
			}
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
