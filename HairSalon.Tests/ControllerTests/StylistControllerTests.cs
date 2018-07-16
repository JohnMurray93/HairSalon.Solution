using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests {
    [TestClass]
    public class StylistControllerTest {

        [TestMethod]
        public void StylistList_ReturnsCorrectView_True () {
            //Arrange
            StylistController controller = new StylistController ();

            //Act
            ActionResult StylistListView = controller.StylistList ();

            //Assert
            Assert.IsInstanceOfType (StylistListView, typeof (ViewResult));
        }

        [TestMethod]
        public void Stylists_HasCorrectModelType_StylistList () {
            //Arrange
            StylistController controller = new StylistController ();
            IActionResult actionResult = controller.StylistList();
            ViewResult stylistListView = controller.StylistList() as ViewResult;

            //Act
            var result = stylistListView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType (result, typeof(List<Stylist>));
        }
    }
}