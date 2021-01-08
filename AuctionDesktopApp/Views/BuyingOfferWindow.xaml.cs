using AuctionLibrary.Models;
using AuctionLibrary.Storage;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AuctionDesktopApp.Views
{
    public partial class BuyingOfferWindow : Window
    {
        private readonly MainWindow _main;
        private readonly SalesSupply _salesSupply;
        private readonly ContactInfo _buyerInfo;
        private readonly AuctionDB _context;

        public BuyingOfferWindow(MainWindow main, SalesSupply salesSupply, ContactInfo buyerInfo)
        {
            InitializeComponent();
            _main = main;
            _context = new AuctionDB();
            _salesSupply = _context.GetSalesSupply(salesSupply.Id);
            _buyerInfo = _context.GetContactById(buyerInfo.Id);
            DataContext = _salesSupply;

            // Set image.
            MetalImage.DataContext = _salesSupply.ImgPath;

            // Add text to binded labels.
            BindedMetalAmount.Content = _salesSupply.MetalAmount + " gram";
            BindedPrice.Content = _salesSupply.HighestPrice + " kr";
            BindedDeadline.DataContext = _salesSupply.DeadlineToString;
        }

        //------------------------------ EVENTHANDLERS --------------------------------------------------------

        /**
         * This method assigns the best BuyingOffer
         * of a salesSupply to a new value object,
         * overwriting the existing value.
         */
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string priceStr = TxtBoxPriceOffer.Text;
            double oldOffer = _salesSupply.HighestPrice;

            if(priceStr == null || priceStr == "" || priceStr == "Angiv pris")
            {
                ErrorLabel.Content = "Indtast venligst et beløb.";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    double price = Convert.ToDouble(priceStr);

                    if (price > oldOffer && !priceStr.Contains("-"))
                    {
                        BuyingOffer offer = new BuyingOffer(price, _salesSupply, _buyerInfo);
                        _context.AddBuyingOfferToSalesSupply(_salesSupply, offer);
                        _context.SaveChanges();
                        
                        _main.ListBox.ItemsSource = null;
                        _main.ListBox.ItemsSource = _context.GetSalesSupplies();
                        _main.Show();
                        this.Hide();
                    }
                    else if (priceStr.Contains("-"))
                    {
                        ErrorLabel.Content = "Indtast venligst et positivt tal.";
                        ErrorLabel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ErrorLabel.Content = "Indtast venligst et beløb som overgår det forrige bud.";
                        ErrorLabel.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception)
                {
                    ErrorLabel.Content = "Indtast venligst et tal.";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
            }
        }

        /**
         * This method just returns to the main page.
         */
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _main.ListBox.ItemsSource = null;
            _main.ListBox.ItemsSource = _context.GetSalesSupplies();
            _main.Show();
            this.Hide();
        }

        /**
          * Clears and changes styling of a textbox when clicked.
          */
        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            // Hide errorlabel.
            ErrorLabel.Visibility = Visibility.Collapsed;

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
