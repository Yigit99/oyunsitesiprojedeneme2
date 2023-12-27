using oyunsitesiprojedeneme2.Domain.Models;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace ooyunsitesiprojedeneme2.Domain
{
    public class SqlConnectionsClass
    {
        public static string connectionString = "Data Source=YIGITCAKMAKCI;Initial Catalog=KarwaGamesDatas;Integrated Security=True";
        public static SqlConnection request = new SqlConnection(connectionString);
    }

    public class SqlConnectionsChangeData
    {

        public string getRole(string getUser, string getPassword) {

            using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                { 
                  command.Connection = connection;
                    command.CommandText = "SELECT Roles FROM UsersDatas1 WHERE UsersName = @UserName AND Password = @Password";
                    command.Parameters.AddWithValue("@Username", getUser);
                    command.Parameters.AddWithValue("@Password", getPassword);
                    string RoleAga = "";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            RoleAga = reader["Roles"].ToString();
                            Console.WriteLine("Veri başarıyla iletildi.");
                        }
                    }
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Veri başarıyla iletildi.");
                    }
                    else
                    {
                        Console.WriteLine("Veri iletilirken hata oluştu.");
                    }

                    return RoleAga;
                }
            }

        }
        //BU KOD "UsersCollection.cs" KONUMUNA TAŞINDI.
        public void adding(UsersDataModel data)
        {
            using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = connection;
                    command.CommandText = "INSERT INTO UsersDatas1 (UsersName, Email, Password, Roles) VALUES (@Name, @Email, @password, @role)";
                    command.Parameters.AddWithValue("@Name", data.UserName);
                    command.Parameters.AddWithValue("@Email", data.Email);
                    command.Parameters.AddWithValue("@password", data.Password);
                    command.Parameters.AddWithValue("@role", "admin");

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
        }

        //BU KOD "ProductsCollection.cs" KONUMUNA TAŞINDI.
        public void productAdding(ProductsDataModel data)
        {
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
                    command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now.ToString());
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
        //BU KOD "UsersCollection.cs" KONUMUNA TAŞINDI.
        public bool DataControUsersEntrance(string UsersName, string Passwords)
        {
            bool isLoginSuccessful = false;
            using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM  UsersDatas1 WHERE UsersName = @Name AND Password = @Password";
                    command.Parameters.AddWithValue("@Name", UsersName);
                    command.Parameters.AddWithValue("@Password", Passwords);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        isLoginSuccessful = true;
                    }

                    else
                    {
                        isLoginSuccessful = false;
                    }

                }
                connection.Close();

            }
            return isLoginSuccessful;
        }

        //BU KOD "ProductsCollection.cs" KONUMUNA TAŞINDI.
        public bool DataControlProduct(ProductsDataModel data) 
        { 
          bool isLoginSucessful=false;
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
                    {isLoginSucessful = true;}
                    else 
                    { isLoginSucessful = false; }
                }
                connect.Close();
            }
            return isLoginSucessful;
        }

        public void bucketAdding(List<int> AdToId)
        {
            if (AdToId != null)
            {
                BucketDataModel bucketData = new BucketDataModel();
                using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        int AddingId = 0;
                        command.Connection = connection;
                        command.CommandText = "SELECT TOP 1 bucketId FROM BucketDatas1 ORDER BY bucketId DESC;";
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
                        BucketDataModel productA = null;
                        using (SqlCommand commanproduct = new SqlCommand("SELECT * FROM ProductsDatas1 WHERE productId = @ProductId"))
                        {
                            commanproduct.Connection = connection;
                            commanproduct.Parameters.AddWithValue("@ProductId", AdToId);
                            using (SqlDataReader oku = commanproduct.ExecuteReader())
                            {

                                while (oku.Read())
                                {

                                    productA = new BucketDataModel()
                                    {
                                        bucketName = oku["productName"].ToString(),
                                        bucketPrice = oku["productPrice"].ToString(),
                                        bucketPhoto = oku["productPhoto"].ToString(),
                                    };

                                }

                            }
                        }

                        command.CommandText = "INSERT INTO BucketDatas1 (bucketId, bucketName, bucketPrice, bucketPhoto) VALUES (@BId, @BName, @BPrice, @BPhoto)";
                        command.Parameters.AddWithValue("@BId", AddingId);
                        command.Parameters.AddWithValue("@BName", productA.bucketName);
                        command.Parameters.AddWithValue("@BPrice", productA.bucketPrice);
                        command.Parameters.AddWithValue("@BPhoto", productA.bucketPhoto);


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
            }
        }

        public List<BucketDataModel> bucketGetFromDataBase(int pageNumber, List<int> cartIds)
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

        //BU KOD "ProductCollection.cs" KONUMUNA TAŞINDI.
        public void RemoveFromProducts(int removeProId)
        {
            using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
            {
                connection.Open();



                using (SqlCommand command = new SqlCommand("DELETE FROM ProductsDatas1 WHERE productId = @productId", connection))
                {
                    command.Parameters.AddWithValue("@productId", removeProId);

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
    }
}
