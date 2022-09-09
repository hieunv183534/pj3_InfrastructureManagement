using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Interfaces.IRepositories
{
    public interface IBaseRepository<TEntity>
    {
        TEntity GetById(Guid id);

        List<TEntity> GetAll();

        int Add(TEntity entity);

        int Update(TEntity entity, Guid id);

        int Delete(Guid id);
    }
}
