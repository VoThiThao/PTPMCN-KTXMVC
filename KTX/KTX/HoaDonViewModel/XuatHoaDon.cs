using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTX.ViewModels
{
    public class XuatHoaDon
    {
        public string MaHD { get; set; }

        public string MaNV { get; set; }

        public string MaPhong { get; set; }

        public DateTime NgayGhi { get; set; }
        //dien
        public int CSD { set; get; }

        public int CSC { set; get; }

        public double DonGia { set; get; }
        //nuoc
        public int CSDN { get; set; }


        public int CSCN { get; set; }

        public double DonGiaN { get; set; }

        public double TongTien { get; set; }
    }
}