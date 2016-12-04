using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeMoApp.Views;

namespace WeMoApp.ViewModels
{
    class ViewModelLocator
    {
        public const string NetworkSelectPageKey = "NetworkSelect";
        public const string AccessPointConnectPageKey = "AccessPoint";
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            else
            {
                // Create run time view services and models
            }

            var nav = new NavigationService();
            nav.Configure(NetworkSelectPageKey, typeof(NetworkSelectPage));
            nav.Configure(AccessPointConnectPageKey, typeof(AccessPointConnectPage));

            //Register your services used here
            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<StartPageViewModel>();
            SimpleIoc.Default.Register<NetworkSelectPageViewModel>();
            SimpleIoc.Default.Register<AccessPointConnectPageViewModel>();

        }


        // <summary>
        // Gets the StartPage view model.
        // </summary>
        // <value>
        // The StartPage view model.
        // </value>
        public StartPageViewModel StartPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartPageViewModel>();
            }
        }

        // <summary>
        // Gets the AccessPointConnectPageViewModel view model.
        // </summary>
        // <value>
        // The AccessPointConnectPageViewModel view model.
        // </value>
        public AccessPointConnectPageViewModel AccessPointConnectPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccessPointConnectPageViewModel>();
            }
        }

        // <summary>
        // Gets the NetworkSelectPage view model.
        // </summary>
        // <value>
        // The NetworkSelectPage view model.
        // </value>
        public NetworkSelectPageViewModel NetworkSelectPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NetworkSelectPageViewModel>();
            }
        }

        // <summary>
        // The cleanup.
        // </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
