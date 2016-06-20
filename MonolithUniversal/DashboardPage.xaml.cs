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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MonolithUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        private Models.Model model;

        private const int tileSize = 100;

        struct Configuration
        {
            public string Label { get; set; }
            public string Signal { get; set; }
            public string Type { get; set; }

            public int ColSpan { get; set; }
            public int RowSpan { get; set; }
        }

        private List<Configuration> configurations;

        private List<Type> tiles;

        public DashboardPage()
        {
            this.InitializeComponent();

            this.tiles = new List<Type>();
            this.configurations = new List<Configuration>();

            this.tiles.Add(typeof(Tiles.Toggle));
            this.tiles.Add(typeof(Tiles.Slider));

            this.configurations.Add(new Configuration { Label = "Label", Signal = "RadioFrequencyBridge.Toggle", Type = "Toggle", ColSpan = 2, RowSpan = 1 });
            this.configurations.Add(new Configuration { Label = "Label", Signal = "RadioFrequencyBridge.Slider", Type = "Slider", ColSpan = 2, RowSpan = 1 });

            this.Container.ItemHeight = tileSize;
            this.Container.ItemWidth = tileSize;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.model = (Models.Model)e.Parameter;

            foreach(Configuration config in this.configurations)
            {
                foreach (Models.ISignal signal in this.model.Signals)
                {
                    if(signal.Identifier == config.Signal)
                    {
                        foreach(Type type in this.tiles)
                        {
                            if(type.Name == config.Type)
                            {
                                object instance = Activator.CreateInstance(type);

                                Tiles.ITile tile = (Tiles.ITile)instance;
                                UIElement element = (UIElement)tile;

                                tile.setSignal(signal);

                                VariableSizedWrapGrid.SetColumnSpan(element, config.ColSpan);
                                VariableSizedWrapGrid.SetRowSpan(element, config.RowSpan);

                                this.Container.Children.Add(element);

                                break;
                            }
                        }

                        break;
                    }
                }
            }
        }


    }
}
