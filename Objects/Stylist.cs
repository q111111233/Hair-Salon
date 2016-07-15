using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalonList.objects
{
  public class Stylist
  {
    private int _id;
    private string _stylist;

    public Stylist(string Stylist, int Id = 0)
    {
      _id = Id;
      _stylist = Stylist;
    }

    public int GetId()
    {
      return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }
    public string GetStylist()
    {
      return _stylist;
    }
    public void SetStylist(string newStylist)
    {
      _stylist = newStylist;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId() == newStylist.GetId();
        bool stylistEquality = this.GetStylist() == newStylist.GetStylist();
        return (idEquality && stylistEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name) OUTPUT INSERTED.id VALUES (@Stylist);", conn);

      SqlParameter stylistParameter = new SqlParameter();
      stylistParameter.ParameterName = "@Stylist";
      stylistParameter.Value = this.GetStylist();

      cmd.Parameters.Add(stylistParameter);

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

    public static List<Stylist> GetAll()
    {
      List<Stylist> AllStylists = new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylist = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylist, stylistId);
        AllStylists.Add(newStylist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllStylists;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = id.ToString();
      cmd.Parameters.Add(stylistIdParameter);
      rdr = cmd.ExecuteReader();

      int foundStylistId = 0;
      string foundStylistName = null;

      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
      }
      Stylist foundStylist = new Stylist(foundStylistName, foundStylistId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStylist;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName OUTPUT INSERTED.name WHERE id = @StylistId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._stylist = rdr.GetString(0);
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

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId; DELETE FROM clients WHERE stylist_id = @StylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();

      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Client> GetClients()
     {
       SqlConnection conn = DB.Connection();
       SqlDataReader rdr = null;
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @StylistId;", conn);
       SqlParameter stylistIdParameter = new SqlParameter();
       stylistIdParameter.ParameterName = "@StylistId";
       stylistIdParameter.Value = this.GetId();
       cmd.Parameters.Add(stylistIdParameter);
       rdr = cmd.ExecuteReader();

       List<Client> clients = new List<Client> {};
       while(rdr.Read())
       {
         int clientId = rdr.GetInt32(0);
         string clientName = rdr.GetString(1);
         int clientStylistId = rdr.GetInt32(2);
         Client newClient = new Client(clientName, clientStylistId, clientId);
         clients.Add(newClient);
       }
       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }
       return clients;
     }
  }
}
