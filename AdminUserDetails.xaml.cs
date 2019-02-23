using MapTest.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class AdminUserDetails : Page
    {
        Database db;
        string DriverName;
        string dristr = "";
        string path;
        SQLite.Net.SQLiteConnection conn;
        public string usr;

        public AdminUserDetails()
        {
            this.InitializeComponent();
            db = new Database();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDataBase.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            //edit1
            //public List<User> users = new List<User>();

            var query2 = conn.Table<UserTable>().ToList();


            foreach (var item2 in query2)
            {

                string str = item2.UserName + "    " + item2.MobileNo + "    " + item2.EmailId + "    " + item2.SelectedQue + "    " + item2.SecretAns;


                listViewDriver.Items.Add(str);

                // listBox.Items.Add(new SolidColorBrush(Color.FromArgb(255, 48, 179, 221)));

                //ItemsSource = query2;

            }
        }

        public void listViewDriver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewDriver.Items.Count() != 0)
            {
                var driver = listViewDriver.SelectedItem;
                //string dristr = null;
                //errorprone - null reference - plesase select a driver from the list
                DriverName = driver.ToString();

                int i;
                for (i = 0; i < 6; i++)
                {
                    dristr = dristr + DriverName[i];
                }
                Debug.WriteLine(dristr);

            }
        }

        private async void SearchDriverAdminbtn_Click(object sender, RoutedEventArgs e)
        {
            var query2 = conn.Table<UserTable>().Where(t => ((t.FirstName == textBoxDriverSearch.Text) || (t.UserName == textBoxDriverSearch.Text)));
            //conn.Execute("UPDATE TransactionsTable SET DriverID = ? Where ID = ?", DriverName.ToString(), k);

            if (query2.ToList().Count() == 0 && textBoxDriverSearch.Text != "")
            {
                listViewDriver.Items.Clear();
            }

            if (query2.ToList().Count() != 0)
            {
                listViewDriver.Items.Clear();
            }

            if (textBoxDriverSearch.Text.ToString() == "")
            {
                MessageDialog showDialog = new MessageDialog("");
                showDialog.Content = "Please Enter User Name or First Name in order to Search";
                await showDialog.ShowAsync();
            }

            foreach (var item2 in query2)
            {

                string str = item2.UserName + "    " + item2.MobileNo + "    " + item2.EmailId + "    " + item2.SelectedQue + "    " + item2.SecretAns;


                listViewDriver.Items.Add(str);

            }
            if (textBoxDriverSearch.Text != "" && listViewDriver.Items.Count() == 0)
            {

                MessageDialog showDialog = new MessageDialog("");
                showDialog.Content = "User Not Listed! To add a new user go to User Registration Page ";
                await showDialog.ShowAsync();
            }
        }

        private async void deleteDriverAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dristr == "")
            {
                MessageDialog showDialog = new MessageDialog("");
                showDialog.Content = "Please Select the User to Delete.";
                await showDialog.ShowAsync();
            }

            var query = conn.Execute("DELETE From UserTable  Where UserName = ?", dristr);
            if (query != 0)
            {
                //Debug.WriteLine(query.ToString());
                MessageDialog showDialog1 = new MessageDialog("");
                showDialog1.Content = "User Details Deleted";
                await showDialog1.ShowAsync();
            }
            if (query == 0 && dristr != "")
            {
                MessageDialog showDialog2 = new MessageDialog("");
                showDialog2.Content = "User Not Existed";
                await showDialog2.ShowAsync();
            } 

        }
        private void buttonBackToPanel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
        }
    }
}
