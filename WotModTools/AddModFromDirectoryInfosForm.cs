using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Environment;

namespace WotModTools {
	public partial class AddModFromDirectoryInfosForm : InputModInfoForm {
		public AddModFromDirectoryInfosForm(IEnumerable<DirectoryInfo> droppedDirectoryInfos) {
		}

		private void AddModFromDirectoryInfosForm_Load(object sender, EventArgs e) {

		}

		//TODO ディレクトリが一つだったら、その直下を入れると考えたほうが良いのか？FromWhichやっぱりいる？ここが悩みどころだけど、今は修正せずにいこう。最終的にはres_modsかaudioあたりをはんべつすればいや
		//protected override void OKButton_Click(object sender, EventArgs e) {


		//}





	}
}