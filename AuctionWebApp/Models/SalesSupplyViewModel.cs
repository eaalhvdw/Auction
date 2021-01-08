using AuctionLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AuctionWebApp.Models
{
    public class SalesSupplyViewModel
    {
        public SalesSupplyViewModel() { }

        [DisplayName("Metaltype")]
        [Required(ErrorMessage = "Vælg venligst en metaltype.")]
        public MetalType MetalType { get; set; }
        [DisplayName("Vægt (g.)")]
        [Required(ErrorMessage = "Indtast venligst en vægtmængde i antal gram af metallet.", AllowEmptyStrings = false)]
        public int MetalAmount { get; set; }
        [DisplayName("Deadline")]
        public DateTime Deadline { get; set; }
        [DisplayName("Deadline Dato")]
        [Required(ErrorMessage = "Vælg venligst en dato for auktionens udløb.")]
        [DataType(DataType.Date)]
        public DateTime DeadlineDate { get; set; }
        [DisplayName("Deadline Tidspunkt")]
        [Required(ErrorMessage = "Vælg venligst et tidspunkt for auktionens udløb.")]
        [DataType(DataType.Time)]
        public DateTime DeadlineTime { get; set; }
        [DisplayName("Salgsudbud")]
        public List<SalesSupply> SalesSupplies { get; set; }
        
        // Properties used to create listbox in index view.
        public int[] SelectedSalesSupplies { get; set; }
        public SelectList SalesSupplySelectList 
        { 
            get
            {
                return new SelectList(SalesSupplies, nameof(SalesSupply.Id), nameof(SalesSupply.SalesSupplyToString));
            }
        }
    }
}