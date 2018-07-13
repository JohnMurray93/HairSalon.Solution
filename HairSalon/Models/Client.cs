using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models {

    public class Client {

        private int _id;
        private string _name;
        private int _stylistId;

        public Client (string name, int stylistId, int id = 0) {

            _id = id;
            _name = name;
            _stylistId = stylistId;
        }

        public string GetName () {
            return _name;
        }
        public int GetId () {
            return _id;
        }
        public int GetStylistId () {
            return _stylistId;
        }

        public override bool Equals (System.Object otherClient) {
            if (!(otherClient is Client)) {
                return false;
            } else {
                Client newClient = (Client) otherClient;
                bool idEquality = this.GetId () == newClient.GetId ();
                bool nameEquality = this.GetName () == newClient.GetName ();
                bool stylistEquality = this.GetStylistId () == newClient.GetStylistId ();
                return (idEquality && nameEquality && stylistEquality);
            }
        }

        public override int GetHashCode () {
            return this.GetName ().GetHashCode ();
        }

        public void Save () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@Name, @StylistId);";

            cmd.Parameters.Add (new MySqlParameter ("@Name", _name));
            cmd.Parameters.Add (new MySqlParameter ("@StylistId", _stylistId));

            cmd.ExecuteNonQuery ();
            _id = (int) cmd.LastInsertedId;
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public static Client Find (int id) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = (@SearchId);";

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", id));

            var rdr = cmd.ExecuteReader () as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            int clientstylistId = 0;

            while (rdr.Read ()) {
                clientId = rdr.GetInt32 (0);
                clientName = rdr.GetString (1);
                clientstylistId = rdr.GetInt32 (2);
            }
            Client newClient = new Client (clientName, clientstylistId, clientId);
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
            return newClient;
        }

        public static List<Client> GetAll () {
            List<Client> allClients = new List<Client> { };
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients ORDER BY name;";
            var rdr = cmd.ExecuteReader () as MySqlDataReader;
            while (rdr.Read ()) {
                int clientId = rdr.GetInt32 (0);
                string clientname = rdr.GetString (1);
                int clientstylistId = rdr.GetInt32 (2);
                Client newClient = new Client (clientname, clientstylistId, clientId);
                allClients.Add (newClient);
            }
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
            return allClients;
        }

        public void UpdateClient (string newClientName) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET name = @NewClientName WHERE id = @SearchId;";

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", _id));
            cmd.Parameters.Add (new MySqlParameter ("@NewClientName", newClientName));

            cmd.ExecuteNonQuery ();
            _name = newClientName;

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public static void DeleteClient (int id) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id = @Id;";

            cmd.Parameters.Add (new MySqlParameter ("@Id", id));

            cmd.ExecuteNonQuery ();
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public static void DeleteClients (int id) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = @Id;";

            cmd.Parameters.Add (new MySqlParameter ("@Id", id));

            cmd.ExecuteNonQuery ();
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public static void DeleteStylistClients (int id) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = @StylistId;";

            cmd.Parameters.Add (new MySqlParameter ("@StylistId", id));

            cmd.ExecuteNonQuery ();
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public static void DeleteAll () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery ();
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }
    }
}