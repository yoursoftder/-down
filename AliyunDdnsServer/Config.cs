using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkHome
{
    [Serializable]
    public class Config
    {
       
        public string AccessKeyID;
        public string AccessKeySecret;
        public static int TTL = 600;
        public static int Interval = 10;
        public bool HideForm = false;
    }
}
