using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Rssdp;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeMoApp.Models;
using WeMoApp.Utilities;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace WeMoApp.ViewModels
{
    class AccessPointConnectPageViewModel : ViewModelBase
    {
        private AccessPoint _accessPoint;
        public AccessPoint AccessPoint
        {
            get
            {
                return _accessPoint;
            }
            set
            {
                Set(ref _accessPoint, value);
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                Set(ref _password, value);
            }
        }

        private string _error;
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                Set(ref _error, value);
            }
        }

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                Set(ref _status, value);
            }
        }

        private string _configureState;
        public string ConfigureState
        {
            get
            {
                return _configureState;
            }
            set
            {
                Set(ref _configureState, value);
            }
        }

        private Service wifiSetupService;
        private SsdpRootDevice selectedDevice;

        public RelayCommand ConnectCommand { get; private set; }

        public AccessPointConnectPageViewModel(INavigationService navigationService)
        {
            ConnectCommand = new RelayCommand(ConnectToNetwork);
        }

        public void Activate(NavigationEventArgs e)
        {
            Tuple<AccessPoint, Service, SsdpRootDevice> args = e.Parameter as Tuple<AccessPoint, Service, SsdpRootDevice>;
            AccessPoint = args.Item1;
            wifiSetupService = args.Item2;
            selectedDevice = args.Item3;
        }

        async public void ConnectToNetwork()
        {
            string hostname = selectedDevice.Location.Host;
            int port = selectedDevice.Location.Port;

            HttpResponseMessage metaResponse = await SoapRequestHelper.MakeRequest(hostname, port,
                "GetMetaInfo", "urn:Belkin:service:metainfo:1", "/upnp/control/metainfo1");

            string metaInfoResponse = await metaResponse.Content.ReadAsStringAsync();
            int indexOfStartMetaInfo = metaInfoResponse.IndexOf("<MetaInfo>");
            int indexOfEndMetaInfo = metaInfoResponse.IndexOf("</MetaInfo>");
            string metainfo = metaInfoResponse.Substring(indexOfStartMetaInfo + 10, indexOfEndMetaInfo - (indexOfStartMetaInfo + 10));

            string encryptedPassword = EncryptPassword(Password, metainfo);
            encryptedPassword += encryptedPassword.Length.ToString("x") + (Password.Length < 16 ? "0" : "") + Password.Length.ToString("x");

            HttpResponseMessage response = await SoapRequestHelper.MakeRequest(hostname, port, "ConnectHomeNetwork", wifiSetupService.ServiceType, wifiSetupService.ControlURL,
                new Tuple<string, object>("auth", AccessPoint.Auth), new Tuple<string, object>("channel", AccessPoint.Channel),
                new Tuple<string, object>("encrypt", AccessPoint.Encrypt), new Tuple<string, object>("password", encryptedPassword),
                new Tuple<string, object>("ssid", AccessPoint.Ssid));
            if (response.IsSuccessStatusCode)
            {
                await Task.Delay(2000);

                HttpResponseMessage statusResponse = await SoapRequestHelper.MakeRequest(hostname, port, "GetNetworkStatus", wifiSetupService.ServiceType, wifiSetupService.ControlURL);
                Status = await statusResponse.Content.ReadAsStringAsync();
                if (statusResponse.IsSuccessStatusCode)
                {
                    HttpResponseMessage closeResponse = await SoapRequestHelper.MakeRequest(hostname, port, "CloseSetup", wifiSetupService.ServiceType, wifiSetupService.ControlURL);
                    if (closeResponse.IsSuccessStatusCode)
                    {
                        Error = "Success!";
                    }
                    else
                    {
                        Error = await response.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    Error = await statusResponse.Content.ReadAsStringAsync();
                }
                
            }
            else
            {
                Error = await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Borrowed from https://github.com/vadimkantorov/wemosetup/blob/master/wemosetup.py
        /// </summary>
        /// <param name="password"></param>
        /// <param name="metainfo"></param>
        /// <returns></returns>
        private string EncryptPassword(string password, string metainfo)
        {
            string[] metaInfoParts = metainfo.Split('|');
            string keydata = metaInfoParts[0].Substring(0, 6) + metaInfoParts[1] + metaInfoParts[0].Substring(6, 6);
            string salt = keydata.Substring(0, 8);
            string iv = keydata.Substring(0, 16);
            byte[] passwordAsBytes = Encoding.ASCII.GetBytes(Password);

            OpenSslPbeParametersGenerator keyGen = new OpenSslPbeParametersGenerator();
            keyGen.Init(Encoding.ASCII.GetBytes(keydata), Encoding.ASCII.GetBytes(salt));
            ICipherParameters cipherParams = keyGen.GenerateDerivedParameters("AES128", 128);

            AesEngine engine = new AesEngine();
            CbcBlockCipher blockCipher = new CbcBlockCipher(engine);
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding());
            ParametersWithIV keyParamWithIv = new ParametersWithIV(cipherParams, Encoding.ASCII.GetBytes(iv), 0, 16);

            cipher.Init(true, keyParamWithIv);
            byte[] outputBytes = new byte[cipher.GetOutputSize(passwordAsBytes.Length)];
            int length = cipher.ProcessBytes(passwordAsBytes, outputBytes, 0);
            cipher.DoFinal(outputBytes, length);
            return Convert.ToBase64String(outputBytes);
        }
    }
}
