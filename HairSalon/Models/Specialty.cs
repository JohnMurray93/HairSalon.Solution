// using System;
// using System.Collections.Generic;
// using MySql.Data.MySqlClient;

// namespace HairSalon.Models {

//     public class Specialty {

//         private int _id;
//         private string _name;

//         public Specialty (string name, int id = 0) {

//             _name = name;
//             _id = id;
//         }

//         public string GetName () {
//             return _name;
//         }
//         public int GetId () {
//             return _id;
//         }

//         public List<Stylist> GetStylists () {
//             List<Stylist> allSpecialtyStylists = new List<Stylist> { };
//             MySqlConnection conn = DB.Connection ();
//             conn.Open ();
//             var cmd = conn.CreateCommand () as MySqlCommand;
//             cmd.CommandText = @"SELECT * FROM Stylists WHERE Specialty_id = @SpecialtyId ORDER BY name;";

//             cmd.Parameters.Add (new MySqlParameter ("@SpecialtyId", this._id));

//             var rdr = cmd.ExecuteReader () as MySqlDataReader;
//             while (rdr.Read ()) {
//                 int StylistId = rdr.GetInt32 (0);
//                 string StylistDescription = rdr.GetString (1);
//                 int StylistspecialtyId = rdr.GetInt32 (2);
//                 Stylist newStylist = new Stylist (StylistDescription, StylistspecialtyId, StylistId);
//                 allSpecialtyStylists.Add (newStylist);
//             }
//             conn.Close ();
//             if (conn != null) {
//                 conn.Dispose ();
//             }
//             return allSpecialtyStylists;
//         }

//         public static List<Specialty> GetAll () {
//             List<Specialty> allSpecialtys = new List<Specialty> { };
//             MySqlConnection conn = DB.Connection ();
//             conn.Open ();
//             MySqlCommand cmd = conn.CreateCommand () as MySqlCommand;
//             cmd.CommandText = @"SELECT * FROM Specialtys;";
//             MySqlDataReader rdr = cmd.ExecuteReader () as MySqlDataReader;
//             while (rdr.Read ()) {
//                 int SpecialtyId = rdr.GetInt32 (0);
//                 string SpecialtyName = rdr.GetString (1);
//                 Specialty newSpecialty = new Specialty (SpecialtyName, SpecialtyId);
//                 allSpecialtys.Add (newSpecialty);
//             }
//             conn.Close ();
//             if (conn != null) {
//                 conn.Dispose ();
//             }
//             return allSpecialtys;
//         }

//         public override bool Equals (System.Object otherSpecialty) {
//             if (!(otherSpecialty is Specialty)) {
//                 return false;
//             } else {
//                 Specialty newSpecialty = (Specialty) otherSpecialty;
//                 bool idEquality = (this.GetId () == newSpecialty.GetId ());
//                 bool nameEquality = (this.GetName () == newSpecialty.GetName ());
//                 return (idEquality && nameEquality);
//             }
//         }

//         public override int GetHashCode () {
//             return this.GetId ().GetHashCode ();
//         }

//         public void Save () {
//             MySqlConnection conn = DB.Connection ();
//             conn.Open ();

//             var cmd = conn.CreateCommand () as MySqlCommand;
//             cmd.CommandText = @"INSERT INTO Specialtys (name) VALUES (@SpecialtyName);";

//             cmd.Parameters.Add (new MySqlParameter ("@SpecialtyName", _name));

//             cmd.ExecuteNonQuery ();
//             _id = (int) cmd.LastInsertedId;

//             conn.Close ();
//             if (conn != null) {
//                 conn.Dispose ();
//             }
//         }

//         public static Specialty Find (int id) {
//             MySqlConnection conn = DB.Connection ();
//             conn.Open ();
//             var cmd = conn.CreateCommand () as MySqlCommand;
//             cmd.CommandText = @"SELECT * FROM Specialtys WHERE id = (@SearchId);";

//             cmd.Parameters.Add (new MySqlParameter ("@SearchId", id));

//             var rdr = cmd.ExecuteReader () as MySqlDataReader;
//             int SpecialtyId = 0;
//             string SpecialtyName = "";

//             while (rdr.Read ()) {
//                 SpecialtyId = rdr.GetInt32 (0);
//                 SpecialtyName = rdr.GetString (1);
//             }
//             Specialty newSpecialty = new Specialty (SpecialtyName, SpecialtyId);
//             conn.Close ();
//             if (conn != null) {
//                 conn.Dispose ();
//             }
//             return newSpecialty;
//         }

//         public static void DeleteSpecialty (int id) {
//             MySqlConnection conn = DB.Connection ();
//             conn.Open ();
//             var cmd = conn.CreateCommand () as MySqlCommand;
//             cmd.CommandText = @"DELETE FROM Specialtys WHERE id = @Id;";

//             cmd.Parameters.Add (new MySqlParameter ("@Id", id));

//             cmd.ExecuteNonQuery ();
//             conn.Close ();
//             if (conn != null) {
//                 conn.Dispose ();
//             }
//         }

//         public static void DeleteAll () {
//             MySqlConnection conn = DB.Connection ();
//             conn.Open ();

//             var cmd = conn.CreateCommand () as MySqlCommand;
//             cmd.CommandText = @"DELETE FROM Specialtys;";

//             cmd.ExecuteNonQuery ();

//             conn.Close ();
//             if (conn != null) {
//                 conn.Dispose ();
//             }
//         }
//     }
// }