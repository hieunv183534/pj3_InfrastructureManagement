using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public int DeleteCategoryTree(string code)
        {
            var categorys = (from category in _entities where category.Code.StartsWith(code) select category).ToList();
            if(categorys.Count > 0)
            {
                _entities.RemoveRange(categorys);
            }

            return _databaseContext.SaveChanges();
        }
    }
}
