using MapTest.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class BookDriver : Page
    {
        Database db;
        string DriverName;
        string path;
        SQLite.Net.SQLiteConnection conn;
        public string usr;
        public BookDriver()
        {
            this.InitializeComponent();
            db = new Database();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDataBase.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            
            //edit1

            var que = conn.Table<DriverTable>().
                Where(t => (t.DRating < 5 && t.DAvailabilityFlag == "Aa"));

            
            var query2 = conn.Table<DriverTable>().
                    Where(t => (t.DAvailabilityFlag == "Aa" && t.DRating == 5));

            foreach (var item2 in query2)
            { 
                
                listBox.Items.Add(item2.DUserName);
                // listBox.Items.Add(new SolidColorBrush(Color.FromArgb(255, 48, 179, 221)));
                            
            }
            foreach (var ite in que)
            {
                //edit1
                /* var query = conn.Table<DriverTable>().
                    Where(t => (t.DAvailabilityFlag == "Aa" && t.DRating == ite.DRating)); //t.DAvailabilityFlag == "Aa" &&

                foreach (var item in query)
                {*/
                listBox.Items.Add(ite.DUserName);
            //}
            }

        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             var driver = listBox.SelectedItem;
             selectedQueTxtBx.Text = driver.ToString();
            DriverName = selectedQueTxtBx.Text;

            var query1 = conn.Table<TransactionsTable>().Where(t => t.UserName == usr);
            int k = 0;
           
            foreach (var item1 in query1)
            {
                k = item1.ID;
            }

            // var query3 = conn.Table<CarTable>().Where(t => t.Availability == "A" && t.RegistrationNumber == j);
            
            var query2 = conn.Table<DriverTable>().Where(t => t.DUserName == DriverName);
            string j = "";
            foreach (var item4 in query2)
            {
                j = item4.DFirstName;
            }

            // Debug.WriteLine("tRANSACTIONStABLEcOUNT = " + k);
            conn.Execute("UPDATE TransactionsTable SET DriverID = ? Where ID = ?", DriverName.ToString(), k);
            conn.Execute("UPDATE TransactionsTable SET DriverName = ? Where ID = ?", j, k);
            //  conn.Execute("UPDATE TransactionsTable SET CabID = ? Where ID = ?", j, k);
            //edit2
            conn.Execute("UPDATE DriverTable SET DAvailabilityFlag= ? Where DUserName = ?", "Bb", DriverName.ToString());

            this.Frame.Navigate(typeof(PaymentPage), k);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            usr = e.Parameter.ToString();
        }


     }
}
