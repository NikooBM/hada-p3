using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace library
{
    public class CADCategory
    {
        // ── Cadena de conexión ──────────────────────────────────────────
        private string constring;

        public CADCategory()
        {
            constring = ConfigurationManager
                            .ConnectionStrings["DefaultConnection"]
                            .ConnectionString;
        }

        // ── READ (por id) ────────────────────────────────────────────────
        public bool Read(ENCategory en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    string sql = "SELECT id, name FROM Categories WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", en.Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        en.Id = (int)reader["id"];
                        en.Name = reader["name"].ToString();
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // ── READ ALL ─────────────────────────────────────────────────────
        public List<ENCategory> ReadAll()
        {
            List<ENCategory> list = new List<ENCategory>();
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    string sql = "SELECT id, name FROM Categories ORDER BY id ASC";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new ENCategory(
                            (int)reader["id"],
                            reader["name"].ToString()
                        ));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
            return list;
        }
    }
}