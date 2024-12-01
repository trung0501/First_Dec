using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLThuvien
{
    class Database
    {
        private SqlConnection sqlConn; // Đối tượng kết nối CSDL
        private SqlDataAdapter da; // Bộ điều phối dữ liệu
        private DataSet ds; // Đối tượng chứa CSDL khi giao tiếp

        public Database()
        {
            string strCnn = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""{Application.StartupPath}\QLThuvienlab10.mdf"";Integrated Security=True;Connect Timeout=30";
            sqlConn = new SqlConnection(strCnn);
        }

        // Phương thức để thực hiện câu lệnh strSQL truy vấn dữ liệu
        public DataTable Execute(string sqlStr)
        {
            da = new SqlDataAdapter(sqlStr, sqlConn);
            ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        // Phương thức để thực hiện các lệnh Thêm, Xóa, Sửa
        public void ExecuteNonQuery(string strSQL)
        {
            SqlCommand sqlcmd = new SqlCommand(strSQL, sqlConn);
            try
            {
                sqlConn.Open(); // Mở kết nối
                sqlcmd.ExecuteNonQuery(); // Thực hiện lệnh Thêm / Xóa / Sửa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                sqlConn.Close(); // Đóng kết nối
            }
        }
    }
}
