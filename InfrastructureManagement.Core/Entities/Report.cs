using InfrastructureManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Entities
{
    public class Report : BaseEntity
    {
        public Guid PositionId { get; set; }

        [ForeignKey("PositionId")]
        public Item? PositionItem { get; set; }

        public string? PositionString { get; set; }

        public int? Quantity { get; set; }

        public Guid? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public Guid? ReporterId { get; set; }

        [ForeignKey("ReporterId")]
        public Account? Reporter { get; set; }

        public string Note { get; set; }

        public string? Reply { get; set; }

        public ReportType? Type { get; set; }

        public ReportStatus? Status { get; set; }

        public string? Urls { get; set; }
    }
}
