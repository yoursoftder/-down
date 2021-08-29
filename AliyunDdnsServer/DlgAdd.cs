using System;
using System.Windows.Forms;

namespace LinkHome
{
    public partial class DlgAdd : Form
    {
        public DlgAdd()
        {
            InitializeComponent();
        }

        public DialogResult Show(string[] domains)
        {
            cboDomain.DataSource = domains;
            return this.ShowDialog();
        }


        public string Domain;
        public string RR;
        public bool DDns;

        private void btnOK_Click(object sender, EventArgs e)
        {
           
            this.Domain = cboDomain.Text;
            this.RR = txtRR.Text.Trim();
            if (this.RR == "")
            {
                this.RR = "@";
            }
            this.DDns = chkDdns.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cboDomain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DlgAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
