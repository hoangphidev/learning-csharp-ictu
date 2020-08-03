using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySanPham.BUS;
using QuanLySanPham.DTO;

namespace QuanLySanPham
{
    public partial class frmMain : Form
    {
        string flag; // trạng thái
        SanPhamBUS spBUS;

        // contructor mặc định
        public frmMain()
        {
            InitializeComponent();
        }
        // sự kiện load khi khởi động chương trình
        private void frmMain_Load(object sender, EventArgs e)
        {
            spBUS = new SanPhamBUS();
            showSanPham();
            lockControl();
            clearData();
        }
        
        // sự kiện đổ dữ liệu lên các ô nhập thông tin
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
                Tác dụng: khi chọn vào dòng dữ liệu trên datagridview,
                          dữ liệu sẽ điền dữ liệu lên Thông tin chi tiết.
             */
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            int index = dgvSanPham.CurrentRow.Index; // Lấy vị trí dòng trên datagridview
            if (index >= 0) 
            {
                // nếu tồn tại dòng trên datagridview thì mới cho phép hiện thông tin lên
                txtMa.Text = dgvSanPham.Rows[index].Cells[0].Value.ToString(); // 0,1,2,3 tương ứng với vị trí từng ô dữ liệu trên 1 dòng với vị trí index của datagridview
                txtTen.Text = dgvSanPham.Rows[index].Cells[1].Value.ToString();
                txtSoLuong.Text = dgvSanPham.Rows[index].Cells[2].Value.ToString();
                txtGia.Text = dgvSanPham.Rows[index].Cells[3].Value.ToString();
            }
        }
        // sự kiện nút thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = "add"; // trạng thái thêm
            txtMa.ReadOnly = false;
            dgvSanPham.Enabled = false;
            unLockControl();
            clearData();
        }
        // sự kiện nút sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = "edit"; // trạng thái sửa
            txtMa.ReadOnly = true;
            unLockControl();
        }
        // sự kiện nút lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == "add")
            {
                // trạng thái lưu
                if (checkMa()==true)
                {
                    SanPham sp = new SanPham();
                    sp.ma = txtMa.Text.ToString();
                    sp.ten = txtTen.Text.ToString();
                    sp.soluong = txtSoLuong.Text.ToString();
                    sp.dongia = txtGia.Text.ToString();
                    if (spBUS.themSP(sp))
                    {
                        showSanPham();
                    }
                    btnReset_Click(sender, e);
                    lockControl();
                    clearData();
                }
            }
            else if (flag == "edit")
            {
                // trạng thái sửa
                SanPham sp = new SanPham();
                sp.ma = txtMa.Text.ToString();
                sp.ten = txtTen.Text.ToString();
                sp.soluong = txtSoLuong.Text.ToString();
                sp.dongia = txtGia.Text.ToString();
                if (spBUS.suaSP(sp))
                {
                    showSanPham();
                }
                lockControl();
                clearData();
            }
        }
        // sự kiện nút xoá
        private void btnXoa_Click(object sender, EventArgs e)
        {
            SanPham sp = new SanPham();
            sp.ma = txtMa.Text.ToString();
            sp.ten = txtTen.Text.ToString();
            // tạo ra hộp thoại xác nhận xoá
            DialogResult dr = MessageBox.Show("Xác nhận xoá " + sp.ten,"Cảnh báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                if (spBUS.xoaSP(sp.ma))
                {
                    showSanPham();
                    clearData();
                }
                else
                {
                    MessageBox.Show("Xoá lỗi !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // sự kiện huỷ
        private void btnHuy_Click(object sender, EventArgs e)
        {
            /*
                Tác dụng: Huỷ tiến trình thêm hoặc sửa
             */
            if (flag == "edit")
            {
                lockControl();
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            else if (flag == "add")
            {
                btnReset_Click(sender,e); 
            }
            /*
                Note: chưa bắt được hết hoàn toàn lỗi logic
             */
        }
        // sự kiện khởi tạo
        private void btnReset_Click(object sender, EventArgs e)
        {
            /*
                Tác dụng: khởi tạo toàn bộ chương trình
             */
            frmMain_Load(sender,e);
            dgvSanPham.Enabled = true;
        }
        // sự kiện làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            /*
                Tác dụng: reset lại các ô nhập thông tin.
             */
            if (flag == "add")
            {
                // khi ở trạng thái thêm thì xoá toàn bộ ô nhập thông tin, chuột trỏ vào ô nhập mã
                txtMa.Clear();
                txtMa.Focus();
                txtTen.Clear();
                txtSoLuong.Clear();
                txtGia.Clear();
            }
            else if (flag == "edit")
            {
                // khi ở trạng thái sửa thì xoá toàn bộ ô nhập thông tin trừ ô nhập mã, chuột trỏ vào ô nhập tên
                txtTen.Clear();
                txtTen.Focus();
                txtSoLuong.Clear();
                txtGia.Clear();
            }
        }

        // lấy dữ liệu từ bus hiển thị lên datatable
        private void showSanPham()
        {
            DataTable dt = spBUS.getSanPham();
            dgvSanPham.DataSource = dt; // đổ dữ kiệu lên datagridview
        }
        // Kiểm tra mã nhập vào có trùng không
        private bool checkMa()
        {
            /*
             Ý tưởng bắt lỗi trùng với mã hoặc id: 
                - cách 1: so sánh mã nhập vào với giá trị trong cột mã trên datagridview
                - cách 2: so sánh mã nhập vào với mã lấy ra từ database (đã thực hiện được trên mô hình bình thường, đang phát triển trên mô hình 3 lớp)
                - cách 3: kết hợp mã nhập vào với 1 mã random gồm số + kí tự tránh lỗi trùng
             */
            for (int i = 0; i < dgvSanPham.Rows.Count - 1; i++)
            {
                if (txtMa.Text == dgvSanPham.Rows[i].Cells[0].Value.ToString())
                {
                    txtMa.Focus(); // nếu trùng thì con trỏ chuột chọn vào ô nhập mã
                    MessageBox.Show("Trùng mã !");
                    return false;
                }
            }
            return true;
        }
        // Xoá các ô nhập thông tin
        private void clearData()
        {
            txtMa.Clear();
            txtTen.Clear();
            txtSoLuong.Clear();
            txtGia.Clear();
            txtTimKiem.Clear();
        }
        // khoá nút lưu và huỷ
        private void lockControl()
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            gbThongTin.Enabled = false;
        }
        // mở khoá nút lưu và huỷ
        private void unLockControl()
        {
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            gbThongTin.Enabled = true;
        }
        // sự kiện tìm kiếm
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string value = txtTimKiem.Text;
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                DataTable dt = spBUS.timkiem(value);
                dgvSanPham.DataSource = dt;
            }
            else
            {
                showSanPham();
            }
        }
    }
}
