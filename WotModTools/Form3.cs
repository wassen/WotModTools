using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WotModTools {
	public partial class FormSetting : Form {
		Settings settings;
		public FormSetting(Settings settings) {
			InitializeComponent();
			this.settings = settings;
			textBox1.Text = settings.WOTVersion;
		}

		private void Form3_Load(object sender, EventArgs e) {

		}

		private void textBox1_TextChanged(object sender, EventArgs e) {

		}

		private void button1_Click(object sender, EventArgs e) {

		}
	}
}
