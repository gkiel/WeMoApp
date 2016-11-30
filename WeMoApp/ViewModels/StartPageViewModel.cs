using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Rssdp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeMoApp.ViewModels
{
    class StartPageViewModel : ViewModelBase
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
        private string _title;
        public string Title
        {

            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        private ObservableCollection<SsdpDevice> _devices;
        public ObservableCollection<SsdpDevice> Devices
        {
            get { return _devices; }
            set { Set(ref _devices, value); }
        }

        public RelayCommand<SsdpDevice> NavigateCommand { get; private set; }

        private readonly INavigationService _navigationService;

        public StartPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Hello Joel";
            NavigateCommand = new RelayCommand<SsdpDevice>(NavigateCommandAction);
            Devices = new ObservableCollection<SsdpDevice>();
            SearchForDevices();
        }

        private void NavigateCommandAction(SsdpDevice device)
        {
            if (device == null) return;

            _navigationService.NavigateTo(ViewModelLocator.NetworkSelectPageKey, device);
        }

        public async void SearchForDevices()
        {
            using (var deviceLocator = new SsdpDeviceLocator())
            {
                var foundDevices = await deviceLocator.SearchAsync("urn:Belkin:device:controllee:1");
                foreach(var foundDevice in foundDevices)
                {
                    var fullDevice = await foundDevice.GetDeviceInfo();
                    _devices.Add(fullDevice);
                }
            }
        }
    }
}
