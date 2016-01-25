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
		public FormSetting() {
			InitializeComponent();
			WotVersionBox.Text = Properties.Settings.Default.WotVersion;
		}

		private void Form3_Load(object sender, EventArgs e) {

		}

		private void button1_Click(object sender, EventArgs e) {
			Properties.Settings.Default.Save();
		}

		private void wotVersionBox_TextChanged(object sender, EventArgs e) {
			Properties.Settings.Default.WotVersion = WotVersionBox.Text;
		}

		private void WotVersionBox_MouseEnter(object sender, EventArgs e) {

		}

		private void toolTip1_Popup(object sender, PopupEventArgs e) {

		}

		private void label1_Click(object sender, EventArgs e) {

		}
	}
}
