using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace nLogin
{
    class Modify
    {
        public Modify()
        {

        }
        SqlCommand sqlCommand;
        SqlDataReader datareader;
        public List<Taikhoan> Taikhoans(string query) // check tai khoan
        {
            List<Taikhoan> taikhoans = new List<Taikhoan>();
            using (SqlConnection sqlConnection = connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query,sqlConnection);
                datareader = sqlCommand.ExecuteReader();
                while(datareader.Read())
                {
                    taikhoans.Add(new Taikhoan(datareader.GetString(0), datareader.GetString(1)));
                }
                sqlConnection.Close();
            }
            return taikhoans;
        }
        public void Command(string query) // dung de dang ky tai khoan
        {
            using (SqlConnection sqlConnection = connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query,sqlConnection);
                sqlCommand.ExecuteNonQuery();// thuc thi cau try van
                sqlConnection.Close();
            }
        }
    }
}
