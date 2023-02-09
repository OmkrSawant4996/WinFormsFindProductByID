using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public class ProductDataStore
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        public ProductDataStore(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public List<int> GetAllProductId()
        {
            try
            {
                string allEmpId = "select Ps.ProductID from products as Ps order by Ps.ProductID asc";
                command = new SqlCommand(allEmpId, connection);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                reader = command.ExecuteReader();
                List<int> productsId = new List<int>();

                while (reader.Read())
                {
                    int productId = (int)reader["ProductID"];
                    productsId.Add(productId);
                }

                return productsId;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public Products GetProduct(int productID)
        {
            try
            {
                string getProductById = $"select Ps.productName, Ps.UnitPrice from products as Ps where Ps.ProductId = @productID";
                command = new SqlCommand(getProductById, connection);
                command.Parameters.AddWithValue("@productID", productID);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Products products = new Products();
                    products.ProductName = reader["productName"].ToString();
                    products.UnitPrice = reader["UnitPrice"] as decimal?;

                    return products;
                }
                return null;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
