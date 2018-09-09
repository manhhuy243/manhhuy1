using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MYWS.Models
{
    public class MonHoc
    {
        public int MonHocID { get; set; }
        public string Ten { get; set; }
        public int SoTiet { get; set; }

    }
    public class QLMonHoc
    {
        //Field
        private List<MonHoc> dsMonHoc = new List<MonHoc>();
        //private method
        private void KhoiTaoNguonDuLieu()
        {
            dsMonHoc.Add(new MonHoc { MonHocID = 1, Ten = "Môn 1", SoTiet = 120 });
            dsMonHoc.Add(new MonHoc { MonHocID = 2, Ten = "Môn 2", SoTiet = 180 });
            dsMonHoc.Add(new MonHoc { MonHocID = 3, Ten = "Môn 3", SoTiet = 30 });
            dsMonHoc.Add(new MonHoc { MonHocID = 4, Ten = "Môn 4", SoTiet = 120 });
            dsMonHoc.Add(new MonHoc { MonHocID = 5, Ten = "Môn 5", SoTiet = 180 });
            dsMonHoc.Add(new MonHoc { MonHocID = 6, Ten = "Môn 6", SoTiet = 30 });
            dsMonHoc.Add(new MonHoc { MonHocID = 7, Ten = "Môn 7", SoTiet = 120 });
            dsMonHoc.Add(new MonHoc { MonHocID = 8, Ten = "Môn 8", SoTiet = 180 });
            dsMonHoc.Add(new MonHoc { MonHocID = 9, Ten = "Môn 9", SoTiet = 30 });
        }

        //constructor
        public QLMonHoc()
        {
            KhoiTaoNguonDuLieu();
        }
        //public method
        public List<MonHoc> Doc()
        {
            return dsMonHoc;
        }
        public MonHoc Doc(int id)
        {
            MonHoc item = dsMonHoc.Find(p => p.MonHocID == id);
            return item;
        }
    }
}