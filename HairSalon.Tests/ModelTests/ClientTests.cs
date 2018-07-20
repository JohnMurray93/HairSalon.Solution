using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests {

    [TestClass]
    public class ClientTests : IDisposable {
        public ClientTests () {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=john_murray_test;";
        }

        public void Dispose () {
            Stylist.DeleteAll ();
            Client.DeleteAll ();
        }

        [TestMethod]
        public void IsEquals_SameClientIsSame_True () {
            //Arrange, Act
            Client firstClient = new Client ("client", 1);
            Client secondClient = new Client ("client", 1);

            //Assert
            Assert.AreEqual (firstClient, secondClient);
        }

        [TestMethod]
        public void IsSave_SavesClientToDatabase_True () {
            //Arrange
            Client testClient = new Client ("client", 1);
            testClient.Save ();

            //Act
            List<Client> result = Client.GetAll ();
            List<Client> testList = new List<Client> { testClient };

            //Assert
            CollectionAssert.AreEqual (testList, result);
        }

        [TestMethod]
        public void IsSave_DatabaseAssignsIdToObject_True () {
            //Arrange
            Client testClient = new Client ("client", 1);
            testClient.Save ();

            //Act
            Client savedClient = Client.GetAll () [0];

            int result = savedClient.GetId ();
            int testId = testClient.GetId ();

            //Assert
            Assert.AreEqual (testId, result);
        }

        [TestMethod]
        public void IsFind_FindClient_True () {
            //Arrange
            Client testClient = new Client ("client", 1);
            testClient.Save ();

            //Act
            Client foundClient = Client.Find (testClient.GetId ());

            //Assert
            Assert.AreEqual (testClient, foundClient);
        }

        [TestMethod]
        public void IsUpdateClient_UpdateClientName_True () {
            //Arrange
            Client testClient = new Client ("Clint", 1);
            testClient.Save ();
            Client testClientTwo = new Client ("George", 1);
            testClientTwo.Save ();

            //Act
            testClient.UpdateClient ("George");

            //Assert
            Assert.AreEqual (testClient.GetName (), testClientTwo.GetName ());
        }

        [TestMethod]
        public void IsDeleteClient_DeletesOneClient_True () {
            //Arrange
            Client newClient1 = new Client ("Client01", 1);
            newClient1.Save ();
            Client newClient2 = new Client ("Client02", 1);
            newClient2.Save ();
            Client newClient3 = new Client ("Client03", 1);
            newClient2.Save ();
            //Act

            List<Client> threeClients = Client.GetAll ();
            Client.DeleteClient (newClient2.GetId ());
            List<Client> threeClientsMinusOne = Client.GetAll ();
            int clientCount = threeClients.Count;
            int newClientCount = threeClientsMinusOne.Count;

            //Assert
            Assert.AreNotEqual (clientCount, newClientCount);
        }
    }
}