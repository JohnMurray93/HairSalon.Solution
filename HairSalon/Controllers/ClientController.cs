using System;
using System.Collections.Generic;
using HairSalon;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers {
    public class ClientController : Controller {

        [HttpGet ("/Stylists/{id}")]
        public ActionResult ClientList (int id) {
            Dictionary<string, object> model = new Dictionary<string, object> ();
            Stylist selectedStylist = Stylist.Find (id);
            List<Client> stylistClients = selectedStylist.GetClients ();
            model.Add ("stylist", selectedStylist);
            model.Add ("client", stylistClients);
            return View (model);
        }

        [HttpPost ("/Stylists/{id}")]
        public ActionResult AddClient (int id) {
            string clientName = Request.Form["inputClient"];
            Client newClient = new Client (clientName, id);
            newClient.Save ();
            Dictionary<string, object> model = new Dictionary<string, object> ();
            Stylist selectedStylist = Stylist.Find (Int32.Parse (Request.Form["stylist-id"]));
            List<Client> stylistClients = selectedStylist.GetClients ();
            model.Add ("client", stylistClients);
            model.Add ("stylist", selectedStylist);
            return View ("ClientList", model);
        }

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

        [HttpPost ("/Clients/Delete")]
        public ActionResult DeleteClients () {
            Client.DeleteAll ();
            return RedirectToAction ("Stylists", "Stylist");
        }

    }
}