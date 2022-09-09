using InfrastructureManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Entities
{
    public class Item : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int? NumOfDay { get; set; }

        public string? Url { get; set; }

        public string? MoreDetail { get; set; }

        public int? QualityScore { get; set; }

        public int? MinScore { get; set; }

        public string? CategoryCode { get; set; }

        public Guid? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public ItemStatus Status { get; set; }
    }
}
