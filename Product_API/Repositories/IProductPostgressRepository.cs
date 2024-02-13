using Npgsql;
using Product_API.Models;

namespace Product_API.Repositories
{
    public class ProductPostgressRepository : IProductRepository
    {
        const string CONNECTIONSTRING = "Host=localhost; Port=5432; Database=Market; User Id=postgres; Password=135;";
        string query = "";

        public Product Add(Product product)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                connection.Open();

                try
                {
                    string query = $"Insert into products(Name, Description, PhotoPath) values ('{product.Name}','{product.Description}','{product.PhotoPath}');";
                    using NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    var reader = command.ExecuteNonQuery();
                }
                catch
                {
                    Console.WriteLine("404 Not Found!");
                }
            }

            return product;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                connection.Open();

                try
                {
                    Product product = new Product();
                    string query = "Select * from product;";
                    using NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        product.Name = reader[0].ToString()!;
                        product.Description = reader[1].ToString()!;
                        product.PhotoPath = reader[2].ToString()!;
                        products.Add(product);
                    }
                }
                catch
                {
                    Console.WriteLine("404 Not Found!");
                }

                return products;
            }
        }

    }
}
