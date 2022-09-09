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
        public Item PositionItem { get; set; }

        public string PositionString { get; set; }

        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public string Note { get; set; }

        public ReportType Type { get; set; }
    }
}
