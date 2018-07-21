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

        public List<Client> GetClients()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT client_id FROM clients_stylists WHERE stylist_id = @StylistId;";

            cmd.Parameters.Add (new MySqlParameter ("@StylistId", _id));

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<int> clientIds = new List<int> {};
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                clientIds.Add(clientId);
            }
            rdr.Dispose();

            List<Client> clients = new List<Client> {};
            foreach (int clientId in clientIds)
            {
                var clientQuery = conn.CreateCommand() as MySqlCommand;
                clientQuery.CommandText = @"SELECT * FROM clients WHERE id = @ClientId;";

                MySqlParameter clientIdParameter = new MySqlParameter();
                clientIdParameter.ParameterName = "@ClientId";
                clientIdParameter.Value = clientId;
                clientQuery.Parameters.Add(clientIdParameter);

                var clientQueryRdr = clientQuery.ExecuteReader() as MySqlDataReader;
                while(clientQueryRdr.Read())
                {
                    int thisClientId = clientQueryRdr.GetInt32(0);
                    string clientDescription = clientQueryRdr.GetString(1);
                    Client foundClient = new Client(clientDescription, thisClientId);
                    clients.Add(foundClient);
                }
                clientQueryRdr.Dispose();
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return clients;
        }

    public void AddClient(Client newClient)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients_stylists (stylist_id, client_id) VALUES (@StylistId, @ClientId);";

            cmd.Parameters.Add (new MySqlParameter ("@StylistId", _id));

            MySqlParameter client_id = new MySqlParameter();
            client_id.ParameterName = "@ClientId";
            client_id.Value = newClient.GetId();
            cmd.Parameters.Add(client_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
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

        public static Stylist Find (int id) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@SearchId);";

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", id));

            var rdr = cmd.ExecuteReader () as MySqlDataReader;
            int StylistId = 0;
            string StylistName = "";

            while (rdr.Read ()) {
                StylistId = rdr.GetInt32 (0);
                StylistName = rdr.GetString (1);
            }
            Stylist newStylist = new Stylist (StylistName, StylistId);
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
            return newStylist;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("DELETE FROM stylists WHERE id = @StylistId; DELETE FROM clients_stylists WHERE stylist_id = @StylistId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@StylistId", _id));

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
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
        public void DeleteStylistClients()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT client_id FROM clients_stylists WHERE stylist_id = @StylistId; DELETE FROM clients_stylists WHERE stylist_id = @StylistId";

            cmd.Parameters.Add (new MySqlParameter ("@StylistId", this._id));

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<int> clientIds = new List<int> {};
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                clientIds.Add(clientId);
            }
            rdr.Dispose();

            List<Client> clients = new List<Client> {};
            foreach (int clientId in clientIds)
            {
                var clientQuery = conn.CreateCommand() as MySqlCommand;
                clientQuery.CommandText = @"DELETE FROM clients WHERE id = @ClientId;";

                MySqlParameter clientIdParameter = new MySqlParameter();
                clientIdParameter.ParameterName = "@ClientId";
                clientIdParameter.Value = clientId;
                clientQuery.Parameters.Add(clientIdParameter);

                var clientQueryRdr = clientQuery.ExecuteReader() as MySqlDataReader;
                while(clientQueryRdr.Read())
                {
                    int thisClientId = clientQueryRdr.GetInt32(0);
                    string clientDescription = clientQueryRdr.GetString(1);
                    Client foundClient = new Client(clientDescription, thisClientId);
                    clients.Add(foundClient);
                }
                clientQueryRdr.Dispose();
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
    }
}