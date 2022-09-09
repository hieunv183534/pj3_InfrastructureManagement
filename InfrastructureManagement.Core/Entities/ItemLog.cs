using InfrastructureManagement.Core.Enums;
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

        public string LogDetail { get; set; }

        public string LogUrl { get; set; }

        public ItemLogType Type { get; set; }
    }
}
