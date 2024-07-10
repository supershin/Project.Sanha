using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    [Keyless]
    public partial class master_project
    {
        public int id { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string project_id { get; set; } = null!;
        public int bu_id { get; set; }
        public int comp_id { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string project_name { get; set; } = null!;
        public int project_type_id { get; set; }
        [Column(TypeName = "date")]
        public DateTime? deliver_on { get; set; }
        public int is_active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime create_on { get; set; }
        public int create_by { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime update_on { get; set; }
        public int update_by { get; set; }
        public int warranty_product { get; set; }
        public int warranty_structure { get; set; }
    }
}
