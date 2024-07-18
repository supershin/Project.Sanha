using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Sanha.Web.Data
{
    [Keyless]
    public partial class user_projects
    {
        public int id { get; set; }
        public int bu_id { get; set; }
        public int project_id { get; set; }
        public int user_id { get; set; }
        public int is_active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime create_on { get; set; }
        public int create_by { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime update_on { get; set; }
        public int update_by { get; set; }
    }
}
