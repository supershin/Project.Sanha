using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    public partial class Sanha_tr_Shopservice_Resource
    {
        [Key]
        public int ID { get; set; }
        public int EventID { get; set; }
        [StringLength(500)]
        public string? FileName { get; set; }
        [StringLength(500)]
        public string? FilePath { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string? MimeType { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
    }
}
