using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreeLayer
{
    public partial class frmMain : Form
    {
        int ID;
        string flag;
        BLLSinhVien bllSV;
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            bllSV = new BLLSinhVien();
            ShowAllSinhVien();
            LockControl();
            ClearData();
        }

        public void ShowAllSinhVien()
        {
            DataTable dt = bllSV.getAllSinhVien();
            dgvSinhVien.DataSource = dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = "add";
            UnLockControl();
            txtMaSV.Focus();
            txtMaSV.ReadOnly = false;
            dgvSinhVien.Enabled = false;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = "edit";
            UnLockControl();
            txtMaSV.ReadOnly = true;

        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == "add")
            {
                if (CheckData())
                {
                    SinhVien sv = new SinhVien();
                    sv.masv = txtMaSV.Text;
                    sv.tensv = txtTenSV.Text;
                    sv.lop = txtLop.Text;
                    sv.diem = float.Parse(txtDiem.Text);
                    if (bllSV.themSV(sv))
                    {
                        ShowAllSinhVien();
                        LockControl();
                        ClearData();
                        dgvSinhVien.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Lỗi thêm sinh viên", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    return;
                }
            }
            else if (flag == "edit")
            {
                if (CheckData())
                {
                    SinhVien sv = new SinhVien();
                    sv.id = ID;
                    sv.masv = txtMaSV.Text;
                    sv.tensv = txtTenSV.Text;
                    sv.lop = txtLop.Text;
                    sv.diem = float.Parse(txtDiem.Text);
                    if (bllSV.suaSV(sv))
                    {
                        ShowAllSinhVien();
                        LockControl();
                        ClearData();
                        dgvSinhVien.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Lỗi sửa sinh viên", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xác nhận xoá " + txtTenSV.Text + " ?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                SinhVien sv = new SinhVien();
                sv.id = ID;
                if (bllSV.xoaSV(sv))
                {
                    ShowAllSinhVien();
                    LockControl();
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Lỗi xoá sinh viên", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearData();
            frmMain_Load(sender, e);
            dgvSinhVien.Enabled = true;
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThem.Enabled = false;
            int index = dgvSinhVien.CurrentRow.Index;
            if (index >= 0)
            {
                ID = Int32.Parse(dgvSinhVien.Rows[index].Cells["id_sv"].Value.ToString());
                txtMaSV.Text = dgvSinhVien.Rows[index].Cells["masv"].Value.ToString();
                txtTenSV.Text = dgvSinhVien.Rows[index].Cells["tensv"].Value.ToString();
                txtLop.Text = dgvSinhVien.Rows[index].Cells["lop"].Value.ToString();
                txtDiem.Text = dgvSinhVien.Rows[index].Cells["diem"].Value.ToString();
            }
        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string value = txtTimKiem.Text;
            if (!string.IsNullOrEmpty(value))
            {
                DataTable dt = bllSV.timSV(value);
                dgvSinhVien.DataSource = dt;
            }
            else
            {
                ShowAllSinhVien();
            }
        }

        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtMaSV.Text))
            {
                MessageBox.Show("Chưa nhập mã sinh viên", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSV.Focus();
                return false;
            }
            return true;
        }
        private void ClearData()
        {
            txtMaSV.Clear();
            txtMaSV.Focus();
            txtTenSV.Clear();
            txtLop.Clear();
            txtDiem.Clear();
        }
        private void LockControl()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            gbThongTin.Enabled = false;
        }
        private void UnLockControl()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            gbThongTin.Enabled = true;
        }

    }
}
