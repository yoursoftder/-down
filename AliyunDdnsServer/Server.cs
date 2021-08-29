using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkHome
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();        }

        private void button2_Click(object sender, EventArgs e)
        {
            //LinkHome.AliDDnsUtils A = new AliDDnsUtils();
            //  A.SetupRecord("yourdomain","123123.d","10.168.1.11");
        // textBox2.Text=   DnsHelp.GetMyId(textBox1.Text).ToString();
        }

        private void Server_Load(object sender, EventArgs e)
        {

        }
    }
}
