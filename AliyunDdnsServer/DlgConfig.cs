using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms; 
namespace LinkHome
{
    public partial class DlgConfig : Form
    {
        public DlgConfig()
        {
            InitializeComponent();
        }

        public DialogResult Show(Config config)
        {
            Q();
          //  txtAccessKeyID.Text = config.AccessKeyID;
          //  txtAccessKeySecret.Text = config.AccessKeySecret;
           // txtInterval.Value = Interval;
            //.Value = TTL;
        //    chkHideForm.Checked = config.HideForm;
            return this.ShowDialog();
        }

        public Config Config;

        private void btnOK_Click(object sender, EventArgs e)
        {
            int a = 1;
            if (!int.TryParse(comboBox2.Text,out a))
            {
                MessageBox.Show("请不要输入特殊字符在端口");
                return;
            }
           MessageBox.Show( save());
            var config = new Config();
            config.AccessKeyID = txtAccessKeyID.Text;
            config.AccessKeySecret = txtAccessKeySecret.Text;
            //config.Interval = (int)txtInterval.Value;
          //  config.TTL = (int)txtTTL.Value;
            config.HideForm = chkHideForm.Checked;
            Config = config;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnHowToGetAccessKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://help.aliyun.com/knowledge_detail/48699.html");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        internal     string save ()
        {
            string v = "";
            try
            {
             if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Domain.set"))//不存在则创建文件
                {
                    // File.Create(Application.StartupPath.ToString() + "/app.set").Close();
                    StreamWriter sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Domain.set");//准备写入文件
                    sw.WriteLine("解析工具配置信息");//写入文件开始0
                    sw.WriteLine("AccessKey ID:");//1
                    sw.WriteLine("AccessKey Secret:");//2
                    sw.WriteLine("TTl:600");//3
                    sw.WriteLine("IP检查刷新频率(秒):30");//4
                    sw.WriteLine("是否启动后自动刷新:否");//5
                    sw.WriteLine("接收客户端服务端口:80");//6
                    sw.WriteLine("是否启动后隐藏:否");//7
                    sw.WriteLine("-");//8
                    sw.WriteLine("-");//9
                    sw.WriteLine("-" );//10
                    sw.WriteLine("-");//11
                    sw.WriteLine("-");//12
                    sw.WriteLine("-");//13
                    sw.WriteLine("- ");//14
                    sw.WriteLine("- ");//15
                    sw.WriteLine("首次打开日期:" + DateTime.Now.ToString("yyyy-MM-dd"));//16
                    sw.WriteLine("-");//17
                    sw.Flush();
                    sw.Close();//写入文件结束
                    v = "请重试";
                }
                else
                {
                    string[] content = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Domain.set").Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);//准备读取文件
                 
                   
                    content[1] = "AccessKey ID:"+txtAccessKeyID.Text; //写 
                    content[2] = "AccessKey Secret:" + txtAccessKeySecret.Text; //写 
                    content[3] = "TTl:" + txtTTL.Text; //写 
                    content[4] = "IP检查刷新频率(秒):" + txtInterval.Text; //写
                    content[5] = "是否启动后自动刷新:" + comboBox1.Text; //写
                    content[6] = "接收客户端服务端口:" + comboBox2.Text; //写
                    content[7] = "是否启动后隐藏:" + (chkHideForm.Checked ?"是":"否"); //写
                    //chkHideForm.Checked
                    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Domain.set", string.Join("\r\n", content));//将内容换行保存在本地TXT中开始
                    v = "保存成功,确定后等待系统检查!";

                }
            }
            catch (Exception e)
            {
                 
                    return e.Message.ToString();
              //  reg.Log("注册文件访问错误>>" + e.Message.ToString());

               // WriteSet();
            }
            return v;
        }

        private void chkHideForm_CheckedChanged(object sender, EventArgs e)
        {

        }
        public void Q()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Domain.set"))//不存在则创建文件
            {
                // File.Create(Application.StartupPath.ToString() + "/app.set").Close();
                StreamWriter sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Domain.set");//准备写入文件
                sw.WriteLine("解析工具配置信息");//写入文件开始0
                sw.WriteLine("AccessKey ID:");//1
                sw.WriteLine("AccessKey Secret:");//2
                sw.WriteLine("TTl:600");//3
                sw.WriteLine("IP检查刷新频率(秒):30");//4
                sw.WriteLine("是否启动后自动刷新:否");//5
                sw.WriteLine("接收客户端服务端口:80");//6
                sw.WriteLine("是否启动后隐藏:否");//7
                sw.WriteLine("-");//8
                sw.WriteLine("-");//9
                sw.WriteLine("-");//10
                sw.WriteLine("-");//11
                sw.WriteLine("-");//12
                sw.WriteLine("-");//13
                sw.WriteLine("- ");//14
                sw.WriteLine("- ");//15
                sw.WriteLine("首次打开日期:" + DateTime.Now.ToString("yyyy-MM-dd"));//16
                sw.WriteLine("-");//17
                sw.Flush();
                sw.Close();//写入文件结束
            }
            else
            {
                string[] content = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Domain.set").Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);//准备读取文件

                txtAccessKeyID.Text = content[1].Split(':')[1].ToString();//将文本的内容赋值给TextBox;
                txtAccessKeySecret.Text = content[2].Split(':')[1].ToString();
                txtTTL.Text = content[3].Split(':')[1].ToString();
                txtInterval.Text = content[4].Split(':')[1].ToString();
                comboBox1.Text = content[5].Split(':')[1].ToString();
                comboBox2.Text = content[6].Split(':')[1].ToString();
                chkHideForm.Checked = (content[7].Split(':')[1].ToString() == "是")?true:false;
            }
        }
        private void DlgConfig_Load(object sender, EventArgs e)
        {
           
        }
    }
}
