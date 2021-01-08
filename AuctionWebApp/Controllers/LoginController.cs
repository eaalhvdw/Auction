using AuctionLibrary.Models;
using AuctionLibrary.Storage;
using System.Web.Mvc;

namespace AuctionWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuctionDB db = new AuctionDB();

        // GET: Login form
        public ActionResult Login()
        {
            return View();
        }

        // POST: Verify user input and redirect to Index in AuctionController.
        [HttpPost]
        public ActionResult Login(ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                int errors = 0;

                if (!db.VerifyName(contactInfo.Name))
                {
                    errors++;
                    contactInfo.LoginNameErrorMsg = "Navnet er ugyldigt.";
                    return View(contactInfo);
                }

                if (!db.VerifyNumber(contactInfo.Number))
                {
                    errors++;
                    contactInfo.LoginNumberErrorMsg = "Telefonnummeret er ugyldigt.";
                    return View(contactInfo);
                }

                if (!db.VerifyEmail(contactInfo.Email))
                {
                    errors++;
                    contactInfo.LoginEmailErrorMsg = "E-mailadressen er ugyldig.";
                    return View(contactInfo);
                }

                if (errors == 0)
                {
                    var resultContactInfo = db.GetContactInfoByStrings(contactInfo.Name, contactInfo.Number, contactInfo.Email);
                    if (resultContactInfo != null)
                    {
                        Session["LoggedContactId"] = resultContactInfo.Id.ToString();
                        return RedirectToAction("Index", "Auction");
                    }
                }
            }
            return View(contactInfo);
        }

        // Log out
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}