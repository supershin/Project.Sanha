using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    [Keyless]
    public partial class user
    {
        public int id { get; set; }
        public int dept_id { get; set; }
        public int role_id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string username { get; set; } = null!;
        [StringLength(200)]
        [Unicode(false)]
        public string password { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string firstname { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string lastname { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? email { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? mobile { get; set; }
        public int is_receive_mail { get; set; }
        public int is_active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime create_on { get; set; }
        public int create_by { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime update_on { get; set; }
        public int update_by { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? reset_token { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? reset_on { get; set; }
    }
}
