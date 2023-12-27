using ooyunsitesiprojedeneme2.domain.Collections.Interfaces;
using ooyunsitesiprojedeneme2.Domain;
using oyunsitesiprojedeneme2.Domain.Models;
using System.Data.SqlClient;

namespace oyunsitesiprojedeneme2.Domain.Collections
{
    public class ProductsCollection : IProductManager
    {


        public void Add(ProductsDataModel data)
        {
            //---------AŞAĞIDAKİ 2 KOD SATIRININ NE OLDUĞU BİLİNMİYOR----------------
            //List<ProductsDataModel> _productDatas = new List<ProductsDataModel>();
            //_productDatas.Add(entity);
            //-----------------------------------------------------------------------

            using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    int AddingId = 0;
                    command.Connection = connection;
                    command.CommandText = "SELECT TOP 1 productId FROM ProductsDatas1 ORDER BY productId DESC;";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Eğer bir satır okunduysa devam et
                            if (!reader.IsDBNull(0))
                            {
                                AddingId = reader.GetInt32(0) + 1;
                            }
                            else
                            {
                                // productId NULL ise veya veritabanında bu satırda productId yoksa, AddingId'e 1 ekle
                                AddingId = 1;
                            }
                        }
                        else
                        {
                            // Hiç satır yoksa, AddingId'e 1 ekle
                            AddingId = 1;
                        }
                    }

                    command.CommandText = "INSERT INTO ProductsDatas1 (productId, productName, productPrice, productReleaseDate, productDescription, productPhoto) VALUES (@Id, @Name, @Price, @ReleaseDate, @Desc, @Photo)";
                    command.Parameters.AddWithValue("@Id", AddingId);
                    command.Parameters.AddWithValue("@Name", data.productName);
                    command.Parameters.AddWithValue("@Price", data.productPrice);
                    command.Parameters.AddWithValue("@ReleaseDate", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@Desc", data.productDescription);
                    command.Parameters.AddWithValue("@Photo", data.productPhoto);


                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Veri başarıyla eklendi.");
                    }
                    else
                    {
                        Console.WriteLine("Veri eklenirken hata oluştu.");
                    }
                }
                connection.Close();
            }

            //-----------------------------------------------------------------------
            //var sqlControl = new SqlConnectionsChangeData();
            //sqlControl.productAdding(entity);
        }
        public void RemoveProduct(string removeForName)
        {

            using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
            {
                connection.Open();



                using (SqlCommand command = new SqlCommand("DELETE FROM ProductsDatas1 WHERE productName = @productName", connection))
                {
                    command.Parameters.AddWithValue("@productName", removeForName);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        Console.WriteLine("sepetten kaldırma işlemi başarılı");
                    }
                    else
                    {

                        Console.WriteLine("sepetten kaldırma işlemi başarısız");
                    }
                }
                connection.Close();
            }
        }
        public bool productControl(ProductsDataModel data)
        {
            bool isLoginSucessful = false;
            using (SqlConnection connect = new SqlConnection(SqlConnectionsClass.connectionString))
            {
                connect.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connect;
                    command.CommandText = "SELECT * FROM ProductsDatas1 WHERE productName = @Name";
                    command.Parameters.AddWithValue("@Name", data.productName);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    { isLoginSucessful = true; }
                    else
                    { isLoginSucessful = false; }
                }
                connect.Close();
            }
            return isLoginSucessful;
        }
        public List<BucketDataModel> GetBuckets(int pageNumber, List<int> cartIds)
        {
            List<BucketDataModel> products = new List<BucketDataModel>();

            using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
            {
                int endId = pageNumber * 5;
                int startId = endId - 5;
                connection.Open();
                foreach (var item in cartIds)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT productId, productName, productPrice, productReleaseDate, productPhoto, productDescription FROM ProductsDatas1 WHERE productId = @productId", connection))
                    {

                        cmd.Parameters.AddWithValue("@productId", item);
                        using (SqlDataReader oku = cmd.ExecuteReader())
                        {

                            while (oku.Read())
                            {

                                BucketDataModel productA = new BucketDataModel()
                                {
                                    bucketId = int.Parse(oku["productId"].ToString()),
                                    bucketName = oku["productName"].ToString(),
                                    bucketPrice = oku["productPrice"].ToString(),
                                    bucketPhoto = oku["productPhoto"].ToString(),
                                };
                                products.Add(productA);
                            }

                        }
                    }

                }
                connection.Close();

            }
            return products;

        }
        public List<ProductsDataModel> GetProductsFromDatabase(string pageNumber)
        {
            if (pageNumber != "adminPagePro")
            {
                List<ProductsDataModel> products = new List<ProductsDataModel>();

                using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
                {
                    int pageIndex = (int.Parse(pageNumber) - 1) * 5;
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductsDatas1 ORDER BY productName OFFSET @pageIndex ROWS FETCH NEXT 5 ROWS ONLY", connection))
                    {

                        cmd.Parameters.AddWithValue("@pageIndex", pageIndex);
                        using (SqlDataReader oku = cmd.ExecuteReader())
                        {

                            while (oku.Read())
                            {

                                ProductsDataModel productA = new ProductsDataModel()
                                {
                                    productId = int.Parse(oku["productId"].ToString()),
                                    productName = oku["productName"].ToString(),
                                    productPrice = oku["productPrice"].ToString(),
                                    productReleaseDate = oku["productReleaseDate"].ToString(),
                                    productPhoto = oku["productPhoto"].ToString(),
                                    productDescription = oku["productDescription"].ToString(),

                                };
                                products.Add(productA);
                            }

                        }

                    }
                    connection.Close();

                }
                return products;
            }

            else
            {
                List<ProductsDataModel> products = new List<ProductsDataModel>();

                using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductsDatas1", connection))
                    {
                        using (SqlDataReader oku = cmd.ExecuteReader())
                        {

                            while (oku.Read())
                            {

                                ProductsDataModel productA = new ProductsDataModel()
                                {
                                    productId = int.Parse(oku["productId"].ToString()),
                                    productName = oku["productName"].ToString(),
                                    productPrice = oku["productPrice"].ToString(),
                                    productReleaseDate = oku["productReleaseDate"].ToString(),
                                    productPhoto = oku["productPhoto"].ToString(),
                                    productDescription = oku["productDescription"].ToString(),
                                };
                                products.Add(productA);
                            }

                        }

                    }
                    connection.Close();

                }
                return products;
            }

        }
    }
}
