using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    public partial class Sanha_tm_Shopservice
    {
        [Key]
        public int ID { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? Name { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }
        [StringLength(500)]
        public string? LogoPath { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
        public bool? DefaultQuata { get; set; }
    }
}
