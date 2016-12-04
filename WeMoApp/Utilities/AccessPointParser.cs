using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeMoApp.Models;

namespace WeMoApp.Utilities
{
    class AccessPointParser
    {
        private readonly string accessPointOutput;

        public AccessPointParser(string accessPointOutput)
        {
            this.accessPointOutput = accessPointOutput;
        }

        public IEnumerable<AccessPoint> ParseAccessPoints()
        {
            int indexOfDollar = accessPointOutput.IndexOf('$');
            string accessPointsText = accessPointOutput.Substring(indexOfDollar + 1).Trim();
            if (accessPointsText.LastIndexOf(',') == accessPointsText.Length - 1)
            {
                accessPointsText = accessPointsText.Substring(0, accessPointsText.Length - 1);
            }
            string[] accessPointLines = accessPointsText.Split(',');
            foreach(string accessPointLine in accessPointLines)
            {
                string[] parts = accessPointLine.Trim().Split('|');
                string ssid = parts[0];
                int channel;
                if (!int.TryParse(parts[1], out channel))
                    continue;
                string[] authAndEncrypt = parts[3].Split('/');
                string auth = authAndEncrypt[0];
                string encrypt = authAndEncrypt.Length == 2 ? authAndEncrypt[1] : "";
                yield return new AccessPoint(ssid, auth, encrypt, channel);
            }
        }
    }
}
