using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests {
    [TestClass]
    public class ClientControllerTest {

        [TestMethod]
        public void ClientList_ReturnsCorrectView_True () {
            //Arrange
            ClientController controller = new ClientController ();

            //Act
            ActionResult ClientListView = controller.ClientList (1);

            //Assert
            Assert.IsInstanceOfType (ClientListView, typeof (ViewResult));
        }

        [TestMethod]
        public void ClientList_HasCorrectModelType_model () {
            //Arrange
            ClientController controller = new ClientController ();
            Dictionary<string, object> model = new Dictionary<string, object> ();
            Stylist selectedStylist = Stylist.Find (1);
            List<Client> stylistClients = selectedStylist.GetClients ();
            model.Add ("stylist", selectedStylist);
            model.Add ("client", stylistClients);
            IActionResult actionResult = controller.ClientList(1);
            ViewResult clientListView = controller.ClientList(1) as ViewResult;

            //Act
            var result = clientListView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType (result, typeof(Dictionary<string, object>));
        }

        [TestMethod]
        public void ClientUpdate_ReturnsCorrectView_True () {
            //Arrange
            ClientController controller = new ClientController ();

            //Act
            ActionResult ClientUpdateView = controller.ClientUpdate (1);

            //Assert
            Assert.IsInstanceOfType (ClientUpdateView, typeof (ViewResult));
        }

        [TestMethod]
        public void ClientUpdate_HasCorrectModelType_Client () {
            //Arrange
            ClientController controller = new ClientController ();
            Client thisClient = Client.Find (1);
            IActionResult actionResult = controller.ClientUpdate(1);
            ViewResult clientUpdateView = controller.ClientUpdate(1) as ViewResult;

            //Act
            var result = clientUpdateView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType (result, typeof(Client));
        }
    }
}