using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2 {
    public partial class FormText : Form {

        string input = "";

        public FormText() {
            InitializeComponent();
        }

        public FormText(string input) {
            InitializeComponent();

            this.input = input;
        }

        private void FormText_Load(object sender, EventArgs e) {
            textBox1.Text = input;
        }
    }
}
