using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Aliyun.Acs.Alidns.Model.V20150109;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using LinkHome;
using static Aliyun.Acs.Alidns.Model.V20150109.DescribeDomainRecordsResponse;
using static Aliyun.Acs.Alidns.Model.V20150109.DescribeDomainsResponse;

namespace LinkHome
{
    public class apiController : ApiController
    {
        public static string appPath = AppDomain.CurrentDomain.BaseDirectory;
        // GET api/<controller>
        //   public IEnumerable<string> Get()
      //  AliDDnsUtils ali = new AliDDnsUtils();
        public   string Get([FromBody] string json)

        {
            try
            {
                string url = Request.RequestUri.ToString();// url full
                url = url.Split('?')[1];
                url = DnsHelp.MD5(url, "");


                //   ali.SetupRecord("域名", "头", "值");
                // ali.UpdateIP("Head", "RecordId", "ip");

                // ?0=头 ?1=外网Ip ?2=域名 ?3=key或者keyreg
                string[] url2 = url.Split('?');//解密后的请求参数
                string fulldomain = url2[0] + "." + url2[2];
                string reid = "";
                if ((url2[1].Contains("(50") || url2[1].Contains("(错误")))
                { return DnsHelp.MD5en("说明?Ip正在获取中?offlineNoIp", "");  }
                try
                {
                    if (url2.Length >= 4 && url2[3].Contains("key") && url2[0].Length > 5)
                    { //长度合理 标识正确
                        bool same = true;


                        // string[] content = System.IO.File.ReadAllText(appPath + "\\_DomainLog.txt").Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);//准备读取文件
                        int lineNumber = 0;
                        string strfromtxt = File.ReadAllText(appPath + "\\_DomainLog.txt", Encoding.GetEncoding("GB2312"));
                        string[] lines = System.IO.File.ReadAllLines(appPath + "\\_DomainLog.txt");

                        if (url2[3] == "keyreg")
                        {
                            if (strfromtxt.Contains(fulldomain))
                            {
                                string msgback = DnsHelp.MD5en("提示:域名已注册,请换一个重新注册", "");
                                return msgback;

                            }
                            AliDDnsUtils Ali = new AliDDnsUtils();
                            Ali.SetupRecord(url2[2], url2[0], url2[1]);

                            DnsHelp.DomainLog("writeLog " + fulldomain + "注册成功 解析值 " + url2[1]);
                            if (Ali.zcmsg == "")
                            {
                                return DnsHelp.MD5en("regok?注册成功?.", "");
                            }
                            else
                            {
                                return DnsHelp.MD5en("regng?"+ Ali.zcmsg+"?.", "");
                            }
                        }

                        // string[] content = System.IO.File.ReadAllText(appPath + "\\_DomainLog.txt").Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);//准备读取文件
                        if (url2[3] == "keyupdate")
                        {
                            if (strfromtxt.Contains(fulldomain))
                            {
                                for (int i = 0; i < lines.Length; i++)
                                    if (lines[i].Contains(fulldomain))
                                    {
                                        lineNumber = i;//定位到包含域名fulldomain的行

                                        break;
                                    }
                            }
                            if (lineNumber > 0)//离线记录有
                            {
                                string s = lines[lineNumber];
                                string[] ss = s.Split('*');//离线记录的ip和解析数据
                                reid = ss[1];
                                AliDDnsUtils Ali = new AliDDnsUtils();
                               //   reid需要用带参数的方法遍历文本获取
                                if (ss[2] != url2[1])
                                { Ali.UpdateIP(url2[0], reid, url2[1]);// 更新解析值
                                    DnsHelp.DomainLog("writeLog " + fulldomain + "解析修改成功 解析值: " + url2[1] + "域名唯一Id: " + reid);
                                    System.Threading.Thread.Sleep(200);                                     
                                    return DnsHelp.MD5en("说明?解析值同步成功同步到IP" + url2[1] + "?jxxyok", "");
                                }
                                else
                                {
                                    DnsHelp.DomainLog("writeLog " + fulldomain + "解析未变无需修改 解析值: " + url2[1] + "请求值: " + ss[2]);
                                    return DnsHelp.MD5en("说明?解析值效验成功?jxxyok", "");
                                }
                            }
                            else//离线记录没有
                            {
                                return "wait?服务器繁忙正在刷新中?offlineNovalue?";
                            }
                        }
                        else { return DnsHelp.MD5en("提示:域名未注册,请重新注册", ""); }
                    }
                    else
                    { //长度不合理 url非法

                        return DnsHelp.MD5en("提示?前缀长度需要大于等于6个字?zcfailed2", "");
                    }
                }
                catch (Exception ee) { return "err..." + ee.Message.ToString(); }
             //   string url3 = url2[1] + "?问号测试?测试2";
               // string T = DnsHelp.MD5en(url3, "");

                return "0?0";
            }
            catch (Exception eee) {
                DnsHelp.DomainLog("writeLog " + "出错" + eee.Message.ToString()); ;
                return eee.ToString(); }
        }
    }
        class DnsHelp
    {
        
        internal static string MD5(string CDKEY, string sKey)
        {
            if (string.IsNullOrEmpty(sKey))
            {
                sKey = "12345678";
            }
            demd5 demd5 = new demd5();
            string result = string.Empty;
            try
            {
                byte[] array = new byte[CDKEY.Length / 2];
                for (int i = 0; i < CDKEY.Length / 2; i++)
                {
                    int num = Convert.ToInt32(CDKEY.Substring(i * 2, 2), 16);
                    array[i] = (byte)num;
                }
                demd5.Key = Encoding.ASCII.GetBytes(sKey);
                demd5.IV = Encoding.ASCII.GetBytes(sKey);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, demd5.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(array, 0, array.Length);
                cryptoStream.FlushFinalBlock();
                new StringBuilder();
                result = Encoding.Default.GetString(memoryStream.ToArray());
            }
            catch (Exception)
            {
                return CDKEY;
            }
            return result;
        }
        internal static string MD5en(string CDKEY, string sKey)
        {
            if (string.IsNullOrEmpty(sKey))
            {
                sKey = "12345678";
            }
            demd5 demd5 = new demd5();
            byte[] bytes = Encoding.Default.GetBytes(CDKEY);
            demd5.Key = Encoding.ASCII.GetBytes(sKey);
            demd5.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, demd5.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in memoryStream.ToArray())
            {
                stringBuilder.AppendFormat("{0:X1}", b);
            }
            stringBuilder.ToString();
            return stringBuilder.ToString();
        }
     public static string appPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string DomainLog(string msg)
        {
            if (msg.Contains("writeLog"))
            {
                
                 
                   
                    string ServerPath = appPath + "\\_DomainLog" + DateTime.Now.ToString("yyyy-MM") + ".log";
                 
                StreamWriter srWriter = new StreamWriter(ServerPath, true);
                    string WriteStr = "*\r\n";
                    WriteStr += "Date:" + DateTime.Now.ToString() + "  \r\n" + ""+ msg;
                    srWriter.WriteLine(WriteStr);
                srWriter.Close();
                return "Wok";//日志写入成功
               
            }
            try
            {
              
                if (!File.Exists(appPath + "\\_DomainLog.txt"))//不存在则创建文件
                {
                    // File.Create(Application.StartupPath.ToString() + "/app.set").Close();
                    StreamWriter sw = File.AppendText(appPath + "\\_DomainLog.txt");//准备写入文件
                    sw.WriteLine("解析记录信息用于离线读取");//写入文件开始0
                    sw.Flush();
                    sw.Close();//写入文件结束
                }
                    string j = "";
                 
                string[] msg2 = msg.Split('*');
                string[] lines = System.IO.File.ReadAllLines(appPath + "\\_DomainLog.txt");
                if (lines.Length == 0)
                {
                    StreamWriter sw = File.AppendText(appPath + "\\_DomainLog.txt");//准备写入文件
                    sw.WriteLine("解析记录信息用于离线读取");//写入文件开始0
                    sw.Flush();
                    sw.Close();//写入文件结束
                }
                string find = msg2[1];
                int lineNumber = 0;
                string strfromtxt = File.ReadAllText(appPath + "\\_DomainLog.txt", Encoding.GetEncoding("GB2312"));
                if (strfromtxt.Contains(find))
                {
                    for (int i = 0; i < lines.Length; i++)
                        if (lines[i].Contains(find))
                        {
                            lineNumber = i + 1;
                            string[] content = System.IO.File.ReadAllText(appPath + "\\_DomainLog.txt").Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);//准备读取文件


                            content[lineNumber - 1] = "" + msg + "*" + DateTime.Now.ToString(); //写唯一码
                            System.IO.File.WriteAllText(appPath + "\\_DomainLog.txt", string.Join("\r\n", content));//将内容换行保存在本地TXT中开始
                            j = msg2[3] + "存在,已经更新解析值\r\n";
                            break;
                        }
                }

                else
                {

                    string ServerPath = appPath + "\\_DomainLog.txt";

                    StreamWriter srWriter = new StreamWriter(ServerPath, true);
                    string WriteStr = "\r\n";
                    WriteStr += msg + "*" + DateTime.Now.ToString() + "";
                    srWriter.WriteLine(WriteStr);
                    srWriter.Close();
                    j = "新增记录" + msg2[3] + "成功\r\n";
                }
                System.Threading.Thread.Sleep(100);
                return j;
            }
            catch(Exception ee) { return ee.Message.ToString(); }
        }
        
      
        
       
    }
}
