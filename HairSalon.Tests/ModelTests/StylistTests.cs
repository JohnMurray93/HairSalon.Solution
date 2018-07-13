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
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist () {
            // Arrange, Act
            Stylist firstStylist = new Stylist ("John");
            Stylist secondStylist = new Stylist ("John");

            // Assert
            Assert.AreEqual (firstStylist, secondStylist);
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0 () {
            //Arrange
            //Act
            int result = Stylist.GetAll ().Count;

            //Assert
            Assert.AreEqual (0, result);
        }
    }
}