using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfToDb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public Service1()
        {
            ConnectToDb();
        }

        SqlConnection conn;
        SqlCommand comm;

        SqlConnectionStringBuilder connStringBuilder;

        void ConnectToDb()
        {
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = "DESKTOP-AF9HFTM\\SQLEXPRESS";
            connStringBuilder.InitialCatalog = "WCF";
            connStringBuilder.Encrypt = true;
            connStringBuilder.TrustServerCertificate = true;
            connStringBuilder.ConnectTimeout = 30;
            connStringBuilder.AsynchronousProcessing = true;
            connStringBuilder.MultipleActiveResultSets = true;
            connStringBuilder.IntegratedSecurity = true;

            conn = new SqlConnection(connStringBuilder.ToString());
            comm = conn.CreateCommand();

        } 


        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int InsertUser(User u)
        {
            try
            {
                comm.CommandText = "INSERT INTO TUser VALUES(@Id, @Name, @Age)";
                comm.Parameters.AddWithValue("Id", u.Id);
                comm.Parameters.AddWithValue("Name", u.Name);
                comm.Parameters.AddWithValue("Age", u.Age);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn!=null)
                {
                    conn.Close();
                }
            }
        }

        public int UpdateUser(User u)
        {
            try
            {
                comm.CommandText = "UPDATE TUser SET Name=@Name, Age=@Age WHERE Id=@Id";
                comm.Parameters.AddWithValue("Id", u.Id);
                comm.Parameters.AddWithValue("Name", u.Name);
                comm.Parameters.AddWithValue("Age", u.Age);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public int DeleteUser(User u)
        {
            try
            {
                comm.CommandText = "DELETE TUser WHERE Id=@Id";
                comm.Parameters.AddWithValue("Id", u.Id);
                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }        

        public User GetUser(User u)
        {
            User user = new User();
            try
            {
                comm.CommandText = "SELECT * FROM TUser WHERE Id = @Id";
                comm.Parameters.AddWithValue("Id", u.Id);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    user.Id = Convert.ToInt32(reader[0]);
                    user.Name = reader[1].ToString();
                    user.Age = Convert.ToInt32(reader[2]);

                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<User> GetAllUsers()
        {            
            List<User> usersL = new List<User>();

            try
            {
                comm.CommandText = "SELECT * FROM TUser";
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User()
                    {
                        Id = Convert.ToInt32(reader[0]),
                        Name = reader[1].ToString(),
                        Age = Convert.ToInt32(reader[2])
                    };
                    usersL.Add(user);
                }

                return usersL;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
