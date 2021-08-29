using Aliyun.Acs.Alidns.Model.V20150109;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace LinkHome
{
    public partial class FrmMain : Form
    { 

        [Serializable]
        class DomainInfo
        {
            public string Subdomain;
            public bool DDns;
         
            [NonSerialized]
            public string ID;

            [NonSerialized]
            public DataGridViewRow Row;

            [NonSerialized]
            public string LastIP;


            public override string ToString()
            {
                return $"{Subdomain}, {DDns}, {Row}";
            }
        }

        const string settingsPath = @".\settings.bin";

        Dictionary<string, DomainInfo> infos;
        Config config;
        AliDDnsUtils ali;
        public Thread tr;
        public FrmMain()
        {
            InitializeComponent();
             //  tr = new Thread(new ThreadStart(refresh));
        }   


        T invoke<T>(Func<T> act)
        {
            return (T)this.Invoke(act);
        }    
  

        string makeSubdomain(string rr, string domain)
        {
            return rr == "@" ? domain : $"{rr}.{domain}";
        }

        T @try<T>(Func<T> func, T defaultValue)
        {
            try
            {
                return func();
            }
            catch
            {
                return defaultValue;
            }
        }

        void errorHandler(string message)
        {
            lblStatus.Text = message;
        }
        public static int TTL = 600;
        public static int Interval = 10;
        public static int Interval2 = 10;
        public static string  Auto = "否";
        public static int port = 80;
        public static string Hide = "否";
        public static string GetSet()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Domain.set"))//不存在则创建文件
            {
                return "请到设置中配置Api信息";
            }
            else
            {
                string[] content = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Domain.set").Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);//准备读取文件

                AliDDnsUtils.AccessKeyID = content[1].Split(':')[1].ToString();//将文本的内容赋值给TextBox;
                AliDDnsUtils.AccessKeySecret = content[2].Split(':')[1].ToString();
                TTL = int .Parse (content[3].Split(':')[1].ToString());
                Interval2 = Interval = int .Parse(content[4].Split(':')[1].ToString());
                Auto = content[5].Split(':')[1].ToString();
                port = int.Parse(content[6].Split(':')[1].ToString());
                Hide = content[7].Split(':')[1].ToString();
                 
                return Httpserver();
            }
        }
      
        internal static string Httpserver()
        {
            try
            {
                string url = "http://localhost:"+ port;
                //Assembly.Load("WebApiClient.TestApi");  //手工加载某个api程序集的controller
                var config = new HttpSelfHostConfiguration(url);
                config.MaxBufferSize = int.MaxValue;
                config.MaxReceivedMessageSize = int.MaxValue;
                config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
                config.Routes.MapHttpRoute("default", "{controller}/{id}", new { id = RouteParameter.Optional });
                var server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();
                return ("Http服务启动成功 端口是 "+ port+"\r\n");
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("服务开启失败" + ex.Message.ToString()+"请用管理员权限运行");
                return port + "服务启动不成功.请用管理员权限运行";// + ex.ToString();
            }

        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            textBox1.Text = GetSet();
            loadSettings();
            notifyIcon.Text = this.Text;
            notifyIcon.Icon = this.Icon;
            FrmMain_Resize(null, null);
            if (Auto == "是")
            {
                new Thread(refreshProc) { IsBackground = true }.Start();
            }
        }

        void refresh()
        {

            if (string.IsNullOrWhiteSpace(AliDDnsUtils.AccessKeyID) || string.IsNullOrWhiteSpace(AliDDnsUtils.AccessKeySecret))
            {
                lblStatus.Text = "请先在设置中配置AccessKeyID和AccessKeySecret后重新打开"; 
                return;
            }

            lock (infos)
            {
                var ip = ServerIP.Get(errorHandler);
                var domains = ali.GetDomains();
                var exist = new HashSet<string>();
                var Dip = ""; var Rid = ""; var head = "";var domainfull = "";
                foreach (var domain in domains)
                { 
                    foreach (var rec in ali.GetRecords(domain))
                    {
                        if (rec.Type != "A" && rec.Type != "CNAME")
                        {
                            continue;
                        }
                        Rid = rec.RecordId;
                        Dip = rec._Value;
                        head = rec.RR;
                        domainfull = head+"."+rec.DomainName;
                        textBox1.AppendText(DnsHelp.DomainLog(head + "*" + Rid + "*" + Dip+"*"+ domainfull));
                        var key = makeSubdomain(rec.RR, rec.DomainName);
                        exist.Add(key);

                        //
                        infos.TryGetValue(key, out var info);

                        if (info == null)
                        {
                            info = new DomainInfo
                            {
                                DDns = false,
                                Subdomain = key
                            };
                            infos.Add(key, info);
                        }

                        if (info.Row == null)
                        {
                            var rowIndex = invoke(() => grid.Rows.Add(key, info.DDns, ""));
                            info.Row = grid.Rows[rowIndex];
                        }

                        // 
                        var localDnsStatus = @try(() => Dns.GetHostAddresses(key).FirstOrDefault(i => i.ToString() == ip) != null, false);
                        var dnsConfigStatus = rec._Value == ip && rec.Type == "A"; 
                       

                        if (info.DDns)
                        {
                            if (localDnsStatus && dnsConfigStatus)
                            {
                              //  status = "正常";
                            }
                            else
                            {
                               // status = $"DNS配置{(dnsConfigStatus ? "正确" : "不正确")}，本地解析{(localDnsStatus ? "正确" : "不正确")}";
                            }

                            //
                            if (rec._Value != ip)
                            {
                                ali.UpdateRecordIP(rec, ip);
                                info.LastIP = ip;
                            }
                        }
                        info.Row.Cells[2].Value = Dip;//解析的ip
                        info.Row.Cells[3].Value = Rid;//recordid
                        info.Row.Cells[4].Value = head;//头
                        //
                      //  info.ID = rec.RecordId;

                    }
                }

                var notexists = infos.Keys.Where(i => !exist.Contains(i)).ToArray();
                if (notexists.Count() > 0)
                {
                    foreach (var k in notexists)
                    {
                        var i = infos[k];
                        if (i.Row != null)
                        {
                            grid.Rows.Remove(i.Row);
                        }
                        infos.Remove(k);
                    }
                    saveSettings();
                }
                lblStatus.Text = $"当前IP：{ip} 最后更新时间：{DateTime.Now}";
            }

        }

        void refreshProc()
        {
            while (true)
            {
                refresh();
                Thread.Sleep(Interval * 1000);
            }
        }

        private void FrmMain_TextChanged(object sender, EventArgs e)
        {
            notifyIcon.Text = this.Text;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            if (Hide=="是")
            {
                this.Hide();
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                var row = grid.Rows[e.RowIndex];
                var info = infos.Values.First(i => i.Row == row);
                info.DDns = !info.DDns;
                row.Cells[1].Value = info.DDns;
                saveSettings();
                new Thread(refresh).Start();
            }
        }


        void saveSettings()
        {
            using var file = File.OpenWrite(settingsPath);
            using var zip = new GZipStream(file, CompressionLevel.Fastest);
            var formatter = new BinaryFormatter();
            formatter.Serialize(zip, new object[] { infos, config });
        }

        void loadSettings()
        {
            try
            {
                using var file = File.OpenRead(settingsPath);
                using var zip = new GZipStream(file, CompressionMode.Decompress);

                var formatter = new BinaryFormatter();
                var settings = formatter.Deserialize(zip) as object[];
                if (settings == null)
                {
                    return;
                }
                infos = (Dictionary<string, DomainInfo>)settings[0];
                config = (Config)settings[1];
            }
            catch
            {
                infos = new Dictionary<string, DomainInfo>();
                config = new Config();
            }
            remakeAli();

        }

        void remakeAli()
        {
            ali = new AliDDnsUtils
            {
               // AccessKeyID = config.AccessKeyID,
               // AccessKeySecret = config.AccessKeySecret,
                TTL = TTL,
                ErrorHandler = errorHandler
            };
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {

            grid.Location = new Point(0, toolStrip.Bottom);
            grid.Size = new Size(
                ClientSize.Width,
                ClientSize.Height - statusStrip.Height - toolStrip.Height - textBox1.Height-button1.Height
            ) ;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new DlgAdd();
            var domains = from i in ali.GetDomains()
                          select i.DomainName;

            while (true)
            {
                if (dlg.Show(domains.ToArray()) != DialogResult.OK)
                {
                    return;
                }

                var key = makeSubdomain(dlg.RR, dlg.Domain);
                if (!infos.ContainsKey(key))
                {
                    ali.SetupRecord(dlg.Domain, dlg.RR, dlg.DDns ? ServerIP.Get(errorHandler) : comboBox3.Text);
                    var info = new DomainInfo
                    {
                        DDns = dlg.DDns,
                        Subdomain = key,
                        Row = null
                    };
                    infos.Add(key, info);
                    refresh();
                    return;
                }

                MessageBox.Show("域名已经存在，请直接在列表中操作。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要删除吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            lock (infos)
            {
                foreach (DataGridViewRow row in grid.SelectedRows)
                {
                    var info = infos.Values.First(i => i.Row == row);
                    ali.DeleteRecord(info.ID);
                    infos.Remove(info.Subdomain);
                    grid.Rows.Remove(row);
                }
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            using var dlg = new DlgConfig();
            if (dlg.Show(config) == DialogResult.OK)
            {
                config = dlg.Config;
                saveSettings();
                remakeAli();
                refresh();
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) >= Convert.ToDateTime("2021-05-01")  )
            {
                textBox1.Text = "更新地址 https://github.com/yoursoftder/-down";
                MessageBox.Show($"{this.Text}\r\n\r\n打勾代表解析到本地\r\n联系作者：t.me/almightsoft", "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{this.Text}\r\n\r\n打勾代表解析到本地\r\n", "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !exit;
            this.Hide();
        }

        bool exit = false;
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "确认要退出吗？\r\n\r\n* 退出后域名不再动态解析。",
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                exit = true;
                this.Close();
            }
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void severToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Server S = new Server();
            S.Show();
            //Hide();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            ali.SetupRecord(comboBox1.Text, comboBox2.Text, comboBox3.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Head = comboBox4.Text  , RecordId = comboBox5.Text, ip = comboBox6.Text;
        textBox1.AppendText(ali. UpdateIP(Head, RecordId, ip));
            //DescribeDomainRecords_Record
            //    ali. UpdateRecordIP(record,   ip);
            //textBox1.Text=   AliDDnsUtils.GetRecordId("123111.gjpsoft.cn");
            //  ali.UpdateIP("123111","192.168.1.1");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            ali.SetupRecord(comboBox1.Text, comboBox2.Text, comboBox3.Text);
        }
    
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Interval = Interval2;
                TextBox.CheckForIllegalCrossThreadCalls = false;
                // tr.Start();
                new Thread(refreshProc) { IsBackground = true }.Start();
            }
            catch { textBox1.AppendText("正在刷新...\r\n"); }
           
            //new Thread(refreshProc) { IsBackground = true }.Start();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Interval = 9999;
                
               // tr.Abort();
            }
            catch { textBox1.AppendText("正在停止...\r\n"); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 30000)
                textBox1.Text = "自动清理日志";
        }
    }


}
