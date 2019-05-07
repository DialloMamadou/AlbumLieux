using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AlbumLieux.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("YEoTt2onn0yuND8lkHIc~8nwMGvlWpcw0Hcg52hD-BA~AhdUmyF5szTXASXeARdbyEdHfozYQDrRXf689jWeW8oMMsPTBrcdQrMXF-cXxu-9");
            LoadApplication(new AlbumLieux.App());
        }
    }
}
