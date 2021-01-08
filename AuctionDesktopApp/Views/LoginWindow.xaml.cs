using AuctionDesktopApp.Views;
using AuctionLibrary.Storage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AuctionDesktopApp
{
    public partial class LoginWindow : Window
    {
        private readonly AuctionDB _context;

        public LoginWindow()
        {
            InitializeComponent();
            _context = new AuctionDB();
        }

        //------------------------------ EVENTHANDLERS --------------------------------------------------------

        /*
         * Validates userinput and opens
         * a new MainWindow sending the
         * userinput as arguments if 
         * validation is succeeded.
         * If validation fails, errormessage(s)
         * are sent to the user at this window.
         */
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide errorlabels to reevaluate validation.
            HideErrorLabels();

            string name = TxtBoxUserName.Text;
            string number = TxtBoxUserNumber.Text;
            string email = TxtBoxUserEmail.Text;

            int errorCounter = 0;

            if (name == null || name == "Navn" || name == "" || !_context.VerifyName(name) || name.Length < 2 || name.Length > 40)
            {
                errorCounter++;
                if (name.Length < 2 || name.Length > 40)
                {
                    nameErrorLabel.Content = "Navnet skal indeholde 2 - 40 tegn.";
                }
                else if (name != "Navn" && name != "" && !_context.VerifyName(name))
                {
                    nameErrorLabel.Content = "Navnet er ugyldigt.";
                }
                else
                {
                    nameErrorLabel.Content = "Indtast venligst dit navn.";
                }
                nameErrorLabel.Visibility = Visibility.Visible;
            }

            if (number == null || number == "Telefonnummer" || number == "" || !_context.VerifyNumber(number) || number.Length < 8 || number.Length > 15)
            {
                errorCounter++;
                if (number.Length < 8 || number.Length > 15)
                {
                    numberErrorLabel.Content = "Telefonnummeret skal indeholde 8 - 15 tal.";
                }
                else if (number != "Telefonnummer" && number != "" && !_context.VerifyNumber(number))
                {
                    numberErrorLabel.Content = "Telefonnummeret er ugyldigt.";
                }
                else
                {
                    numberErrorLabel.Content = "Indtast venligst dit telefonnummer.";
                }
                numberErrorLabel.Visibility = Visibility.Visible;
            }

            if (email == null || email == "E-mail" || email == "" || !_context.VerifyEmail(email) || email.Length < 6)
            {
                errorCounter++;
                if(email.Length < 6)
                {
                    emailErrorLabel.Content = "E-mailadressen skal indeholde minimum 6 tegn.";
                }
                else if (email != "E-mail" && email != "" && !_context.VerifyEmail(email))
                {
                    emailErrorLabel.Content = "E-mailadressen er ugyldig.";
                }
                else
                {
                    emailErrorLabel.Content = "Indtast venligst din e-mailadresse.";
                }
                emailErrorLabel.Visibility = Visibility.Visible;
            }

            if (errorCounter == 0)
            {
                MainWindow mainWindow = new MainWindow(name, number, email);
                mainWindow.Show();
                this.Hide();
            }
        }

        /**
         * Hides all errorlabels.
         */
        private void HideErrorLabels()
        {
            nameErrorLabel.Visibility = Visibility.Collapsed;
            numberErrorLabel.Visibility = Visibility.Collapsed;
            emailErrorLabel.Visibility = Visibility.Collapsed;
        }

        /**
          * Clears and changes styling of a textbox when clicked.
          */
        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            // Hide errorlabels.
            HideErrorLabels();

            // Get TextBox.
            TextBox source = e.Source as TextBox;

            // Get placeholder style for comparison.
            Style placeholder = (Style)Resources["PlaceholderTxtBox"];

            if (source != null && source.Style == placeholder)
            {
                // Clear TextBox
                source.Clear();

                // Change styling of TextBox.
                source.Style = (Style)Resources["NormalTxtBox"];
            }
        }
    }
}
