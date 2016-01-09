using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WotModTool {
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

	public class Setting {
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


		public Setting() {
			WotDir = @"C:\Games\World_of_Tanks\";
			this.Workspace = @"workspace\";
			WOTVersion = @"0.9.13";
		}

	}
}
