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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Obelisk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Models.Model model;
        private Providers.Provider control;

        public MainPage()
        {
            this.InitializeComponent();

            this.model = new Models.Model();
            this.control = new Providers.Provider(this.model);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.SplitViewContent.Navigate(typeof(DashboardPage), this.model);
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            this.SplitViewContent.Navigate(typeof(DashboardPage), this.model);
        }

        private void Plugins_Click(object sender, RoutedEventArgs e)
        {
            this.SplitViewContent.Navigate(typeof(PluginsPage), this.model);
        }

        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            this.SplitViewContent.Navigate(typeof(DevicesPage), this.model);
        }

        private void Bindings_Click(object sender, RoutedEventArgs e)
        {
            this.SplitViewContent.Navigate(typeof(BindingPage), this.model);
        }
    }
}
