using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalonList.objects
{
  public class Client
  {
    private int _id;
    private string _client;
    private int _stylistId;

    public Client(string Client, int StylistId, int Id = 0)
    {
      _id = Id;
      _client = Client;
      _stylistId = StylistId;
    }

    public int GetId()
    {
      return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }
    public string GetClient()
    {
      return _client;
    }
    public void SetClient(string newClient)
    {
      _client = newClient;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public void SetStylistId(int newStylistId)
    {
      _stylistId = newStylistId;
    }

    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();
        bool clientEquality = this.GetClient() == newClient.GetClient();
        bool stylistIdEquality = this.GetStylistId() == newClient.GetStylistId();
        return (idEquality && clientEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (client_name, stylist_id) OUTPUT INSERTED.id VALUES (@Client, @Stylist);", conn);

      SqlParameter clientParameter = new SqlParameter();
      clientParameter.ParameterName = "@Client";
      clientParameter.Value = this.GetClient();
      cmd.Parameters.Add(clientParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@Stylist";
      stylistIdParameter.Value = this.GetStylistId();
      cmd.Parameters.Add(stylistIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Client> GetAll()
    {
      List<Client> AllClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string client = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);

        Client newClient = new Client(client, stylistId, clientId);
        AllClients.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllClients;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = id.ToString();
      cmd.Parameters.Add(clientIdParameter);
      rdr = cmd.ExecuteReader();

      int foundClientId = 0;
      string foundClientName = null;
      int foundStylistId = 0;

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        foundStylistId = rdr.GetInt32(2);
      }
      Client foundClient = new Client(foundClientName, foundStylistId, foundClientId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundClient;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET client_name = @NewName OUTPUT INSERTED.client_name WHERE id = @ClientId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.GetId();
      cmd.Parameters.Add(clientIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._client = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE client_id = @ClientId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.GetId();

      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
