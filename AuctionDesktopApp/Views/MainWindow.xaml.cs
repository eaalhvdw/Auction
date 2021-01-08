using AuctionLibrary.Models;
using AuctionLibrary.Storage;
using System;
using System.Windows;

namespace AuctionDesktopApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly string _name;
        private readonly string _number;
        private readonly string _email;
        private readonly AuctionDB _context;
        private ContactInfo _buyerInfo;

        public MainWindow(string name, string number, string email)
        {
            InitializeComponent();
            _name = name;
            _number = number;
            _email = email;
            _context = new AuctionDB();
            DataContext = _context;

            ContactInfo dbBuyerInfo = _context.GetContactInfoByStrings(_name, _number, _email);

            if (dbBuyerInfo == null)
            {
                _buyerInfo = new ContactInfo(_name, _number, _email);
                _context.ContactInfoes.Add(_buyerInfo);
                _context.SaveChanges();
            }
            else
            {
                _buyerInfo = dbBuyerInfo;
            }

            ListBox.ItemsSource = _context.GetSalesSupplies();
        }

        //------------------------------ EVENTHANDLERS --------------------------------------------------------

        /**
         * This eventhandler validates the user 
         * as a buyer and returns a new window
         * if the validations checks out.
         * Validation fails if the buyer is the
         * seller of the salesSupply or if the
         * auction for the sales supply has 
         * reach deadline. As a bonus if the 
         * auction for the sales supply has 
         * reached deadline and the buyer 
         * has won the action, a dialog will
         * appear to inform the user, if the
         * user cliks on the auction.
         */
        private void AddBuyingOffer_DoubleClick(object sender, RoutedEventArgs e)
        {
            SalesSupply listItem = ListBox.SelectedItem as SalesSupply;
            SalesSupply salesSupply = _context.GetSalesSupply(listItem.Id);
            ContactInfo contactSeller = salesSupply.ContactInfo;

            if(contactSeller !=  null)
            {
                if (contactSeller != _buyerInfo && salesSupply.Deadline > DateTime.Now)
                {
                    BuyingOfferWindow offerWindow = new BuyingOfferWindow(this, salesSupply, _buyerInfo);
                    offerWindow.Show();
                    this.Hide();
                }
                else if (contactSeller == _buyerInfo)
                {
                    MessageBox.Show("Du kan ikke byde på dit eget salgsudbud.", "Fejlmeddelelse", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else if (salesSupply.HighestBuyingOffer != null && salesSupply.HighestBuyingOffer.ContactInfo == _buyerInfo)
                {
                    MessageBox.Show("Tillykke! Du har vundet denne auktion.", "Lykønskning", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Tidsfristen er udløbet. Auktionen er lukket.", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
