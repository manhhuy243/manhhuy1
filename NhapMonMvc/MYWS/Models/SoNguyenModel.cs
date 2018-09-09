using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYWS.Models
{
    public class SoNguyenModel
    {
        [Display(Name ="Số A")]
        [Range(1,100,ErrorMessage ="{0} không được để trống.")]
        [Required(ErrorMessage = "{0} không được để trống.")]
        public int SoA { get; set; }

        [Display(Name = "Số B")]
        [Range(1, 100, ErrorMessage = "{0} không được để trống.")]
        [Required(ErrorMessage = "{0} không được để trống.")]
        public int SoB { get; set; }     

        [Display(Name = "Số C")]
        [Range(1, 100, ErrorMessage = "{0} không được để trống.")]
        [Required(ErrorMessage = "{0} không được để trống.")]
        public int SoC { get; set; }


    }
}