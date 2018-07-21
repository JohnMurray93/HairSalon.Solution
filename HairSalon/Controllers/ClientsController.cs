using System;
using System.Collections.Generic;
using HairSalon;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers {
    public class ClientsController : Controller {

        [HttpGet ("/Clients")]
        public ActionResult Index () {
            return View (Client.GetAll ());
        }

        [HttpPost ("/Clients/NewClient")]
        public ActionResult NewClient () {
            Client newClient = new Client (Request.Form["inputClient"]);
            newClient.Save ();
            return RedirectToAction ("Index");
        }

        [HttpGet ("/Client/{id}/Details")]
        public ActionResult ClientDetails (int id) {
            // Dictionary<string, object> model = new Dictionary<string, object> ();
            Client selectedClient = Client.Find (id);
            // List<Stylist> stylistStylists = selectedClient.GetStylists ();
            // model.Add ("stylist", selectedClient);
            // model.Add ("client", stylistStylists);
            return View (selectedClient);
        }

        [HttpPost ("/Client/{id}/AddStylist")]
        public ActionResult AddStylist (int id) {
            Client thisClient = Client.Find (id);
            Stylist newStylist = new Stylist (Request.Form["inputName"]);
            newStylist.Save ();
            thisClient.AddStylist(newStylist);
            return RedirectToAction ("ClientDetails");
        }

        [HttpPost ("/Client/{id}/Delete")]
        public ActionResult DeleteClient (int id) {
            Client thisClient = Client.Find (id);
            thisClient.Delete ();
            return RedirectToAction ("Clients");
        }

        [HttpGet ("/Client/{id}/Update")]
        public ActionResult UpdateClient (int id) {
            Client thisClient = Client.Find (id);
            return View (thisClient);
        }

        [HttpPost ("/Client/{id}/Update")]
        public ActionResult EditClient (int id) {
            Client thisClient = Client.Find (id);
            thisClient.UpdateClient (Request.Form["new-name"]);
            return RedirectToAction ("Index");
        }

        [HttpPost ("/Clients/Delete")]
        public ActionResult DeleteAllClients () {
            Client.DeleteAll ();
            return RedirectToAction ("Index");
        }
    }
}