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
    public sealed partial class UserLogin : Page
    {
        Database db;
        string DriverName;
        string dristr = "";
        string path;
        SQLite.Net.SQLiteConnection conn;
        public string usr;
        string str;

        public UserLogin()
        {
            this.InitializeComponent();
            db = new Database();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDataBase.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

        }

        private async void forgotPswdBtn_Click(object sender, RoutedEventArgs e)
        {
            
            var query2 = conn.Table<UserTable>().
                   Where(t => (t.UserName == txtBxUserNameLoginPage.Text));
            foreach (var ite in query2)
            {
                secretQueTextBlock.Text = ite.SelectedQue;
            }
            var query3 = conn.Table<UserTable>().
                  Where(t => (t.UserName == txtBxUserNameLoginPage.Text));
            foreach (var ite in query3)
            {
                // secretQueTextBox.Text = ite.SecretAns;
                str = ite.SecretAns;
            }
            secretQueTextBlock.Visibility = Visibility.Visible;
            secretQueTextBox.Visibility = Visibility.Visible;
            forgotPswdBtn.Content = "Login Through Security Answer ";
            if(secretQueTextBox.Password == str)
            {
                var message = new MessageDialog("Login Success");
                await message.ShowAsync();
                this.Frame.Navigate(typeof(MainPage1),txtBxUserNameLoginPage.Text);
            }
            else 
            {
                if (txtBxUserNameLoginPage.Text != "")
                {
                    MessageDialog md = new MessageDialog(" Invalid Security Answer! Please Contact Customer Care @ 8121294822");
                    await md.ShowAsync();
                }
            }
           // secretQueTextBlock.Text;
            
         }

        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserSignUpPage));
        }

        private async void userLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(db.Login(txtBxUserNameLoginPage.Text,txtBxPasswordLoginPage.Password.ToString()))
            {
                var message = new MessageDialog("Login Success");
                await message.ShowAsync();
                this.Frame.Navigate(typeof(MainPage1), txtBxUserNameLoginPage.Text);
            }
            else
            {
                var message = new MessageDialog("UserName or Password Incorrect");
                await message.ShowAsync();
            }
        }

        
    }
}
