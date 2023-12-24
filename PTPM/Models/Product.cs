using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace PTPM.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ShortDesc { get; set; }

    public string? Desciption { get; set; }

    public int? Catid { get; set; }

    public int? Price { get; set; }

    public int? Discount { get; set; }

    public string? Thumb { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public bool BestSellers { get; set; }

    public bool HomeFlag { get; set; }

    public bool Active { get; set; }

    public string? Tags { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public string? MetaDesc { get; set; }

    public string? MetaKey { get; set; }

    public int? UnitslnStock { get; set; }

    public virtual Category? Cat { get; set; }
    public Product()
    { }
    public Product(int id)
    {
        Read(id);
    }
    public void Read(int ID)
    {
        string URL = "Data Source=DESKTOP-QRII27H;Initial Catalog=PTPM;Trusted_Connection=true;TrustServerCertificate=Yes;";
        SqlConnection connection = new SqlConnection(URL);
        connection.Open();
        try
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select * from Products where ProductID =" + ID.ToString();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ProductId = reader.GetInt32(0);
                ProductName = reader.GetString(1);
                ShortDesc = reader.GetString(2);
                Desciption = reader.GetString(3);
                Catid = reader.GetInt32(4);
                Price = reader.GetInt32(5);
                Discount = reader.GetInt32(6);
                Thumb = reader.GetString(7);
                DateCreated = reader.GetDateTime(8);
                DateModified = reader.GetDateTime(9);
                BestSellers = reader.GetBoolean(10);
                HomeFlag = reader.GetBoolean(11);
                Active = reader.GetBoolean(12);
                Tags = reader.GetString(13);
                Title = reader.GetString(14);
                Alias = reader.GetString(15);
                MetaDesc = reader.GetString(16);
            }
        }
        catch (Exception ex) { throw; }
    }
}
