using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace EmployeeManagement
{
    public partial class EmployeeManagement : Form
    {
        string flag;
        string str = "datasource=127.0.0.1; username=root; password=; database=dotnet_db";
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataTable table = new DataTable();
        public EmployeeManagement()
        {
            InitializeComponent();
        }

        private void EmployeeManagement_Load(object sender, EventArgs e)
        {
            LockControl();
            con = new MySqlConnection(str);
            con.Open();
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UnlockControl();
            flag = "add";
            txtID.ReadOnly = false;
            ClearData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UnlockControl();
            flag = "edit";
            txtID.ReadOnly = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (flag=="add")
            {
                if (CheckData())
                {
                    string sql = "insert into tb_nv(id,name,phone,email) values (@id,@name,@phone,@email)";
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("id", txtID.Text).Value.ToString();
                    cmd.Parameters.AddWithValue("name", txtName.Text).Value.ToString();
                    cmd.Parameters.AddWithValue("phone", txtPhone.Text).Value.ToString();
                    cmd.Parameters.AddWithValue("email", txtEmail.Text).Value.ToString();
                    cmd.ExecuteNonQuery();
                    LoadData();
                    ClearData();
                    LockControl();
                }
                else
                {
                    MessageBox.Show("Add Error !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else if (flag == "edit")
            {
                if (CheckData())
                {
                    string sql = "update tb_nv set name = @name,phone = @phone, email = @email where id = @id";
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("id", txtID.Text).Value.ToString();
                    cmd.Parameters.AddWithValue("name", txtName.Text).Value.ToString();
                    cmd.Parameters.AddWithValue("phone", txtPhone.Text).Value.ToString();
                    cmd.Parameters.AddWithValue("email", txtEmail.Text).Value.ToString();
                    cmd.ExecuteNonQuery();
                    LoadData();
                    ClearData();
                    LockControl();
                }
                else
                {
                    MessageBox.Show("Update Error !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            int index = dgvEmployee.CurrentRow.Index;
            if (index >= 0)
            {
                txtID.Text = dgvEmployee.Rows[index].Cells[0].Value.ToString();
                txtName.Text = dgvEmployee.Rows[index].Cells[1].Value.ToString();
                txtPhone.Text = dgvEmployee.Rows[index].Cells[2].Value.ToString();
                txtEmail.Text = dgvEmployee.Rows[index].Cells[3].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Confirm Delete ?", "Warm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                string sql = "delete from tb_nv where id = @id";
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", txtID.Text).Value.ToString();
                cmd.ExecuteNonQuery();
                LoadData();
                ClearData();
            }
            LockControl();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LockControl();
            ClearData();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void ClearData()
        {
            txtID.Focus();
            txtID.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
        }
        private void LoadData()
        {
            string sql = "select * from tb_nv";
            cmd = new MySqlCommand(sql, con);
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dgvEmployee.DataSource = table;
        }
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Please input ID !", "Warm",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtID.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please input name !", "Warm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please input phone !", "Warm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please input email !", "Warm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            return true;
        }
        private void LockControl()
        {
            btnAdd.Enabled = true;

            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            gbDetail.Enabled = false;

            btnAdd.Focus();
        }
        private void UnlockControl()
        {
            btnAdd.Enabled = false;

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            gbDetail.Enabled = true;

            txtID.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                LoadData();
            }
            else
            {
                if (rbID.Checked == true)
                {
                    string sql = "select * from tb_nv where id = @id";
                    string k = "id";
                    SearchBox(sql, k);
                }
                else if (rbName.Checked == true)
                {
                    string sql = "select * from tb_nv where name = @name";
                    string k = "name";
                    SearchBox(sql, k);
                }
                else if (rbPhone.Checked == true)
                {
                    string sql = "select * from tb_nv where phone = @phone";
                    string k = "phone";
                    SearchBox(sql, k);
                }
                else if (rbEmail.Checked == true)
                {
                    string sql = "select * from tb_nv where email = @email";
                    string k = "email";
                    SearchBox(sql, k);
                }
            }
        }

        private void SearchBox(string sql, string k)
        {
            cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue(k, txtSearch.Text).Value.ToString();
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dgvEmployee.DataSource = table;
        }
    }
}
