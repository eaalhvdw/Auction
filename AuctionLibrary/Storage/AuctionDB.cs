using AuctionLibrary.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace AuctionLibrary.Storage
{
    public class AuctionDB : DbContext
    {
        public AuctionDB() : base("name=AuctionConnectionString") { }

        public DbSet<ContactInfo> ContactInfoes { get; set; }
        public DbSet<SalesSupply> SalesSupplies { get; set; }

        /**
         * Method to update references, used when a 
         * new buyingoffer is placed on a salesSupply.
         */
        public void AddBuyingOfferToSalesSupply(SalesSupply sale, BuyingOffer offer)
        {
            SalesSupply dbSalesSupply = SalesSupplies
                .Where(s => s.Id == sale.Id)
                .FirstOrDefault();

            dbSalesSupply.HighestBuyingOffer = offer;
            dbSalesSupply.HighestPrice = offer.PriceAmount;
            offer.SalesSupply = dbSalesSupply;
        }

        /**
         * Method to get contact information
         * by the login inputs, name, number
         * and email. It also retrieves both
         * the list of sales supplies and the
         * list of buying offers that are made
         * with these contact informations.
         */
        public ContactInfo GetContactInfoByStrings(string name, string number, string email)
        {
            return ContactInfoes
                .Where(c => c.Name == name && c.Number == number && c.Email == email)
                .Include(c => c.SalesSupplies)
                .Include(c => c.BuyingOffers)
                .FirstOrDefault();
        }

        /**
         * Method to get contact information
         * by an id, including both the list 
         * of sales supplies and the list of 
         * buying offers that are made with 
         * these contact informations.
         */
        public ContactInfo GetContactById(int id)
        {
            return ContactInfoes
                .Where(c => c.Id == id)
                .Include(c => c.SalesSupplies)
                .Include(c => c.BuyingOffers)
                .FirstOrDefault();
        }

        /**
         * Method to get sales supply 
         * including both the contact
         * information and the best
         * buying offer.
         */
        public SalesSupply GetSalesSupply(int id)
        {
            return SalesSupplies
                .Where(s => s.Id == id)
                .Include(s => s.ContactInfo)
                .Include(s => s.HighestBuyingOffer)
                .FirstOrDefault();
        }


        /**
         * Method to get all sales supplies 
         * including both the contact info 
         * and the Buying offer.
         */
        public List<SalesSupply> GetSalesSupplies()
        {
            return SalesSupplies
                .Include(s => s.HighestBuyingOffer)
                .Include(s => s.ContactInfo)
                .ToList();
        }

        /**
         * This method validates a name.
         * A name cannot contain numbers or symbols,
         * except for '-'. This method verifies a 
         * name by matching the string with at least 
         * one of the letter characters from the danish 
         * alphabet at the beginning, followed by zero 
         * or more occurences of a group with zero or 
         * one dash or whitespace character followed 
         * by at least one letter character. Finally 
         * the string must end with a letter character.
         * This verification is an updated version of
         * my previous regex for name verification, as
         * I since then have figured out how to eliminate 
         * the earlier flaw, without using if-else statements.
         */
        public bool VerifyName(string name)
        {
            string pattern = @"^[a-zæøå]+((-|\s)?[a-zæøå]+)*[a-zæøå]$";
            Regex regx = new Regex(pattern, RegexOptions.IgnoreCase);

            if (regx.IsMatch(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * This method validates a number.
         * A phone number cannot contain letter 
         * chacacters or symbols, except for '+' 
         * and '-' depending on the phone number 
         * format. This method verifies a phone
         * number by matching the string with
         * zero or one occurence of either 1)
         * a '+' symbol followed by two digits
         * and zero or one whitespace characters 
         * or 2) two zeros and zero or one 
         * whitespace characters followed by 
         * two digits at the beginnning. The 
         * beginning is followed by at least 
         * one occurence of a digit and zero
         * or one occurences of a dash or a 
         * whitespace character. Then zero or
         * more occurences of one to many digits 
         * follows and finally the string must
         * end with a digit.
         */
         public bool VerifyNumber(string number)
        {
            string pattern = @"^(\+\d{2}(\s)?|00(\s)?\d{2})?(\d{1}(-|\s)?)+(\d{1,})+\d$";
            Regex regx = new Regex(pattern, RegexOptions.IgnoreCase);

            if(regx.IsMatch(number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * This method validates email by matching all letters, numbers and specific 
         * signs: '.', '_', '&', '/', '%', '+' and '-' one or more times
         * in a pattern that contains a single '@' and ends with a '.'
         * followed by two or three of any letter characters.
         */
        public bool VerifyEmail(string email)
        {
            string pattern = @"^[a-zæøå0-9._&\/%+-]+@[a-zæøå0-9.-]+\.[a-zæøå]{2,3}?$";
            Regex regx = new Regex(pattern, RegexOptions.IgnoreCase);

            if (regx.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
