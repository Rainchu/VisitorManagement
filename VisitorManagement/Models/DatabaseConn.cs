using Microsoft.Data.SqlClient;
using System.Data;

namespace VisitorManagement.Models
{
    public class DatabaseConn
    {


        public static DataTable ExecuteProcedure(string ProcedureName, string[,] Param)
        {
            DataTable dt = new DataTable();
            string connectionString = @"Data Source=DESKTOP-72AQOLK\SQLEXPRESS01;Initial Catalog=VISITOR;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(ProcedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    for (int i = 0; i < Param.Length / 2; i++)
                    {
                        cmd.Parameters.AddWithValue(Param[i, 0], Param[i, 1]);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            con.Open();
                            da.Fill(dt);
                        }
                        catch (SqlException sqlEx)
                        {
                            Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                            // Log exception or handle accordingly
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"General Exception: {ex.Message}");
                            // Log exception or handle accordingly
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                        }
                    }
                }
            }
            return dt;
        }



        public static DataTable ExecuteProcedure(string ProcedureName)
        {
            DataTable dt = new DataTable();
            string connectionString = @"Data Source=DESKTOP-72AQOLK\SQLEXPRESS01;Initial Catalog=VISITOR;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        conn.Open();
                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                // Log exception or handle accordingly
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                // Log exception or handle accordingly
            }

            return dt;
        }





    }
}
