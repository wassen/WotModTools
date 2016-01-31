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
				foreach (var b in a.Skip(0).Take(c.i).Concat(a.Skip(c.i + 1).Take(a.Count() - c.i + 1))) {
					Console.WriteLine(b);
				}
			}
			Console.WriteLine();
			foreach (var c in a) {
				foreach (var b in a.Except(new int[] { c })) {
					Console.WriteLine(b);
				}
			}
			Console.ReadKey();
		}
	}
}








	