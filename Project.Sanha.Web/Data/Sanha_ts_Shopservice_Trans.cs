using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    public partial class Sanha_ts_Shopservice_Trans
    {
        [Key]
        public int ID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string ProjectID { get; set; } = null!;
        public int UnitID { get; set; }
        public int ShopID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerFirstName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerLastName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerMobile { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerEmail { get; set; }
        [Column(TypeName = "text")]
        public string? CustomerSignature { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? StaffFirstName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? StaffLastName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartWorkDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndWorkDate { get; set; }
        [Column(TypeName = "text")]
        public string? StaffSignature { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Remark { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
        public int? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AuditDate { get; set; }
        public int? AuditBy { get; set; }
    }
}
