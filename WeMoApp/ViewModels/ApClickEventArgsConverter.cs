﻿using System;
using WeMoApp.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace WeMoApp.ViewModels
{
    class ApClickEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = value as ItemClickEventArgs;
            if (args == null)
                throw new ArgumentException("Value is not ItemClickEventArgs");
            if (args.ClickedItem is AccessPoint)
            {
                var selectedItem = args.ClickedItem as AccessPoint;
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
