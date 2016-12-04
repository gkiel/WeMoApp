using Rssdp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace WeMoApp.ViewModels
{
    public sealed class SsdpDeviceClickEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = value as ItemClickEventArgs;
            if (args == null)
                throw new ArgumentException("Value is not ItemClickEventArgs");
            if (args.ClickedItem is SsdpRootDevice)
            {
                var selectedItem = args.ClickedItem as SsdpRootDevice;
                return selectedItem;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
