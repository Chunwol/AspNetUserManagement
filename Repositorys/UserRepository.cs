using AspNetUserManagement.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;


namespace AspNetUserManagement.Repositorys
{
    public class UserRepository
    {
        private SqlConnection con;

        public UserRepository()
        {
            con = new SqlConnection();
            con.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public void AddUser(string userID, string name, string password, byte[] passwordSalt)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "AddUser";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_ID", userID);
            cmd.Parameters.AddWithValue("@USER_PW", password);
            cmd.Parameters.AddWithValue("@USER_SALT", Convert.ToBase64String(passwordSalt));
            cmd.Parameters.AddWithValue("@USER_NM", name);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public UserModel GetUserByUserId(string userId)
        {
            UserModel r = new UserModel();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetUserByUserID";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_ID", userId);

            con.Open();
            IDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                r.Pk = dr.GetGuid(0);
                r.Id = dr.GetString(1);
                r.Password = dr.GetString(2);
                r.PasswordSalt = dr.GetString(3);
                r.Name = dr.GetString(4);
            }
            con.Close();

            return r;
        }

        public UserModel GetUserByUserPk(Guid userPk)
        {
            UserModel r = new UserModel();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetUserByUserPK";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_PK", userPk);

            con.Open();
            IDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                r.Pk = dr.GetGuid(0);
                r.Id = dr.GetString(1);
                r.Password = dr.GetString(2);
                r.PasswordSalt = dr.GetString(3);
                r.Name = dr.GetString(4);
            }
            con.Close();

            return r;
        }
    }
}