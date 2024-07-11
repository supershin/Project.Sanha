using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    public partial class Sanha_tr_UnitShopservice
    {
        [Key]
        public int ID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string ProjectID { get; set; } = null!;
        public int UnitID { get; set; }
        public int ShopID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? ContractNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public int? Used_Quota { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
    }
}
