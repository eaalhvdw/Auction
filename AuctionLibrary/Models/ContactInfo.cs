using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuctionLibrary.Models
{
    public class ContactInfo
    {
        public ContactInfo(string name, string number, string email)
        {
            Name = name;
            Number = number;
            Email = email;
            SalesSupplies = new List<SalesSupply>();
            BuyingOffers = new List<BuyingOffer>();
        }

        public ContactInfo() 
        {
            SalesSupplies = new List<SalesSupply>();
            BuyingOffers = new List<BuyingOffer>();
        }

        public int Id { get; set; }
        [DisplayName("Navn")]
        [StringLength(40, ErrorMessage = "Navnet skal indeholde 2 - 40 tegn.", MinimumLength = 2)]
        [Required(ErrorMessage = "Indtast venligst dit navn.", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [DisplayName("Telefonnummer")]
        [StringLength(15, ErrorMessage = "Telefonnummeret skal indeholde 8 - 15 tal.", MinimumLength = 8)]
        [Required(ErrorMessage = "Indtast venligst dit telefonnummer.", AllowEmptyStrings = false)]
        public string Number { get; set; }
        [DisplayName("E-mailadresse")]
        [Required(ErrorMessage = "Indtast venligst din e-mailadresse.", AllowEmptyStrings = false)]
        [MinLength(8, ErrorMessage = "E-mailadressen skal indeholde minimum 6 tegn.")]
        public string Email { get; set; }
        public string LoginNameErrorMsg { get; set; }
        public string LoginNumberErrorMsg { get; set; }
        public string LoginEmailErrorMsg { get; set; }
        public List<SalesSupply> SalesSupplies { get; set; }
        public List<BuyingOffer> BuyingOffers { get; set; }
    }
}
