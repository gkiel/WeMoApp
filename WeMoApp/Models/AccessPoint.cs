using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeMoApp.Models
{
    class AccessPoint
    {
        public string Ssid { get; set; }
        public string Auth { get; set; }
        public string Encrypt { get; set; }
        public int Channel { get; set; }

        public AccessPoint(string ssid, string auth, string encrypt, int channel)
        {
            Ssid = ssid;
            Auth = auth;
            Encrypt = encrypt;
            Channel = channel;
        }
    }
}
