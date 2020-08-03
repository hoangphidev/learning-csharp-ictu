using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuanLySanPham.DAL;
using QuanLySanPham.DTO;

namespace QuanLySanPham.BUS
{
    class SanPhamBUS
    {
        SanPhamDAL spDAL;
        Random rd;
        string ma;
        // hàm khởi tạo
        public SanPhamBUS()
        {
            rd = new Random();
            spDAL = new SanPhamDAL();
            // tạo ta mã với 3 số và 1 kí tự in hoa
            ma = rd.Next(100, 999).ToString() + Convert.ToString((char)rd.Next(65, 90));
        }
        // lấy dữ liệu từ cơ sở dữ liệu ra bus
        public DataTable getSanPham()
        {
            return spDAL.getSanPham();
        }
        // thêm
        public bool themSP(SanPham sp)
        {
            sp.ma = "SP_" + sp.ma + "_" + ma; // mã có cấu trúc SP_"mã nhập vào"_"mã random" .
            return spDAL.themSP(sp);
        }
        // sửa
        public bool suaSP(SanPham sp)
        {
            return spDAL.suaSP(sp);
        }
        // xoá
        public bool xoaSP(string ma)
        {
            return spDAL.xoaSP(ma);
        }
        // tìm kiếm
        public DataTable timkiem(String sp)
        {
            return spDAL.timkiem(sp);
        }
    }
}
