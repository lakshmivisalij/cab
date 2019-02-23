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
    public sealed partial class DriverActivitiesPage : Page
    {
        Database db;
        string DriverName;
        string dristr;
        string path;
        SQLite.Net.SQLiteConnection conn;
        public string usr;
        string cab;
        public DriverActivitiesPage()
        {
            this.InitializeComponent();
            db = new Database();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDataBase.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            Debug.WriteLine(dristr);

           
        }


           
            
        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            dristr = e.Parameter.ToString();

            Debug.WriteLine("string is"+dristr);
            var query2 = conn.Table<TransactionsTable>().Where(t => ((t.DriverId == dristr)));
            if(query2.Count() == 0)
            {
                MessageDialog md = new MessageDialog("");
                md.Content = "You haven't taken any rides yet!";
                await md.ShowAsync();
            }
            Debug.WriteLine("Number of Queries"+query2.ToString());
            foreach (var item2 in query2)
            {

                string str = item2.CabID + "    " + item2.CarType + "    " + item2.Date + "    " + item2.Source + "    " + item2.Destination + "    " + item2.Fare;

                listViewDriver.Items.Add(str);

            }

        }

        private void buttonBackToPanel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
        }

        private async void buttonAvailable_Click(object sender, RoutedEventArgs e)
        {
            //var query2 = conn.Table<DriverTable>().Where(t => t.DUserName == dristr);
            var query3 = conn.Table<TransactionsTable>().Where(t => t.DriverId == dristr);
            foreach (var ite in query3)
            {
                cab = ite.CabID;
                conn.Execute("UPDATE CarTable SET Availability= ? Where RegistrationNumber = ?", "A", cab);
            }
            conn.Execute("UPDATE DriverTable SET DAvailabilityFlag= ? Where DUserName = ?", "Aa", dristr);
            MessageDialog md1 = new MessageDialog("Made Available!");
            await md1.ShowAsync();
        }
       
    }
}
