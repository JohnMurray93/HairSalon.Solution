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

        public static List<Stylist> GetAll () {
            List<Stylist> allStylists = new List<Stylist> { };
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            MySqlCommand cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader () as MySqlDataReader;
            while (rdr.Read ()) {
                int stylistId = rdr.GetInt32 (0);
                string stylistName = rdr.GetString (1);
                Stylist newStylist = new Stylist (stylistName, stylistId);
                allStylists.Add (newStylist);
            }
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
            return allStylists;
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

        public void Save () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@StylistName);";

            cmd.Parameters.Add (new MySqlParameter ("@StylistName", _name));

            cmd.ExecuteNonQuery ();
            _id = (int) cmd.LastInsertedId;

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
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