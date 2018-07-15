using System;
using System.Collections.Generic;
using HairSalon;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers {
    public class StylistController : Controller {

        [HttpGet ("/Stylists")]
        public ActionResult StylistList () {
            return View ("Stylists", Stylist.GetAll ());
        }

        [HttpPost ("/Stylists")]
        public ActionResult Stylists () {
            Stylist newStylist = new Stylist (Request.Form["inputStylist"]);
            newStylist.Save ();
            return View (Stylist.GetAll ());
        }

        [HttpPost ("/Stylists/{id}/Delete")]
        public ActionResult YoureFired (int id) {
            Stylist.DeleteStylist (id);
            Client.DeleteClients (id);
            return RedirectToAction ("Stylists");
        }

        [HttpPost ("/Stylists/Delete")]
        public ActionResult YoureAllFired () {
            Client.DeleteAll ();
            Stylist.DeleteAll ();
            return RedirectToAction ("Stylists");
        }
    }
}