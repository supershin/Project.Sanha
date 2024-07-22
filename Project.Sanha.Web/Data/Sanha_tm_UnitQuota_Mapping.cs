using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    public partial class Sanha_tm_UnitQuota_Mapping
    {
        [Key]
        public int ID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string ProjectID { get; set; } = null!;
        public int ShopID { get; set; }
        public int? UnitID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? UnitCode { get; set; }
        public int Quota { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
    }
}
