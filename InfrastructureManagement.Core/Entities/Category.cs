using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Entities
{
    [Table("Categorys")]
    public class Category : BaseEntity
    {
        public string  Code { get; set; }

        public int? Level { get; set; }

        public string Name { get; set; }

        public string MetaDatas { get; set; }

        public Guid? ParentId { get; set; }

        [NotMapped]
        public List<Category>? Children { get; set; }
    }
}
