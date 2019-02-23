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
using WinUX.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddCarDetails : Page
    {
        Database db;
        public AddCarDetails()
        {
            this.InitializeComponent();
            db = new Database();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // var secretque = listBox.SelectedItem;
            // selectedQueTxtBx.Text = secretque.ToString();
            var selected = listBox.Items.Cast<ListBoxItem>().Where(p => p.IsSelected).Select(t => t.Content.ToString()).ToArray();
            carTypeTxtBx.Text = string.Join(", ", selected);
        }

        private async void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog showDialog = new MessageDialog("");
                
            if (carName.Text == "")
                showDialog.Content = "Enter the Car Name";
            else if ((carName.Text.ToString().Length < 6) || (carName.Text.ToString().Length > 15))
                showDialog.Content = "Your Car Name should be between 6 and 15 characters";

            else if ((dom.Date == null))
                showDialog.Content = "Enter Your Date of Manufacturing";

            else if ((registrationNumberTxtBx.Text == ""))
                showDialog.Content = "Enter Car Registration Number";
           
            else if (companyNameTxtBx.Text == "")
                showDialog.Content = "Enter Car Manufacturing Comapany Name";
           
          
            else
            {
                int code = db.CarRegister(new Common.CarTable()
                {
                    CarCompanyName = companyNameTxtBx.Text.Trim(),
                    DOM = dom.Date.Date.ToString().Trim(),
                    RegistrationNumber = registrationNumberTxtBx.Text.Trim(),
                    CarName = carName.Text.Trim(),
                    CarType = carTypeTxtBx.Text.Trim(),
                    Availability = "A"

                });

                if (code == -1)
                {
                    showDialog.Content = "Car Already Added";

                }
                else
                {
                    showDialog.Content = "Car Add Up Succeeded!";

                }

            }
            await showDialog.ShowAsync();
        }

        private void buttonBackPanel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
        }
    }
}