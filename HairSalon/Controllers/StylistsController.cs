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
            return RedirectToAction ("Index");
        }

        [HttpGet ("/Stylist/{id}")]
        public ActionResult StylistDetails (int id) {
            Stylist selectedStylist = Stylist.Find (id);
            return View (selectedStylist);
        }

        [HttpPost ("/Stylist/{id}")]
        public ActionResult AddClient (int id) {
            Stylist thisStylist = Stylist.Find (id);
            Client newClient = new Client (Request.Form["inputClient"]);
            newClient.Save ();
            thisStylist.AddClient(newClient);
            return RedirectToAction ("StylistDetails");
        }

        [HttpPost ("/Stylist/{id}/Delete")]
        public ActionResult DeleteStylist (int id) {
            Stylist thisStylist = Stylist.Find (id);
            thisStylist.Delete ();
            return RedirectToAction ("Index");
        }

        [HttpPost ("/Stylist/{id}/Clients/Delete")]
        public ActionResult DeleteStylistClients (int id) {
            Stylist thisStylist = Stylist.Find (id);
            thisStylist.DeleteStylistClients();
            return RedirectToAction ("Index");
        }

        [HttpPost ("/Stylists/Delete")]
        public ActionResult DeleteAllStylists () {
            Stylist.DeleteAll ();
            return RedirectToAction ("Index");
        }
    }
}