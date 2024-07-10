using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    [Keyless]
    public partial class master_unit
    {
        public int id { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string project_id { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string unit_id { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string unit_code { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string unit_status_id { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? addr_no { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? floor { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? bulid { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? water_meter { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? electric_meter { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? area { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? contract_number { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? customer_name { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? customer_mobile { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? customer_email { get; set; }
        [Column(TypeName = "date")]
        public DateTime? book_date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? contract_date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? transfer_date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime create_on { get; set; }
        public int create_by { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime update_on { get; set; }
        public int update_by { get; set; }
    }
}
