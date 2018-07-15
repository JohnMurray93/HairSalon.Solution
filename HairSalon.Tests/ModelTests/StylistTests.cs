using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests {
    [TestClass]
    public class StylistTests : IDisposable {

        public StylistTests () {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=john_murray;";
        }

        public void Dispose () {
            Stylist.DeleteAll ();
            Client.DeleteAll ();
        }

        [TestMethod]
        public void IsEquals_ReturnsTrueIfNamesAreTheSame_True () {
            // Arrange, Act
            Stylist firstStylist = new Stylist ("John");
            Stylist secondStylist = new Stylist ("John");

            // Assert
            Assert.AreEqual (firstStylist, secondStylist);
        }

        [TestMethod]
        public void IsGetAll_DbStartsEmpty_0 () {
            //Arrange
            //Act
            int result = Stylist.GetAll ().Count;

            //Assert
            Assert.AreEqual (0, result);
        }

        [TestMethod]
        public void IsSave_SavesStylistToDatabase_True () {
            //Arrange
            Stylist testStylist = new Stylist ("Mo");

            //Act
            testStylist.Save ();
            List<Stylist> result = Stylist.GetAll ();
            List<Stylist> testList = new List<Stylist> { testStylist };

            //Assert
            CollectionAssert.AreEqual (testList, result);
        }

        [TestMethod]
        public void IsGetAll_ReturnsAllStylists_True () {
            //Arrange
            Stylist newStylist1 = new Stylist ("stylist01");
            newStylist1.Save ();
            Stylist newStylist2 = new Stylist ("stylist02");
            newStylist2.Save ();
            //Act

            List<Stylist> twoStylists = new List<Stylist> { newStylist1, newStylist2 };
            List<Stylist> result = Stylist.GetAll ();

            //Assert
            CollectionAssert.AreEqual (twoStylists, result);
            Console.WriteLine (result.Count);
            Console.WriteLine (twoStylists.Count);
        }

        [TestMethod]
        public void IsFind_ReturnsFoundStylistsFromDatabase_FoundStylist () {
            //Arrange
            Stylist testStylist = new Stylist ("Stylist");
            testStylist.Save ();

            //Act
            Stylist foundStylist = Stylist.Find (testStylist.GetId ());

            //Assert
            Assert.AreEqual (testStylist, foundStylist);
        }

        [TestMethod]
        public void IsDeleteStylist_DeletesOneStylist_True () {
            //Arrange
            Stylist newStylist1 = new Stylist ("stylist01");
            newStylist1.Save ();
            Stylist newStylist2 = new Stylist ("stylist02");
            newStylist2.Save ();
            Stylist newStylist3 = new Stylist ("stylist03");
            newStylist2.Save ();
            //Act

            List<Stylist> threeStylists = Stylist.GetAll ();
            Stylist.DeleteStylist (newStylist2.GetId ());
            List<Stylist> afterDelete = Stylist.GetAll ();

            //Assert
            CollectionAssert.AreNotEqual (threeStylists, afterDelete);
        }

        [TestMethod]
        public void IsGetClients_GetStylistClientList_True () {
            //Arrange
            Stylist stylistOne = new Stylist ("Miranda", 1);

            Client clientOne = new Client ("Frank", 1);
            clientOne.Save ();
            Client clientTwo = new Client ("Tom", 1);
            clientTwo.Save ();
            Client clientThree = new Client ("Gerald", 1);
            clientThree.Save ();
            //Act
            int clientCount = stylistOne.GetClients ().Count;

            //Assert
            Assert.AreEqual (3, clientCount);
        }
    }
}