﻿using System;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    class IsdoneToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var a = (int)value;
            var x = "";
            switch (a)
            {
                case 2:
                    {
                        x = "";
                        break;
                    }
                case 1:
                    {
                        x = "";
                        break;
                    }
                case 0:
                    {
                        x = "";
                        break;
                    }
                default:
                    break;
            }

            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
