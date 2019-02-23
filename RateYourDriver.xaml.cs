using MapTest.Common;
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
    public sealed partial class RateYourDriver : Page
    {
        Database db;

        string path;
        SQLite.Net.SQLiteConnection conn;
        public int k1;
        public string k1str;
        public RateYourDriver()
        {
            this.InitializeComponent();
            db = new Database();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDataBase.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
                      
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            k1str = e.Parameter.ToString();
            k1 = Convert.ToInt32(k1str);
        }

        private async void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            int rating = 1;
            if (radioButton.IsChecked == true)
            {
                rating = 1;
            }
            var query1 = conn.Table<TransactionsTable>().Where(t => t.ID == k1);
            int k = 0;
            string st = "";
            string st2 = "";
            foreach (var item1 in query1)
            {
                k = item1.ID;
                st = item1.DriverId;
                st2 = item1.CabID;

            }

            conn.Execute("UPDATE TransactionsTable SET DriverRating = ? Where ID = ?", rating, k);
            conn.Execute("UPDATE DriverTable SET DRating = ? Where DUserName = ?", 1, st);
            conn.Execute("UPDATE DriverTable SET DAvailabilityFlag = ? Where DUserName = ?", "Aa", st);
            conn.Execute("UPDATE CarTable SET Availability = ? Where RegistrationNumber = ?", "A", st2);
            var message = new MessageDialog("Driver Rated!");
            await message.ShowAsync();

            this.Frame.Navigate(typeof(MainPage1), k);

        }

        private async void radioButton_Copy2_Checked(object sender, RoutedEventArgs e)
        {
            int rating = 2;
            if (radioButton_Copy2.IsChecked == true)
            {
                rating = 2;
            }
            var query1 = conn.Table<TransactionsTable>().Where(t => t.ID == k1);
            int k = 0;
            string st = "";
            string st2 = "";

            foreach (var item1 in query1)
            {
                k = item1.ID;
                st = item1.DriverId;
                st2 = item1.CabID;
            }

            conn.Execute("UPDATE TransactionsTable SET DriverRating = ? Where ID = ?", rating, k);
            conn.Execute("UPDATE DriverTable SET DRating = ? Where DUserName = ?", 2, st);
            conn.Execute("UPDATE DriverTable SET DAvailabilityFlag = ? Where DUserName = ?", "Aa", st);
            conn.Execute("UPDATE CarTable SET Availability = ? Where RegistrationNumber = ?", "A", st2);
            var message = new MessageDialog("Driver Rated!");
            await message.ShowAsync();

            this.Frame.Navigate(typeof(MainPage1), k);
        }

        private async void radioButton_Copy3_Checked(object sender, RoutedEventArgs e)
        {
            int rating = 3;
            if (radioButton_Copy3.IsChecked == true)
            {
                rating = 3;
            }
            var query1 = conn.Table<TransactionsTable>().Where(t => t.ID == k1);
            int k = 0;
            string st = "";
            string st2 = "";

            foreach (var item1 in query1)
            {
                k = item1.ID;
                st = item1.DriverId;
                st2 = item1.CabID;
            }

            conn.Execute("UPDATE TransactionsTable SET DriverRating = ? Where ID = ?", rating, k);
            conn.Execute("UPDATE DriverTable SET DRating = ? Where DUserName = ?", 3, st);
            conn.Execute("UPDATE DriverTable SET DAvailabilityFlag = ? Where DUserName = ?", "Aa", st);
            conn.Execute("UPDATE CarTable SET Availability = ? Where RegistrationNumber = ?", "A", st2);

            var message = new MessageDialog("Driver Rated!");
            await message.ShowAsync();

            this.Frame.Navigate(typeof(MainPage1), k);
        }

        private async void radioButton_Copy4_Checked(object sender, RoutedEventArgs e)
        {
            int rating = 4;
            if (radioButton_Copy4.IsChecked == true)
            {
                rating = 4;
            }
            var query1 = conn.Table<TransactionsTable>().Where(t => t.ID == k1);
            int k = 0;
            string st = "";
            string st2 = "";
            foreach (var item1 in query1)
            {
                k = item1.ID;
                st = item1.DriverId;
                st2 = item1.CabID;
            }

             conn.Execute("UPDATE TransactionsTable SET DriverRating = ? Where ID = ?", rating, k);
             conn.Execute("UPDATE DriverTable SET DRating = ? Where DUserName = ?", 4, st);

            conn.Execute("UPDATE DriverTable SET DAvailabilityFlag = ? Where DUserName = ?", "Aa", st);
            conn.Execute("UPDATE CarTable SET Availability = ? Where RegistrationNumber = ?", "A", st2);

            var message = new MessageDialog("Driver Rated!");
            await message.ShowAsync();

            this.Frame.Navigate(typeof(MainPage1), k);
        }

        private async void radioButton_Copy5_Checked(object sender, RoutedEventArgs e)
        {
            int rating = 5;
            if (radioButton_Copy5.IsChecked == true)
            {
                rating = 5;
            }
            var query1 = conn.Table<TransactionsTable>().Where(t => t.ID == k1);
            int k = 0;
            string st = "";
            string st2 = "";
            foreach (var item1 in query1)
            {
                k = item1.ID;
                st = item1.DriverId;
                st2 = item1.CabID;
            }

             conn.Execute("UPDATE TransactionsTable SET DriverRating = ? Where ID = ?", rating, k);
            //edit1
            conn.Execute("UPDATE DriverTable SET DRating = ? Where DUserName = ?", 5, st);
            conn.Execute("UPDATE DriverTable SET DAvailabilityFlag = ? Where DUserName = ?", "Aa", st);
            conn.Execute("UPDATE CarTable SET Availability = ? Where RegistrationNumber = ?", "A", st2);

            var message = new MessageDialog("Driver Rated!");
            await message.ShowAsync();

            this.Frame.Navigate(typeof(MainPage1), k);

        } 

     /*   private async void  radioButton_Copy5_Click(object sender, RoutedEventArgs e)
        {
            int rating = 5;
            if (radioButton_Copy5.IsChecked == true)
            {
                rating = 5;
            }
            var query1 = conn.Table<TransactionsTable>().Where(t => t.ID == k1);
            int k = 0;
            string st = "";
            string st2 = "";
            foreach (var item1 in query1)
            {
                k = item1.ID;
                st = item1.DriverId;
                st2 = item1.CabID;
            }

            conn.Execute("UPDATE TransactionsTable SET DriverRating = ? Where ID = ?", rating, k);
            //edit1
            conn.Execute("UPDATE DriverTable SET DriverRating = ? Where ID = ?", rating, k);
            conn.Execute("UPDATE DriverTable SET DAvailabilityFlag = ? Where DUserName = ?", "Aa", st);
            conn.Execute("UPDATE CarTable SET Availability = ? Where RegistrationNumber = ?", "A", st2);

            var message = new MessageDialog("Driver Rated!");
            await message.ShowAsync();

            this.Frame.Navigate(typeof(MainPage1), k);


        }
*/    }
}
