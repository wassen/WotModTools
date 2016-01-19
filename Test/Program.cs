using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Test {
	class Program {
		static void Main(string[] args) {

			var a = new FileInfo(@"C:\Games\World_of_Tanks\res_mods\0.9.13\audio\ambient.fev");
			Console.WriteLine(a.GetHashCode());
			Console.ReadKey();

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

*/
