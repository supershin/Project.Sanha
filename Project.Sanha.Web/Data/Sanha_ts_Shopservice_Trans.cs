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
        public int? EventID { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string? CustomerName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerMobile { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerEmail { get; set; }
        public int? CustomerRelationID { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string? StaffName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? WorkTime { get; set; }
        [Unicode(false)]
        public string? Remark { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string? UpdateBy { get; set; }
        public int? Status { get; set; }
        public int? UsedQuota { get; set; }
    }
}
