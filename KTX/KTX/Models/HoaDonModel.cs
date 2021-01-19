using DocumentFormat.OpenXml.Office2010.ExcelAc;
using KTX.ViewModels;
using Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KTX.Models
{
    public class HoaDonModel
    {
        private DBKTX db;

        public HoaDonModel()
        {
            db = new DBKTX();
        }

        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        [Display(Name = "Mã hóa đơn")]
        public string MaHD
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        [Display(Name = "Mã nhân viên")]
        public string MaNV
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        [Display(Name = "Mã phòng")]
        public string MaPhong
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        [Display(Name = "Ngày ghi")]
        public DateTime NgayGhi
        {
            get;
            set;
        }
        public double tongTien { get; set; }
        public virtual DIEN DIEN { get; set; }
        public virtual NUOC NUOC { get; set; }


        public List<HOADON> listAll()
        {
            return db.HOADONs.ToList();
        }

        public List<HoaDonViewModel> FindHD(string searchString)
        {

            var query = from i in db.HOADONs
                        join c in db.PHONGs on i.MaPhong equals c.MaPhong
                        join d in db.DIENs on c.MaPhong equals d.MaPhong
                        join e in db.NUOCs on d.MaPhong equals e.MaPhong
                        where (i.MaHD.Contains(searchString))
                        select new
                        {
                            hd = i.MaHD,
                            nv = i.MaNV,
                            mp = i.MaPhong,
                            nghi = i.NgayGhi,
                            chiSoMoiD = d.CSC,
                            chiSoCuD = d.CSD,
                            dogiaD = d.DonGia,
                            chiSoMoiN = e.CSC,
                            chiSoCuN = e.CSD,
                            donGiaN = e.DonGia,
                        };
            List<HoaDonViewModel> hd = new List<HoaDonViewModel>();

            foreach (var item in query)
            {

                HoaDonViewModel hd1 = new HoaDonViewModel();
                hd1.MaHD = item.hd;
                hd1.MaNV = item.nv;
                hd1.MaPhong = item.mp;
                hd1.NgayGhi = item.nghi;
                var tongTien = (item.chiSoMoiD - item.chiSoCuD) * item.dogiaD + (item.chiSoMoiN - item.chiSoCuN) * item.donGiaN;
                hd1.TongTien = tongTien;
                hd.Add(hd1);

            }
            //check;
            return hd;


        }
        //in hóa đơn
        public List<HoaDonViewModel> inHoaDons()
        {
            var query = from i in db.HOADONs
                        join c in db.PHONGs on i.MaPhong equals c.MaPhong
                        join d in db.DIENs on c.MaPhong equals d.MaPhong
                        join e in db.NUOCs on d.MaPhong equals e.MaPhong
                        select new
                        {
                            hd = i.MaHD,
                            nv = i.MaNV,
                            mp = i.MaPhong,
                            nghi = i.NgayGhi,
                            chiSoMoiD = d.CSC,
                            chiSoCuD = d.CSD,
                            dogiaD = d.DonGia,
                            chiSoMoiN = e.CSC,
                            chiSoCuN = e.CSD,
                            donGiaN = e.DonGia,
                        };
            List<HoaDonViewModel> hd = new List<HoaDonViewModel>();

            foreach (var item in query)
            {

                HoaDonViewModel hd1 = new HoaDonViewModel();
                hd1.MaHD = item.hd;
                hd1.MaNV = item.nv;
                hd1.MaPhong = item.mp;
                hd1.NgayGhi = item.nghi;
                var tongTien = (item.chiSoMoiD - item.chiSoCuD) * item.dogiaD + (item.chiSoMoiN - item.chiSoCuN) * item.donGiaN;
                hd1.TongTien = tongTien;
                hd.Add(hd1);

            }
            return hd;
        }

        public List<XuatHoaDon> listAllHD()
        {
            var query = from i in db.HOADONs
                        join c in db.PHONGs on i.MaPhong equals c.MaPhong
                        join d in db.DIENs on c.MaPhong equals d.MaPhong
                        join e in db.NUOCs on d.MaPhong equals e.MaPhong
                        select new
                        {
                            hd = i.MaHD,
                            nv = i.MaNV,
                            mp = i.MaPhong,
                            nghi = i.NgayGhi,
                            chiSoMoiD = d.CSC,
                            chiSoCuD = d.CSD,
                            dogiaD = d.DonGia,
                            chiSoMoiN = e.CSC,
                            chiSoCuN = e.CSD,
                            donGiaN = e.DonGia,
                        };

            List<XuatHoaDon> hd = new List<XuatHoaDon>();

            foreach (var item in query)
            {
                XuatHoaDon xuatHoaDon = new XuatHoaDon()
                {
                    MaHD = item.hd,
                    MaNV = item.nv,
                    MaPhong = item.mp,
                    NgayGhi = item.nghi,
                    CSC = item.chiSoMoiD,
                    CSD = item.chiSoCuD,
                    DonGia = item.dogiaD,
                    CSCN = item.chiSoMoiN,
                    CSDN = item.chiSoCuN,
                    DonGiaN = item.donGiaN

                };

                hd.Add(xuatHoaDon);
            }

            return hd;
        }


        public String Insert(HOADON enity)
        {
            db.HOADONs.Add(enity);
            db.SaveChanges();


            return enity.MaHD;
        }
        public HOADON Find(string maHD)
        {
            return db.HOADONs.Find(maHD);

        }

        public HOADON GetByMaHD(string maHD)
        {
            return db.HOADONs.SingleOrDefault(x => x.MaHD == maHD);
        }

        public bool Update(HOADON maHD)
        {
            try
            {
                var hd = db.HOADONs.Select(x => x).Where(x => x.MaHD == maHD.MaHD).FirstOrDefault();
                hd.MaHD = maHD.MaHD;
                hd.MaNV = maHD.MaNV;
                hd.MaPhong = maHD.MaPhong;
                hd.NgayGhi = maHD.NgayGhi;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public void Delete(string maHD)
        {
            try
            {
                var hd = db.HOADONs.FirstOrDefault(x => x.MaHD.Contains(maHD));
                db.HOADONs.Remove(hd);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}