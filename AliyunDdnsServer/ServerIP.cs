using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LinkHome
{
    static class ServerIP
    {
        static Func<string>[] getIpFuncs = new Func<string>[] {
            getServerIPSohu,
            getServerIPIfconfig,
            getServerIPTrackip
        };
         

        public static string Get(ErrorHandler errorCallback = null)
        {
            var i = 0;
            while (true)
            {
                try
                {
                    return getIpFuncs[i]();
                }
                catch (Exception ex)
                {
                    errorCallback?.Invoke(ex.Message);
                    i++;
                    if (i == getIpFuncs.Length)
                    {
                        i = 0;
                    }
                    Thread.Sleep(100);
                }
            }
        }

        static string getResponse(string url)
        {
            var http = WebRequest.CreateHttp(url);
            using var response = http.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        static string getServerIPTrackip()
        {
            var json = getResponse(@"http://www.trackip.net/ip?json");
            var serializer = new JavaScriptSerializer();
            var data = (Dictionary<string, object>)serializer.DeserializeObject(json);
            return (string)data["IP"];
        }

        static string getServerIPIfconfig()
        {
            return getResponse(@"http://echo-ip.starworks.cc/");
        }

        static string getServerIPSohu()
        {
            var js = getResponse(@"http://pv.sohu.com/cityjson?ie=utf-8");
            var tokens = js.Split('\"');
            return tokens[3];
        }
    }
}
