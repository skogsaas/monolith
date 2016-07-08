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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Obelisk.Tiles
{
    public sealed partial class Toggle : UserControl, ITile
    {
        private Models.ISignal signal;

        public Toggle()
        {
            this.InitializeComponent();
        }

        public string Identifier
        {
            get { return (string)GetValue(IdentifierProperty); }
            set { SetValue(IdentifierProperty, value); }
        }

        public static readonly DependencyProperty IdentifierProperty =
            DependencyProperty.Register("Identifier", typeof(string), typeof(Toggle), new PropertyMetadata("Unknown"));

        public bool State
        {
            get { return (bool)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(bool), typeof(Toggle), new PropertyMetadata(false));

        public void setSignal(Models.ISignal s)
        {
            this.signal = s;

            this.Identifier = this.signal.Identifier;

            Binding state = new Binding();
            state.Source = this.signal;
            state.Path = new PropertyPath("State");
            state.Mode = BindingMode.TwoWay;
            state.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(this, StateProperty, state);
        }

    }
}
