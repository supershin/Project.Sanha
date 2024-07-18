namespace Project.Sanha.Web.Models
{
    public class ApproveModel
    {
        public int JuristicId { get; set; }

        public string? strSearch { get; set; }

        public string? ID { get; set; }
        public string? ProjectId { get; set; }

        public int? Status { get; set; }
        public string? ValidForm { get; set; }
        public string? ValidThrough { get; set; }
    }
}
