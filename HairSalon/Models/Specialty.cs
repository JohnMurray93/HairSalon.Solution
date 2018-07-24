using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models {

    public class Specialty {

        private int _id;
        private string _name;

        public Specialty (string name, int id = 0) {

            _name = name;
            _id = id;
        }

        public string GetName () {
            return _name;
        }
        public int GetId () {
            return _id;
        }

        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(@"SELECT stylist_id FROM specialties_stylists WHERE specialty_id = @SpecialtyId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SpecialtyId", _id));

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<int> stylistIds = new List<int> {};
            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                stylistIds.Add(stylistId);
            }
            rdr.Dispose();

            List<Stylist> stylists = new List<Stylist> {};
            foreach (int stylistId in stylistIds)
            {
                MySqlCommand stylistQuery = new MySqlCommand(@"SELECT * FROM stylists WHERE id = @StylistId;", conn);

                MySqlParameter stylistIdParameter = new MySqlParameter();
                stylistIdParameter.ParameterName = "@StylistId";
                stylistIdParameter.Value = stylistId;
                stylistQuery.Parameters.Add(stylistIdParameter);

                var stylistQueryRdr = stylistQuery.ExecuteReader() as MySqlDataReader;
                while(stylistQueryRdr.Read())
                {
                    int thisStylistId = stylistQueryRdr.GetInt32(0);
                    string stylistDescription = stylistQueryRdr.GetString(1);
                    Stylist foundStylist = new Stylist(stylistDescription, thisStylistId);
                    stylists.Add(foundStylist);
                }
                stylistQueryRdr.Dispose();
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return stylists;
        }

    public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(@"INSERT INTO specialties_stylists (specialty_id, stylist_id) VALUES (@SpecialtyId, @StylistId);", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SpecialtyId", _id));

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@StylistId";
            stylist_id.Value = newStylist.GetId();
            cmd.Parameters.Add(stylist_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Specialty> GetAll () {
            List<Specialty> allSpecialtys = new List<Specialty> { };
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM specialties;", conn);
            MySqlDataReader rdr = cmd.ExecuteReader () as MySqlDataReader;
            while (rdr.Read ()) {
                int stylistId = rdr.GetInt32 (0);
                string stylistName = rdr.GetString (1);
                Specialty newSpecialty = new Specialty (stylistName, stylistId);
                allSpecialtys.Add (newSpecialty);
            }
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
            return allSpecialtys;
        }

        public override bool Equals (System.Object otherSpecialty) {
            if (!(otherSpecialty is Specialty)) {
                return false;
            } else {
                Specialty newSpecialty = (Specialty) otherSpecialty;
                bool idEquality = (this.GetId () == newSpecialty.GetId ());
                bool nameEquality = (this.GetName () == newSpecialty.GetName ());
                return (idEquality && nameEquality);
            }
        }

        public override int GetHashCode () {
            return this.GetId ().GetHashCode ();
        }

        public void Save () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"INSERT INTO specialties (name) VALUES (@SpecialtyName);", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SpecialtyName", _name));

            cmd.ExecuteNonQuery ();
            _id = (int) cmd.LastInsertedId;

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public static Specialty Find (int id) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM specialties WHERE id = (@SearchId);", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", id));

            var rdr = cmd.ExecuteReader () as MySqlDataReader;
            int SpecialtyId = 0;
            string SpecialtyName = "";

            while (rdr.Read ()) {
                SpecialtyId = rdr.GetInt32 (0);
                SpecialtyName = rdr.GetString (1);
            }
            Specialty newSpecialty = new Specialty (SpecialtyName, SpecialtyId);
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
            return newSpecialty;
        }

        public void UpdateSpecialty (string newSpecialtyName) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"UPDATE specialties SET name = @NewSpecialtyName WHERE id = @SearchId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", _id));
            cmd.Parameters.Add (new MySqlParameter ("@NewSpecialtyName", newSpecialtyName));

            cmd.ExecuteNonQuery ();
            _name = newSpecialtyName;

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("DELETE FROM specialties WHERE id = @SpecialtyId; DELETE FROM specialties_stylists WHERE stylist_id = @SpecialtyId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SpecialtyId", _id));

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public static void DeleteAll () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"DELETE FROM specialties; DELETE FROM specialties_stylists", conn);

            cmd.ExecuteNonQuery ();

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }
        public void DeleteSpecialtyStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(@"SELECT stylist_id FROM specialties_stylists WHERE specialty_id = @SpecialtyId; DELETE FROM specialties_stylists WHERE specialty_id = @SpecialtyId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SpecialtyId", this._id));

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            rdr.Dispose();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
    }
}