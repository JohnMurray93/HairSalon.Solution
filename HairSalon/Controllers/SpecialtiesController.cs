using System;
using System.Collections.Generic;
using HairSalon;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers {
    public class SpecialtiesController : Controller {

        [HttpGet ("/Specialties")]
        public ActionResult Index () {
            return View (Specialty.GetAll ());
        }

        [HttpPost ("/Specialties/NewSpecialty")]
        public ActionResult NewSpecialty () {
            Specialty newSpecialty = new Specialty (Request.Form["inputSpecialty"]);
            newSpecialty.Save ();
            return RedirectToAction ("Index");
        }

        [HttpGet ("/Specialty/{id}/Details")]
        public ActionResult SpecialtyDetails (int id) {
            Specialty selectedSpecialty = Specialty.Find (id);
            return View (selectedSpecialty);
        }

        [HttpGet ("/Specialty/{id}/Update")]
        public ActionResult UpdateSpecialty (int id) {
            Specialty thisSpecialty = Specialty.Find (id);
            return View (thisSpecialty);
        }

        [HttpPost ("/Specialty/{id}/Update")]
        public ActionResult EditSpecialty (int id) {
            Specialty thisSpecialty = Specialty.Find (id);
            thisSpecialty.UpdateSpecialty (Request.Form["new-name"]);
            return RedirectToAction ("Index");
        }

        [HttpPost ("/Specialty/{id}/AddStylist")]
        public ActionResult AddStylist (int id) {
            Specialty thisSpecialty = Specialty.Find (id);
            Stylist newStylist = new Stylist (Request.Form["inputStylist"]);
            newStylist.Save ();
            thisSpecialty.AddStylist(newStylist);
            return RedirectToAction ("SpecialtyDetails");
        }

        [HttpPost ("/Specialty/{id}/Delete")]
        public ActionResult DeleteSpecialty (int id) {
            Specialty thisSpecialty = Specialty.Find (id);
            thisSpecialty.Delete ();
            return RedirectToAction ("Index");
        }

        [HttpPost ("/Specialty/{id}/Stylists/Delete")]
        public ActionResult DeleteSpecialtyStylists (int id) {
            Specialty thisSpecialty = Specialty.Find (id);
            thisSpecialty.DeleteSpecialtyStylists();
            return RedirectToAction ("Index");
        }

        [HttpPost ("/Specialties/Delete")]
        public ActionResult DeleteAllSpecialties () {
            Specialty.DeleteAll ();
            return RedirectToAction ("Index");
        }
    }
}