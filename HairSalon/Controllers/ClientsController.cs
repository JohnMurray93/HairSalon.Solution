using System;
using System.Collections.Generic;
using HairSalon;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers {
    public class ClientController : Controller {

        [HttpGet ("/Client/{id}/Update")]
        public ActionResult ClientUpdate (int id) {
            Client thisClient = Client.Find (id);
            return View (thisClient);
        }

        [HttpPost ("/Client/{id}/Update")]
        public ActionResult ClientEdit (int id) {
            Client thisClient = Client.Find (id);
            thisClient.UpdateClient (Request.Form["new-name"]);
            return RedirectToAction ("Stylists", "Stylist");
        }

        [HttpPost ("/Client/{id}/Delete")]
        public ActionResult ClientDelete (int id) {
            Client thisClient = Client.Find (id);
            thisClient.Delete();
            return RedirectToAction ("Stylists", "Stylist");
        }

        [HttpPost ("/Clients/Delete")]
        public ActionResult Deletes () {
            Client.DeleteAll ();
            return RedirectToAction ("Stylists", "Stylist");
        }

    }
}