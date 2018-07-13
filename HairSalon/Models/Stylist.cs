using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models {

    public class Stylist {

        private int _id;
        private string _name;

        public Stylist (string name, int id = 0) {

            _name = name;
            _id = id;
        }

        public string GetName () {
            return _name;
        }
        public int GetId () {
            return _id;
        }

        public override bool Equals (System.Object otherStylist) {
            if (!(otherStylist is Stylist)) {
                return false;
            } else {
                Stylist newStylist = (Stylist) otherStylist;
                bool idEquality = (this.GetId () == newStylist.GetId ());
                bool nameEquality = (this.GetName () == newStylist.GetName ());
                return (idEquality && nameEquality);
            }
        }

        public override int GetHashCode () {
            return this.GetId ().GetHashCode ();
        }

        public static void DeleteAll () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";

            cmd.ExecuteNonQuery ();

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }
    }
}