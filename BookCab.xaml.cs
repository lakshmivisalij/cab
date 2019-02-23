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
using System.Data.Common;
using System.Numerics;
using MapTest.Common;
using SQLite.Net;
using System.Diagnostics;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookCab : Page
    {
        Database db;
       
        string path;
        SQLite.Net.SQLiteConnection conn;
        public string usr;
        public BookCab()
        {
            this.InitializeComponent();
            db = new Database();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDataBase.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            //conn.CreateTable<Transaction>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) 
        {
            base.OnNavigatedTo(e);

            usr = e.Parameter.ToString();
            Debug.WriteLine(usr); 
            string di = (db.FindDistance(usr));
            
            decimal dist;
            dist = Convert.ToDecimal(di);
            decimal minicost, microcost, micro_non_ac_cost;
            minicost = Math.Ceiling(15 * dist);
            microcost = Math.Ceiling(21 * dist);
            micro_non_ac_cost = Math.Ceiling(10 * dist);
            txtblk_Mic_Non_Ac.Text = micro_non_ac_cost.ToString();
            txtblk_Mic.Text = microcost.ToString();
            txtblk_Mini.Text = minicost.ToString(); 
            
                      
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var query = conn.Table<TransactionsTable>();
            int k = 0;
           // if (query.Count() > 0)
            //    k = query.Count();
            foreach(var item1 in query)
            {
                k = item1.ID;
            }
            var query2 = conn.Table<CarTable>().Where(t => t.Availability == "A" && t.CarType == "Micro - NON - AC"); 
            string j = "";
             foreach(var item in query2)
                j = item.RegistrationNumber;
            var query3 = conn.Table<CarTable>().Where(t => t.Availability == "A" && t.RegistrationNumber == j);


            Debug.WriteLine("tRANSACTIONStABLEcOUNT = " + k);
            conn.Execute("UPDATE TransactionsTable SET CarType = ? Where ID = ?", "Micro - NON - AC", k);
            conn.Execute("UPDATE TransactionsTable SET Fare = ? Where ID = ?", txtblk_Mic_Non_Ac.Text, k);
            conn.Execute("UPDATE TransactionsTable SET CabID = ? Where ID = ?", j, k);
            conn.Execute("UPDATE CarTable SET Availability= ? Where RegistrationNumber = ?", "B", j);

            this.Frame.Navigate(typeof(BookDriver),usr);
        }

        private void btn_Mic_Click(object sender, RoutedEventArgs e)
        {
            var query = conn.Table<TransactionsTable>();
            int k = 0;
            if (query.Count() > 0)
                k = query.Count();
            var query2 = conn.Table<CarTable>().Where(t => t.Availability == "A" && t.CarType == "Micro - AC");
            string j = "";
            foreach (var item in query2)
                j = item.RegistrationNumber;

            Debug.WriteLine("tRANSACTIONStABLEcOUNT = " + k);
            conn.Execute("UPDATE TransactionsTable SET CarType = ? Where ID = ?", "Micro - AC", k);
            conn.Execute("UPDATE TransactionsTable SET Fare = ? Where ID = ?", txtblk_Mic.Text, k);
            conn.Execute("UPDATE TransactionsTable SET CabID = ? Where ID = ?", j, k);

            this.Frame.Navigate(typeof(BookDriver), usr);
        }

        private void btn_Mini_Click(object sender, RoutedEventArgs e)
        {
            //db.InsertCarDetailsMini(usr);
            // string fare = "";

            // var query = conn.Update(new Transaction() { Fare = txtblk_Mini.Text, CarType = "Mini" }
            //   where(t => t.UserName == usr));
            // var last = conn.Execute("select count(*) from TransactionsTable");
            var query = conn.Table<TransactionsTable>();
            int k=0;
            if (query.Count() > 0)
                 k = query.Count();
            Debug.WriteLine("tRANSACTIONStABLEcOUNT = " + k);
            conn.Execute("UPDATE TransactionsTable SET CarType = ? Where ID = ?", "Mini", k);
            conn.Execute("UPDATE TransactionsTable SET Fare = ? Where ID = ?", txtblk_Mini.Text, k);

            //  conn.Execute("UPDATE Transaction SET Fare = ? Where UserName = ?", "100", 'viksha');

            // var add = conn.Insert(new Message() { Name = textBox.Text });
            /* foreach (var item in query)
             {
                 item.Fare = txtblk_Mini.Text;
                 item.CarType = "Mini";
                 item.CabID = "AP Mini";
                     }*/
           /* path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDataBase.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<TransactionsTable>();*/

            /*using ( conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                var existingUser = conn.Query<TransactionsTable>("Select * from TransactionsTable where UserName = ?" + usr).Last();
                    if (existingUser != null)
                {
                    existingUser.Fare = txtblk_Mini.Text.Trim();
                    existingUser.CabID = "AP";
                    existingUser.CarType = "Mini";
                    
                    conn.RunInTransaction(() =>
                    {
                        conn.Update(existingUser);
                    });
                }
            }*/
            //var trans = await conn.Table<Transaction>().Where(t => t.UserName == usr).LastOrDefault();


            this.Frame.Navigate(typeof(BookDriver), usr);
        }
    }
}
