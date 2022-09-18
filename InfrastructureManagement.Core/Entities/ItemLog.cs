using InfrastructureManagement.Core.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Entities
{
    public class ItemLog : BaseEntity
    {
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [Column(TypeName = "TEXT")]
        public string? LogDetail { get; set; }

        [Column(TypeName = "TEXT")]
        public string LogData { get; set; }

        public ItemLogType Type { get; set; }
    }
}
