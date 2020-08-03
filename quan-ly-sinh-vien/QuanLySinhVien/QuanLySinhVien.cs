using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLySinhVien
{
    public partial class QuanLySinhVien : Form
    {
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        String str = @"Data Source=DESKTOP-9C3Q1PD;Initial Catalog=db_sinhvien;Integrated Security=True";
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        public QuanLySinhVien()
        {
            InitializeComponent();
        }
        private void KhoiTao()
        {
            txtMaSV.ReadOnly = false;
            txtMaSV.Clear();
            txtHoSV.Clear();
            txtTenSV.Clear();
            txtMaKhoa.Clear();
            cbGioiTinh.Text = "Chọn";
            ngaySinh.Text = DateTime.Now.ToString();
            txtMaSV.Focus();
        }
        private void LoadData()
        {
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "select * from tb_sinhvien";
            dataAdapter.SelectCommand = sqlCommand;
            table.Clear();
            dataAdapter.Fill(table);
            tbSinhVien.DataSource = table;
        }

        private void QuanLySinhVien_Load(object sender, EventArgs e)
        {
            KhoiTao();
            sqlConnection = new SqlConnection(str);
            sqlConnection.Open();
            LoadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Xác nhận đóng chương trình !", "Chú ý", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void tbSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSV.ReadOnly = true;
            int i;
            i = tbSinhVien.CurrentRow.Index;
            txtMaSV.Text = tbSinhVien.Rows[i].Cells[0].Value.ToString();
            txtHoSV.Text = tbSinhVien.Rows[i].Cells[1].Value.ToString();
            txtTenSV.Text = tbSinhVien.Rows[i].Cells[2].Value.ToString();
            ngaySinh.Text = tbSinhVien.Rows[i].Cells[3].Value.ToString();
            cbGioiTinh.Text = tbSinhVien.Rows[i].Cells[4].Value.ToString();
            txtMaKhoa.Text = tbSinhVien.Rows[i].Cells[5].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "insert into tb_sinhvien values('" + txtMaSV.Text + "',N'" + txtHoSV.Text + "',N'" + txtTenSV.Text + "','" + ngaySinh.Text + "',N'" + cbGioiTinh.Text + "','" + txtMaKhoa.Text + "')";
            sqlCommand.ExecuteNonQuery();
            LoadData();
            KhoiTao();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "update tb_sinhvien set MaSV = '" + txtMaSV.Text + "' , HoSV = N'" + txtHoSV.Text + "', TenSV = N'" + txtTenSV.Text + "', NgaySinh = '" + ngaySinh.Text + "', GioiTinh = N'" + cbGioiTinh.Text + "', MaKhoa = '" + txtMaKhoa.Text + "' where MaSV = '"+txtMaSV.Text+"' ";
            sqlCommand.ExecuteNonQuery();
            LoadData();
            KhoiTao();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "delete from tb_sinhvien where MaSV = '"+txtMaSV.Text+"'";
            sqlCommand.ExecuteNonQuery();
            LoadData();
            KhoiTao();
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            KhoiTao();
        }
    }
}
