using MonolithUniversal.Models;
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

namespace MonolithUniversal.Tiles
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Slider : UserControl, ITile
    {
        private ISignal signal;

        public Slider()
        {
            this.InitializeComponent();
        }

        public string Identifier
        {
            get { return (string)GetValue(IdentifierProperty); }
            set { SetValue(IdentifierProperty, value); }
        }

        public static readonly DependencyProperty IdentifierProperty =
            DependencyProperty.Register("Identifier", typeof(string), typeof(Slider), new PropertyMetadata(""));

        public int State
        {
            get { return (int)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(int), typeof(Slider), new PropertyMetadata(0));

        public void setSignal(ISignal s)
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
