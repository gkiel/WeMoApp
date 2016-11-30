using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Rssdp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private SsdpDevice _selectedDevice;
        public SsdpDevice SelectedDevice
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

        private readonly INavigationService _navigationService;

        public NetworkSelectPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
