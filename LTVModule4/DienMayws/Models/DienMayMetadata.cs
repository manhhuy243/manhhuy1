using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DienMayws.Models
{
    [MetadataType(typeof(HoaDon.HoaDonMetadata))]
    public partial class HoaDon
    {
        internal class HoaDonMetadata
        {
            [Display(Name ="Hóa đơn ID")]
            public int HoaDonID;

            [Display(Name = "Ngày đạt hàng")]
            [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =false)]
            public System.DateTime NgayDatHang;

            [Display(Name = "Họ tên khách hàng")]
            public string HoTenKhach;

            [Display(Name = "Đại chỉ")]
            public string DiaChi;

            [Display(Name = "Diện thoại")]
            public string DienThoai;

            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage ="Email không hợp lệ!")]
            public string Email;

            [Display(Name = "Tổng Tiền")]
            [DisplayFormat(DataFormatString ="{0:#,##0VND}",ApplyFormatInEditMode =false)]
            public int TongTien;
        }

    }

    [MetadataType(typeof(ChungLoai.ChungLoaiMetadata))]
    public partial class ChungLoai
    {
        internal class ChungLoaiMetadata
        {
            [Display(Name ="Chủng Loại ID")]
            public int ChungLoaiID;

            [Display(Name ="Tên Chủng Loại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            //[StringLength(50,ErrorMessage ="{0} tối đa là {1} ký tự")]
            [StringLength(50,MinimumLength =2,ErrorMessage ="{0} từ {2} đến {1} ký tự")]
            public string Ten;

            [Display(Name = "Bí Danh")]
            public string BiDanh;
        }

    }

    [MetadataType(typeof(Loai.LoaiMetadata))]
    public partial class Loai
    {
        internal class LoaiMetadata
        {
            [Display(Name ="Loại ID")]
            public int LoaiID;

            [Display(Name = "Tên Loại")]
            [Required(ErrorMessage ="{0} không được để trống")]
            [MaxLength(50,ErrorMessage ="{0} tối đa là {1} ký tự")]
            [MinLength(2,ErrorMessage ="{0} tối thiểu {1} ký tự")]
            public string Ten;

            [Display(Name = "Chủng Loại")]
            public Nullable<int> ChungLoaiID;

            [Display(Name = "Bí Danh")]
            public string BiDanh;


        }

    }
    public partial class SanPham
    {
        internal class SanPhamMetadata
        {
            private SanPhamMetadata() { }
            [ScaffoldColumn(false)]
            public int SanPhamID;

            [Display(Name ="Nhà Sản Xuất")]
            public Nullable<int> NhaSanXuatID;

            [Display(Name = "Loại")]
            public Nullable<int> LoaiID;

            [Display(Name = "Tên Sản Phẩm")]
            [Required(ErrorMessage = "{0} không được để trống")]
            //[StringLength(50,ErrorMessage ="{0} tối đa là {1} ký tự")]
            [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} từ {2} đến {1} ký tự")]
            public string Ten;

            [Display(Name ="Giá Bán")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [Range(0,int.MaxValue,ErrorMessage ="{0} phải từ {1} đến {2}")]
            public int GiaBan;

            [Display(Name ="Mô Tả")]
            [StringLength(250, MinimumLength = 2, ErrorMessage = "{0} từ {2} đến {1} ký tự")]
            [DataType(DataType.Password)]
            public string MoTa;

            [Display(Name = "Xuất Xứ")]
            public string XuatXu;

            [Display(Name ="Trạng Thái")]
            public string TrangThai;

            [Display(Name = "Hình 1")]           
            public string Hinh1;

            [Display(Name = "Hình 2")]
            public string Hinh2;

            [Display(Name = "Hình 3")]
            public string Hinh3;

            [Display(Name = "Bí Danh")]
            [ScaffoldColumn(false)]
            public string BiDanh;

            [Display(Name = "Kích Thước")]
            public string KichCo;

            [Display(Name = "Bang Tan")]
            public string BangTan;

            [Display(Name = "Camera")]
            public string Camera;

            [Display(Name = "GPRS")]
            public string GPRS;

            [Display(Name = "Đặc Tính")]
            [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} từ {2} đến {1} ký tự")]
            public string DacTinh;
        }
    }
}