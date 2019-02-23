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
    public sealed partial class AdminLogin : Page
    {
        Database db;
        public AdminLogin()
        {
            this.InitializeComponent();
            db = new Database();
        }

        private void forgotPswdBtn_Click(object sender, RoutedEventArgs e)
        {
            //secretQueTextBlock.Visibility = Visibility.Visible;
            //secretQueTextBox.Visibility = Visibility.Visible;

        }

        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserSignUpPage));
        }

        private async void userLoginBtn_Click(object sender, RoutedEventArgs e)
        {
           /* if (db.Login(txtBxUserNameLoginPage.Text, txtBxPasswordLoginPage.Text))
            {
                var message = new MessageDialog("Login Success");
                await message.ShowAsync();
                this.Frame.Navigate(typeof(MainPage1), txtBxUserNameLoginPage.Text);
            } */

            if((txtBxUserNameLoginPage.Text == "Visali") && (passwordBox.Password == "C@bM@n1"))
            {
                this.Frame.Navigate(typeof(AdminPage));
            }
            else
            {
                var message = new MessageDialog("UserName or Password Incorrect, Please Contact Support Team @8341349996!");
                await message.ShowAsync();
            }
        }
    }
}
