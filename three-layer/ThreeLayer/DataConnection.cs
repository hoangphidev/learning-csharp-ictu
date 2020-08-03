using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ThreeLayer
{
    class DataConnection
    {
        string str;
        public DataConnection()
        {
            str = "datasource=127.0.0.1; username=root; password=; database=db_phi";
        }

        public MySqlConnection getConnection()
        {
            return new MySqlConnection(str);
        }
    }
}
