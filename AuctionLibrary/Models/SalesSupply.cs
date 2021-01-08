using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuctionLibrary.Models
{
    public class SalesSupply
    {
        public SalesSupply(MetalType metalType, int metalAmount, DateTime deadline, ContactInfo contact) 
        {
            MetalType = metalType;
            MetalAmount = metalAmount;
            HighestPrice = 0;
            Deadline = deadline;
            ContactInfo = contact;
            HighestBuyingOffer = null;

            ImgFolderPath = "C:/Users/Louise Helene Hamle/source/repos/Lesson22Assignment/Auction/AuctionLibrary/Img";

            if (MetalType == MetalType.Guld)
            {
                ImgPath = ImgFolderPath + "/Guld.png";
            }
            else if (MetalType == MetalType.Sølv)
            {
                ImgPath = ImgFolderPath + "/Sølv.png";
            }
            else if (MetalType == MetalType.Platin)
            {
                ImgPath = ImgFolderPath + "/Platin.png";
            }
            else
            {
                ImgPath = ImgFolderPath + "/Palladium.png";
            }
        }

        public SalesSupply() { }

        public int Id { get; set; }
        public MetalType MetalType { get; set; }
        public int MetalAmount { get; set; }
        public double HighestPrice { get; set; }
        public DateTime Deadline { get; set; }
        public BuyingOffer HighestBuyingOffer { get; set; }
        [Required]
        public ContactInfo ContactInfo { get; set; }
        public string ImgFolderPath { get; set; }
        public string ImgPath { get; set; }

        /**
         * Helper ToString method for the
         * display of the metaltype prop.
         */
        public string MetalTypeToString
        {
            get
            {
                string text = "Metaltype: " + MetalType;
                return FixedLength(20, text);
            }
        }

        /**
         * Helper ToString method for the
         * display of the metalamount prop.
         */
        public string MetalAmountToString
        {
            get
            {
                string text = "Mængde: " + MetalAmount + " gram";
                return FixedLength(20, text);
            }
        }

        /**
         * Helper ToString method for the
         * display of the highest price prop.
         */
        public string HighestPriceToString
        {
            get
            {
                string text = "Højeste bud: " + HighestPrice + " kr";
                return FixedLength(20, text);
            }
        }

        /**
         * Helper ToString method for the
         * display of the deadline prop.
         */
        public string DeadlineToString
        {
            get
            {
                string text = "";

                if(Deadline < DateTime.Now)
                {
                    text = "Auktionen er udløbet d. " + Deadline.ToShortDateString() + " kl. " + Deadline.ToShortTimeString();
                }
                else
                {
                    text = "Auktionen udløber d. " + Deadline.ToShortDateString() + " kl. " + Deadline.ToShortTimeString();
                }
                return FixedLength(45, text);
            }
        }

        /**
         * Custom ToString method to 
         * display a sales supply object.
         * This is used in the WebApp.
         */
        public string SalesSupplyToString
        {
            get
            {
                string metalType = FixedLength(30, MetalTypeToString);
                string metalAmount = FixedLength(30, MetalAmountToString);
                string price = FixedLength(30, HighestPriceToString);
                string deadline = FixedLength(50, DeadlineToString);

                string metalTypeFinal = metalType.Replace(" ", "\u00A0");
                string metalAmountFinal = metalAmount.Replace(" ", "\u00A0");
                string priceFinal = price.Replace(" ", "\u00A0");
                string deadlineFinal = deadline.Replace(" ", "\u00A0");

                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("{0}", metalTypeFinal));
                sb.Append(string.Format("{0}", metalAmountFinal));
                sb.Append(string.Format("{0}", priceFinal));
                sb.Append(string.Format("{0}", deadlineFinal));
                return sb.ToString();
            }
        }

        /**
         * Helper method to format the length 
         * of the ToString helper methods.
         */
        private string FixedLength(int length, string text)
        {
            return text.PadRight(length).Substring(0, length);
        }
    }
}
