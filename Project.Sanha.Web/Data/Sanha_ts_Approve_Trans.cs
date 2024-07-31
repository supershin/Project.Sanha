using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    public partial class Sanha_ts_Approve_Trans
    {
        [Key]
        public Guid ID { get; set; }
        public int TransID { get; set; }
        public int ProjectID { get; set; }
        public int UnitID { get; set; }
        public int ShopID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? OrderNo { get; set; }
        public int Status { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string? Note { get; set; }
        [Required]
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
    }
}
