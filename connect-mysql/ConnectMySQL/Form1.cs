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


namespace ConnectMySQL
{
    public partial class Form1 : Form
    {
        string conStr;
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataTable table = new DataTable();
        public Form1()
        {
            InitializeComponent();
            conStr = "datasource=127.0.0.1; port=3306; username=root; password=; database=dotnet_db;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection(conStr);
            con.Open();
            LoadData();
        }

        private void LoadData()
        {
            string sql = "select * from tb_nv";
            cmd = new MySqlCommand(sql,con);
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dgvNV.DataSource = table;
        }
    }
}
