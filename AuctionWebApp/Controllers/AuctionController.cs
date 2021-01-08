using AuctionLibrary.Models;
using AuctionLibrary.Storage;
using AuctionWebApp.Models;
using System.Web.Mvc;

namespace AuctionWebApp.Controllers
{
    public class AuctionController : Controller
    {
        private readonly AuctionDB db = new AuctionDB();

        // GET: Front page
        public ActionResult Index()
        {
            if (Session["LoggedContactId"] != null)
            {
                return View(new SalesSupplyViewModel() { SalesSupplies = db.GetSalesSupplies() });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: Create form for new sales supply.
        public ActionResult CreateSalesSupply()
        {
            if (Session["LoggedContactId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: Create a sales supply.
        [HttpPost]
        public ActionResult CreateSalesSupply(SalesSupplyViewModel salesView)
        {
            if (Session["LoggedContactId"] != null)
            {
                if(ModelState.IsValid)
                {
                    if (int.TryParse(Session["LoggedContactId"].ToString(), out int id))
                    {
                        ContactInfo contact = db.GetContactById(id);
                        
                        // Convert deadline date and time into a DateTime object
                        /*
                        SalesSupply newSale = new SalesSupply(salesView.MetalType, salesView.MetalAmount, salesView.Deadline, contact);
                        db.SalesSupplies.Add(newSale);
                        contact.SalesSupplies.Add(newSale);
                        db.SaveChanges();
                        */
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(salesView);
                    }
                }
                else
                {
                    return View(salesView);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}