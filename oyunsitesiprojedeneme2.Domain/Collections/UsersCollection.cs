using ooyunsitesiprojedeneme2.domain.Collections.Interfaces;
using ooyunsitesiprojedeneme2.Domain;
using oyunsitesiprojedeneme2.Domain.Models;
using System.Data;
using System.Data.SqlClient;

namespace oyunsitesiprojedeneme2.Domain.Collections
{
    public class UsersCollection : IRepository
    {
   

        public void Add(UsersDataModel data)
        {
            // Bu alttaki iki kodun neden burada yer aldığını bilmiyorum ancak buraya koyduysam bir amacı vardır heralde diye silmedim.
            //List<UsersDataModel> _UsersDatas = new List<UsersDataModel>();
            //_UsersDatas.Add(entity);
            //----------------------------------------------
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
            //----------------------------------------------
            //var sqlControl = new SqlConnectionsChangeData();
            //sqlControl.adding(entity);
        }

        public void usersRemove(UsersDataModel data) {

            using (SqlConnection connect = new SqlConnection(SqlConnectionsClass.connectionString))
            { 
            connect.Open();
                using(SqlCommand command = new SqlCommand())
                {
                    command.Connection = connect;
                    command.CommandText = "DELETE FROM UsersDatas1 WHERE UsersName = @usersname AND Email = @email;";
                    command.Parameters.AddWithValue("@usersname", data.UserName);
                    command.Parameters.AddWithValue("@email", data.Email);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Veri başarıyla silindi");
                    }
                    else 
                    {
                        Console.WriteLine("Veri silinirken bir hata olştu");
                    }
                }
            }

        }

        public bool Login(UsersDataModel data)
        {
            bool isLoginSuccessful = false;
            using (SqlConnection connection = new SqlConnection(SqlConnectionsClass.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM  UsersDatas1 WHERE UsersName = @Name AND Password = @Password";
                    command.Parameters.AddWithValue("@Name", data.UserName);
                    command.Parameters.AddWithValue("@Password", data.Password);
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
       
    }
}
