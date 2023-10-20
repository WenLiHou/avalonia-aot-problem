using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace AvaloniaApplication1.Localization
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        public static LocalizationResourceManager Instance { get; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public LocalizationResourceManager()
        {
        }

        private List<CultureInfo> _cultureInfos = new List<CultureInfo>
        {
            CultureInfo.GetCultureInfo("zh-cn"),
            CultureInfo.GetCultureInfo("en-us")
        };

        public IList<CultureInfo> AvailableCultures
        {
            get => _cultureInfos;
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public object this[string resourceKey] => Strings.ResourceManager.GetObject(resourceKey) ?? Array.Empty<byte>();

        public CultureInfo CurrentCulture
        {
            get
            {
                _currentCulture ??= AvailableCultures.First();
                return _currentCulture;
            }
            set
            {
                if (value != null && _currentCulture != value && AvailableCultures.Contains(_currentCulture))
                {
                    _currentCulture = value;

                    SetCulture(value);
                }
            }
        }
        private CultureInfo? _currentCulture;


        private void SetCulture(CultureInfo culture)
        {
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = culture;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
