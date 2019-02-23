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
    public sealed partial class DriverRegistration : Page
    {
        Database db;
        public DriverRegistration()
        {
            this.InitializeComponent();
            db = new Database();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // var secretque = listBox.SelectedItem;
            // selectedQueTxtBx.Text = secretque.ToString();
            var selected = listBox.Items.Cast<ListBoxItem>().Where(p => p.IsSelected).Select(t => t.Content.ToString()).ToArray();
            selectedQueTxtBx.Text = string.Join(", ", selected);
        }

        private async void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog showDialog = new MessageDialog("");

            if ((firstNameTxtBx.Text.ToString() == ""))
                showDialog.Content = "Enter Your First Name";

            else if (lastName.Text == "")
                showDialog.Content = "Enter Your Last Name";
            else if (userName.Text == "")
                showDialog.Content = "Enter Your User Name";
            else if ((userName.Text.ToString().Length < 6) || (userName.Text.ToString().Length > 15))
                showDialog.Content = "Your User Name should be between 6 and 15 characters";

            else if ((dob.Date == null))
                showDialog.Content = "Enter Your Date of Birth";

            else if ((maleRadioButton.IsChecked == false) && (femaleRadioButton.IsChecked == false))
                showDialog.Content = "Select Your Gender";

            else if ((mobileNumberTxtBx.Text == ""))
                showDialog.Content = "Enter Your Mobile Number";
            else if ((mobileNumberTxtBx.Text.ToString().Length != 10))
                showDialog.Content = "Your Mobile Number should consist 10 digits";
            else if (((Convert.ToUInt64(mobileNumberTxtBx.Text) <= 1000000000) && (Convert.ToUInt64(mobileNumberTxtBx.Text) >= 9999999999)))
                showDialog.Content = "Your Mobile Number consists 10 digits";
            else if (emailTxtBx.Text == "")
                showDialog.Content = "Enter Your Email ID";
            else if (passwordBox.Password.ToString() != verifyPasswordBox.Password.ToString())
                showDialog.Content = "Password Mismatch";
            else if (secretAns.Text == "")
                showDialog.Content = "Enter Your Secret Answer";
            else
            {
                if (maleRadioButton.IsChecked == true)
                {
                    genderTxtBlock.Text = "Male";
                }
                else
                {
                    genderTxtBlock.Text = "Female";
                }

                int code = db.DriRegister(new Common.DriverTable()
                {
                    DUserName = userName.Text.Trim(),
                    DFirstName = firstNameTxtBx.Text.Trim(),
                    DLastName = lastName.Text.Trim(),

                    DDOB = dob.Date.Date.ToString().Trim(),
                    DPassWord = passwordBox.Password.Trim(),
                    DEmailId = emailTxtBx.Text.Trim(),
                    DMobileNo = mobileNumberTxtBx.Text.Trim(),
                    DSecretAns = secretAns.Text.Trim(),
                    DSelectedQue = selectedQueTxtBx.Text.Trim(),
                    DGender = genderTxtBlock.Text.Trim(),
                    DAvailabilityFlag = "Aa".Trim(),
                    //DRating = 3
                    
                });

                if (code == -1)
                {
                    showDialog.Content = "Driver User Name Already Taken";

                }
                else
                {
                    showDialog.Content = "Driver Sign Up Succeeded!";

                }

            }
            await showDialog.ShowAsync();
        }

    }
}