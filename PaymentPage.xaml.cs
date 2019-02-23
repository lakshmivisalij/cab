using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaymentPage : Page
    {
        public int k;
        public string kstr;
        public PaymentPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            kstr = e.Parameter.ToString();
            k = Convert.ToInt32(kstr);
        }
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog md = new MessageDialog("");
            if(textBoxCardNum.Text.ToString().Length != 16)
            {
                md.Content= "Card Length Should be 16 Digits";
                await md.ShowAsync();

            }
            if (textBoxCVV.Password.ToString().Length != 3 && (textBoxCardNum.Text.ToString().Length == 16))
            {
                md.Content = "CVV Length Should be 3 Digits";
                await md.ShowAsync();

            }
            if (textBoxPin.Password.ToString().Length != 4 && (textBoxCVV.Password.ToString().Length == 3))
            {
                md.Content = "PIN Length Should be 4 Digits";
                await md.ShowAsync();

            }
            if((textBoxCardNum.Text.ToString().Length == 16)&& (textBoxCVV.Password.ToString().Length == 3) && (textBoxPin.Password.ToString().Length == 4))
            this.Frame.Navigate(typeof(RateYourDriver), k);
        }

    }
}
