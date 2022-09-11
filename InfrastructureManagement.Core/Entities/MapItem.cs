using InfrastructureManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Entities
{
    public class MapItem : BaseEntity
    {
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item? Item { get; set; }

        public Guid ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Item? Parent { get; set; }

        public bool IsFixed { get; set; }

        public RelationshipType RelationshipType { get; set; }
    }
}
