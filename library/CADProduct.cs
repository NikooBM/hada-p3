using System;
using System.Configuration;
using System.Data.SqlClient;

namespace library
{
    public class CADProduct
    {
        // ── Cadena de conexión (se lee de Web.config del proyecto proWeb) ─
        private string constring;

        public CADProduct()
        {
            constring = ConfigurationManager
                            .ConnectionStrings["DefaultConnection"]
                            .ConnectionString;
        }

        // ── Helper: rellena un ENProduct a partir de un SqlDataReader ────
        private void FillProduct(ENProduct en, SqlDataReader r)
        {
            en.Code = r["code"].ToString();
            en.Name = r["name"].ToString();
            en.Amount = (int)r["amount"];
            en.Price = (float)(double)r["price"]; // SQL FLOAT → double → float
            en.Category = (int)r["category"];
            en.CreationDate = (DateTime)r["creationDate"];
        }

        // ── CREATE ───────────────────────────────────────────────────────
        public bool Create(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    string sql = @"INSERT INTO Products
                                       (code, name, amount, price, category, creationDate)
                                   VALUES
                                       (@code, @name, @amount, @price, @category, @creationDate)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    cmd.Parameters.AddWithValue("@name", en.Name);
                    cmd.Parameters.AddWithValue("@amount", en.Amount);
                    cmd.Parameters.AddWithValue("@price", en.Price);
                    cmd.Parameters.AddWithValue("@category", en.Category);
                    cmd.Parameters.AddWithValue("@creationDate", en.CreationDate);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // ── UPDATE ───────────────────────────────────────────────────────
        public bool Update(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    string sql = @"UPDATE Products
                                   SET name         = @name,
                                       amount       = @amount,
                                       price        = @price,
                                       category     = @category,
                                       creationDate = @creationDate
                                   WHERE code = @code";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    cmd.Parameters.AddWithValue("@name", en.Name);
                    cmd.Parameters.AddWithValue("@amount", en.Amount);
                    cmd.Parameters.AddWithValue("@price", en.Price);
                    cmd.Parameters.AddWithValue("@category", en.Category);
                    cmd.Parameters.AddWithValue("@creationDate", en.CreationDate);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // ── DELETE ───────────────────────────────────────────────────────
        public bool Delete(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    string sql = "DELETE FROM Products WHERE code = @code";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                return false;
            }
        }

        // ── READ (por code) ──────────────────────────────────────────────
        public bool Read(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    string sql = @"SELECT code, name, amount, price, category, creationDate
                                   FROM Products
                                   WHERE code = @code";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        FillProduct(en, reader);
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

        // ── READ FIRST ───────────────────────────────────────────────────
        public bool ReadFirst(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    string sql = @"SELECT TOP 1 code, name, amount, price, category, creationDate
                                   FROM Products
                                   ORDER BY code ASC";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        FillProduct(en, reader);
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

        // ── READ NEXT ────────────────────────────────────────────────────
        public bool ReadNext(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    string sql = @"SELECT TOP 1 code, name, amount, price, category, creationDate
                                   FROM Products
                                   WHERE code > @code
                                   ORDER BY code ASC";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        FillProduct(en, reader);
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

        // ── READ PREV ────────────────────────────────────────────────────
        public bool ReadPrev(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    string sql = @"SELECT TOP 1 code, name, amount, price, category, creationDate
                                   FROM Products
                                   WHERE code < @code
                                   ORDER BY code DESC";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        FillProduct(en, reader);
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
    }
}