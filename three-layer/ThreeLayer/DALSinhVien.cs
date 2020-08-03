using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ThreeLayer
{
    class DALSinhVien
    {
        DataConnection dc;
        MySqlDataAdapter da;
        MySqlCommand cmd;
        public DALSinhVien()
        {
            dc = new DataConnection();
        }

        public DataTable getAllSinhVien()
        {
            // B1: Tạo truy vấn
            string sql = "SELECT * FROM tb_3layer";
            // B2: Tạo kết nối  đến CSDL
            MySqlConnection con = dc.getConnection();
            // B3: Khởi tạo đối tượng lớp MySqlDataAdapter
            da = new MySqlDataAdapter(sql, con);
            // B4: Mở kết nối
            con.Open();
            // B5: Đổ dữ liệu từ MySqlDataAdapter vào DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            // B6: Đóng kết nối
            con.Close();
            return dt;
        }

        internal DataTable timSV(SinhVien sv)
        {
            throw new NotImplementedException();
        }

        public bool themSV(SinhVien sv)
        {
            string sql = "INSERT INTO tb_3layer(masv, tensv, lop, diem) VALUES (@masv, @tensv, @lop, @diem)";
            MySqlConnection con = dc.getConnection();
            try
            {
                cmd = new MySqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("masv", MySqlDbType.VarChar).Value = sv.masv;
                cmd.Parameters.Add("tensv", MySqlDbType.VarChar).Value = sv.tensv;
                cmd.Parameters.Add("lop", MySqlDbType.VarChar).Value = sv.lop;
                cmd.Parameters.Add("diem", MySqlDbType.Float).Value = sv.diem;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool suaSV(SinhVien sv)
        {
            string sql = "UPDATE tb_3layer SET masv=@masv, tensv=@tensv, lop=@lop, diem=@diem WHERE id=@id_sv";
            MySqlConnection con = dc.getConnection();
            try
            {
                cmd = new MySqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("id_sv", MySqlDbType.Int32).Value = sv.id;
                cmd.Parameters.Add("masv", MySqlDbType.VarChar).Value = sv.masv;
                cmd.Parameters.Add("tensv", MySqlDbType.VarChar).Value = sv.tensv;
                cmd.Parameters.Add("lop", MySqlDbType.VarChar).Value = sv.lop;
                cmd.Parameters.Add("diem", MySqlDbType.Float).Value = sv.diem;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool xoaSV(SinhVien sv)
        {
            string sql = "DELETE FROM tb_3layer WHERE id=@id_sv";
            MySqlConnection con = dc.getConnection();
            try
            {
                cmd = new MySqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("id_sv", MySqlDbType.Int32).Value = sv.id;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public DataTable timSV(string sv)
        {
            string sql = "SELECT * FROM tb_3layer where tensv like '%"+sv+ "%' or lop like '%" + sv + "%' or diem like '%" + sv + "%' or masv like '%" + sv + "%'";
            MySqlConnection con = dc.getConnection();
            da = new MySqlDataAdapter(sql, con);
            con.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
    }
}
