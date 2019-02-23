using MapTest.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class DriverLogin : Page
    {
        string usrnm;
        Database db;
        public DriverLogin()
        {
            this.InitializeComponent();
            db = new Database();
        }

        private async void forgotPswdBtn_Click(object sender, RoutedEventArgs e)
        {
            //secretQueTextBlock.Visibility = Visibility.Visible;
            //secretQueTextBox.Visibility = Visibility.Visible;
            MessageDialog md = new MessageDialog("Please Contact Driver Support Care @ 011 - 88888888 ");
            await md.ShowAsync();

        }

        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DriverRegistration));
        }
        
        private async void userLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (db.DLogin(txtBxUserNameLoginPage.Text, txtBxPasswordLoginPage.Password.ToString()))
            {
                var message = new MessageDialog("Login Success");
                await message.ShowAsync();
                this.Frame.Navigate(typeof(DriverActivitiesPage), txtBxUserNameLoginPage.Text);
            }
            else
            {
                var message = new MessageDialog("UserName or Password Incorrect");
                await message.ShowAsync();
            }
        }
    }
}
