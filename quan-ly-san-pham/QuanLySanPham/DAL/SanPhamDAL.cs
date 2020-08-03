using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using QuanLySanPham.DTO;

namespace QuanLySanPham.DAL
{
    class SanPhamDAL
    {
        public String str; // chuỗi kết nối
        public MySqlConnection conn; // thiết lập kết nối tới nguồn dữ liệu
        public MySqlCommand cmd; // thực thi các câu lệnh truy vấn với nguồn dữ liệu từ một Connection
        public MySqlDataAdapter da = new MySqlDataAdapter(); // đặt dữ liệu vào DataSet và cập nhật dữ liệu từ DataSet về nguồn dữ liệu
        /*
            Ví dụ : 
                có 1 bể nước (DataSource),
                có 1 cái máy bơm (DataAdapter),
                và 1 cái thùng đựng nước (DataSet)
                => khi lấy nước dùng cái bơm lấy nước từ bể, kiểm tra và lọc nước sau đó lại dùng cái bơm hút lại về cái bể nước
                => Vai trò MySqlDataAdapter tương tự như cái máy bơm nói trên.
        */
        // khởi tạo
        public SanPhamDAL()
        {
            str = "datasource=127.0.0.1; username=root; password=; database=db_qlsp";
            /*
             datasource : dùng để gán địa chỉ mạng (tên máy hoặc IP hoặc domain) của server
             username: tài khoản kết nối server
             password: mật khẩu kết nối server
             database: tên cơ sở dữ liệu
             */
            conn = new MySqlConnection(str); // khởi tạo kết nối
            conn.Open(); // mở kết nối
        }
        // lấy sản phẩm từ csdl về bảng
        public DataTable getSanPham()
        {
            string sql = "select * from tb_sp";
            cmd = new MySqlCommand(sql, conn);
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        // hàm thêm
        public bool themSP(SanPham sp)
        {
            // câu truy vấn
            string sql = "insert into tb_sp(ma,ten,soluong,dongia) values (@ma,@ten,@soluong,@dongia)";
            cmd = new MySqlCommand(sql, conn); // thự thi câu truy vấn
            cmd.Parameters.Add("ma", MySqlDbType.VarChar).Value = sp.ma; // thiết lậo tham số
            cmd.Parameters.Add("ten", MySqlDbType.VarChar).Value = sp.ten;
            cmd.Parameters.Add("soluong", MySqlDbType.VarChar).Value = sp.soluong;
            cmd.Parameters.Add("dongia", MySqlDbType.VarChar).Value = sp.dongia;
            cmd.ExecuteNonQuery();  // thực thi câu lệnh truy vấn không cần trả về dữ liệu gì
            return true;
        }
        // hàm sửa
        public bool suaSP(SanPham sp)
        {
            string sql = "update tb_sp set ten=@ten, soluong=@soluong, dongia=@dongia where ma=@ma";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("ma", MySqlDbType.VarChar).Value = sp.ma;
            cmd.Parameters.Add("ten", MySqlDbType.VarChar).Value = sp.ten;
            cmd.Parameters.Add("soluong", MySqlDbType.VarChar).Value = sp.soluong;
            cmd.Parameters.Add("dongia", MySqlDbType.VarChar).Value = sp.dongia;
            cmd.ExecuteNonQuery();
            return true;
        }
        // hàm xoá
        public bool xoaSP(string ma)
        {
            string sql = "delete from tb_sp where ma = @ma";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("ma", MySqlDbType.VarChar).Value = ma;
            cmd.ExecuteNonQuery();
            return true;
        }
        // hàm tìm kiếm
        public DataTable timkiem(string sp)
        {
            string sql = "select * from tb_sp where ma like '%" + sp + "%' or ten like '%" + sp + "%' or soluong like '%" + sp + "%' or dongia like '%" + sp + "%'";
            cmd = new MySqlCommand(sql, conn);
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
