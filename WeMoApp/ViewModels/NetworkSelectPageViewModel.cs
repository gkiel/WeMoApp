using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Rssdp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WeMoApp.Models;
using WeMoApp.Utilities;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace WeMoApp.ViewModels
{
    class NetworkSelectPageViewModel : ViewModelBase
    {
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");

            }
        }

        private SsdpRootDevice _selectedDevice;
        public SsdpRootDevice SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                Set(ref _selectedDevice, value);
            }
        }

        private ObservableCollection<AccessPoint> _accessPoints;
        public ObservableCollection<AccessPoint> AccessPoints
        {
            get
            {
                return _accessPoints;
            }
            set
            {
                Set(ref _accessPoints, value);
            }
        }

        public RelayCommand<AccessPoint> NavigateCommand { get; private set; }
        public RelayCommand RefreshApListCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private Service wifiSetupService;

        public NetworkSelectPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new RelayCommand<AccessPoint>(NavigateCommandAction);
            RefreshApListCommand = new RelayCommand(GetApList);
            AccessPoints = new ObservableCollection<AccessPoint>();
        }

        async public void Activate(NavigationEventArgs e)
        {
            SelectedDevice = e.Parameter as SsdpRootDevice;

            HttpClient client = new HttpClient();
            string deviceSetupXml = await client.GetStringAsync(SelectedDevice.Location);
            XmlSerializer serializer = new XmlSerializer(typeof(DeviceSetup));
            DeviceSetup deviceSetup = (DeviceSetup)serializer.Deserialize(new StringReader(deviceSetupXml));
            wifiSetupService = null;
            foreach (Service service in deviceSetup.Device.ServiceList.Service)
            {
                if (service.ServiceId == "urn:Belkin:serviceId:WiFiSetup1")
                {
                    wifiSetupService = service;
                    break;
                }
            }

            GetApList();
        }

        private void NavigateCommandAction(AccessPoint accessPoint)
        {
            if (accessPoint == null) return;

            _navigationService.NavigateTo(ViewModelLocator.AccessPointConnectPageKey, new Tuple<AccessPoint, Service, SsdpRootDevice>(accessPoint, wifiSetupService, SelectedDevice));
        }

        async private void GetApList()
        {
            string hostname = SelectedDevice.Location.Host;
            int port = SelectedDevice.Location.Port;
            HttpResponseMessage response = await SoapRequestHelper.MakeRequest(hostname, port, "GetApList", wifiSetupService.ServiceType, wifiSetupService.ControlURL);
            string responseText = await response.Content.ReadAsStringAsync();
            int indexOfApListStart = responseText.IndexOf("<ApList>");
            int indexOfApListEnd = responseText.IndexOf("</ApList>");
            AccessPointParser app = new AccessPointParser(responseText.Substring(indexOfApListStart + 8, indexOfApListEnd - (indexOfApListStart + 8)));
            IEnumerable<AccessPoint> aps = app.ParseAccessPoints();
            AccessPoints = new ObservableCollection<AccessPoint>(aps);
        }
    }
}
