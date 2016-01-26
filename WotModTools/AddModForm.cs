using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using static System.Environment;
using System.IO;
using System.Text.RegularExpressions;

namespace WotModTools {


	public partial class AddModForm : Form {
		public string ToWhichFolder { get; set; }
		public string FromWhichFolder { get; set; }
		public AddModForm() {
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e) {
		}

		private void ToWhichButton_Click(object sender, EventArgs e) {
			if (ToWhichBrowserDialog.ShowDialog() == DialogResult.OK) {

				if (isParentDir(Properties.Settings.Default.WotDir, ToWhichBrowserDialog.SelectedPath) ||
					isSamePath(Properties.Settings.Default.WotDir, ToWhichBrowserDialog.SelectedPath)) {
					this.ToWhichTextBox.Text = ToWhichBrowserDialog.SelectedPath;
				}
				else {
					ToWhichBrowserDialog.SelectedPath = this.ToWhichTextBox.Text;
					MessageBox.Show("WOTディレクトリより下のディレクトリを選択してください。WOTディレクトリは" + Properties.Settings.Default.WotDir + "です。この値は設定から変更できます。");
				}
			}
		}

		protected bool isSamePath(string argDir1, string argDir2) {
			return Program.removeLastBackSlash(argDir1) == Program.removeLastBackSlash(argDir2);
		}

		protected bool isParentDir(string target, string child) {
			target = Program.removeLastBackSlash(target);
			IEnumerable<string> parents = getParentDirectories(child);
			foreach (string parent in parents) {
				if (target == parent)
					return true;
			}
			return false;
		}



		protected IEnumerable<string> getParentDirectories(string argDir) {
			while (Path.GetPathRoot(argDir) != argDir) {
				argDir = Path.GetDirectoryName(argDir);
				yield return argDir;
			}
		}


		private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e) {

		}


		private void label3_Click(object sender, EventArgs e) {

		}

		protected virtual void OKButton_Click(object sender, EventArgs e) {

		}

		private void AddModForm_Load(object sender, EventArgs e) {

		}
	}
}
