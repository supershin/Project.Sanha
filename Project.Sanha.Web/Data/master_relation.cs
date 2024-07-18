using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    [Keyless]
    public partial class master_relation
    {
        public int id { get; set; }
        public int? seq { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string name { get; set; } = null!;
        public int is_active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime create_on { get; set; }
        public int create_by { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime update_on { get; set; }
        public int update_by { get; set; }
    }
}
