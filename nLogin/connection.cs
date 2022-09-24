using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace nLogin
{
    internal class connection
    {
        //các bạn cần cài dặt  SQL server để có thể truy xuất và lưu dữ liệu.
        private static string strconection = @"Data Source=DESKTOP-G61KFSS;Initial Catalog=Login;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(strconection);
        }
    }
}
