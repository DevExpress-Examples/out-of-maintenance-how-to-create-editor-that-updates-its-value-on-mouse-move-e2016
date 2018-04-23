using System;
using DevExpress.XtraEditors;

namespace Q245342 {
    public partial class MainForm :XtraForm {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            dataTable1.Rows.Add(0m);
        }
    }
}
