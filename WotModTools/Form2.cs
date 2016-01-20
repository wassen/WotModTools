using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotModTools {
	public partial class ModForm : Form {

		public string TextBox { get; set; }

		public ModForm(string modName) {
			InitializeComponent();
			label2.Text = modName;
			textBox1.Text = modName;
		}

		private void Form2_Load(object sender, EventArgs e) {

		}

		private void textBox1_TextChanged(object sender, EventArgs e) {
			TextBox = textBox1.Text;
		}

		private void button1_Click(object sender, EventArgs e) {
			//空白ですよとか、exitしても委員会？とか教えてあげたい感じ。
			//this.Close();
		}

		private void label2_Click(object sender, EventArgs e) {

		}
	}
}
