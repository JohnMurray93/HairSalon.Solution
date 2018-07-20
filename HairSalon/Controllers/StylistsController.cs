using System;
using System.Collections.Generic;
using HairSalon;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers {
    public class StylistsController : Controller {

        [HttpGet ("/Stylists")]
        public ActionResult Index () {
            return View (Stylist.GetAll ());
        }

        [HttpPost ("/Stylists")]
        public ActionResult NewStylist () {
            Stylist newStylist = new Stylist (Request.Form["inputStylist"]);
            newStylist.Save ();
            return View ("Index", Stylist.GetAll ());
        }

        [HttpGet ("/Stylist/{id}")]
        public ActionResult Details (int id) {
            Dictionary<string, object> model = new Dictionary<string, object> ();
            Stylist selectedStylist = Stylist.Find (id);
            List<Client> stylistClients = selectedStylist.GetClients ();
            model.Add ("stylist", selectedStylist);
            model.Add ("client", stylistClients);
            return View (model);
        }

        [HttpPost ("/Stylist/{id}")]
        public ActionResult AddClient (int id) {
            Stylist thisStylist = Stylist.Find (id);
            Client newClient = new Client (Request.Form["inputClient"]);
            newClient.Save ();
            thisStylist.AddClient(newClient);
            return RedirectToAction ("Details");
        }

        [HttpPost ("/Stylist/{id}/Delete")]
        public ActionResult YoureFired (int id) {
            Stylist thisStylist = Stylist.Find (id);
            thisStylist.Delete ();
            return RedirectToAction ("Stylists");
        }

        [HttpPost ("/Stylists/Delete")]
        public ActionResult YoureAllFired () {
            Stylist.DeleteAll ();
            return RedirectToAction ("Index");
        }
    }
}