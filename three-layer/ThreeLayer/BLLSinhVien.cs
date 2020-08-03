using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayer
{
    class BLLSinhVien
    {
        DALSinhVien dalSV;
        public BLLSinhVien()
        {
            dalSV = new DALSinhVien();
        }

        public DataTable getAllSinhVien()
        {
            return dalSV.getAllSinhVien();
        }

        public bool themSV(SinhVien sv)
        {
            return dalSV.themSV(sv);
        }

        public bool suaSV(SinhVien sv)
        {
            return dalSV.suaSV(sv);
        }

        public bool xoaSV(SinhVien sv)
        {
            return dalSV.xoaSV(sv);
        }

        public DataTable timSV(string sv)
        {
            return dalSV.timSV(sv);
        }
    }
}
