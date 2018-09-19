using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DienMayws.Models;
namespace DienMayws.ViewModels
{

    public class ChungLoaiViewModel
    {
        public int ChungLoaiID { get; set; }
        public string Ten { get; set; }
        public int TongSoSanPham { get; set; }
        public IEnumerable<Loai> Loais { get; set; }
        public string BiDanh { get; set; }
    }
}