using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models {

    public class Client {

        private int _id;
        private string _name;

        public Client (string name, int id = 0) {

            _id = id;
            _name = name;
        }

        public string GetName () {
            return _name;
        }
        public int GetId () {
            return _id;
        }

        public override bool Equals (System.Object otherClient) {
            if (!(otherClient is Client)) {
                return false;
            } else {
                Client newClient = (Client) otherClient;
                bool idEquality = this.GetId () == newClient.GetId ();
                bool nameEquality = this.GetName () == newClient.GetName ();
                return (idEquality && nameEquality);
            }
        }

        public override int GetHashCode () {
            return this.GetName ().GetHashCode ();
        }

        public void Save () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"INSERT INTO clients (name) VALUES (@Name);", conn);

            cmd.Parameters.Add (new MySqlParameter ("@Name", _name));

            cmd.ExecuteNonQuery ();
            _id = (int) cmd.LastInsertedId;
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            
            MySqlCommand cmd = new MySqlCommand(@"INSERT INTO clients_stylists (stylist_id, client_id) VALUES (@StylistId, @ClientId);", conn);

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@StylistId";
            stylist_id.Value = newStylist.GetId();
            cmd.Parameters.Add(stylist_id);

            cmd.Parameters.Add (new MySqlParameter ("@ClientId", _id));

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(@"SELECT stylist_id FROM clients_stylists WHERE client_id = @ClientId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@ClientId", _id));

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
                    string stylistName = stylistQueryRdr.GetString(1);
                    Stylist foundStylist = new Stylist(stylistName, thisStylistId);
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

        public static Client Find (int id) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM clients WHERE id = (@SearchId);", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", id));

            var rdr = cmd.ExecuteReader () as MySqlDataReader;
            int clientId = 0;
            string clientName = "";

            while (rdr.Read ()) {
                clientId = rdr.GetInt32 (0);
                clientName = rdr.GetString (1);
            }
            Client newClient = new Client (clientName, clientId);
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

            MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM clients ORDER BY name;", conn);

            var rdr = cmd.ExecuteReader () as MySqlDataReader;
            while (rdr.Read ()) {
                int clientId = rdr.GetInt32 (0);
                string clientname = rdr.GetString (1);
                Client newClient = new Client (clientname, clientId);
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
            MySqlCommand cmd = new MySqlCommand(@"UPDATE clients SET name = @NewClientName WHERE id = @SearchId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", _id));
            cmd.Parameters.Add (new MySqlParameter ("@NewClientName", newClientName));

            cmd.ExecuteNonQuery ();
            _name = newClientName;

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(@"DELETE FROM clients WHERE id = @ClientId; DELETE FROM clients_stylists WHERE client_id = @ClientId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@ClientId", _id));

            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static void DeleteAll () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"DELETE FROM clients; Delete From clients_stylists", conn);

            cmd.ExecuteNonQuery ();
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }
    }
}