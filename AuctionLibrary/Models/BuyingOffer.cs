using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuctionLibrary.Models
{
    public class BuyingOffer
    {
        public BuyingOffer(double price, SalesSupply sale, ContactInfo contact)
        {
            PriceAmount = price;
            SalesSupply = sale;
            ContactInfo = contact;
        }

        public BuyingOffer() { }

        public int Id { get; set; }
        [DisplayName("Pris")]
        public double PriceAmount { get; set; }
        [DisplayName("Salgsudbud")]
        [Required]
        public SalesSupply SalesSupply { get; set; }
        [Required]
        public ContactInfo ContactInfo { get; set; }
    }
}
