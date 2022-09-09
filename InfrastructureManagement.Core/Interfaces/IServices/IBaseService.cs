using InfrastructureManagement.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Interfaces.IServices
{
    public interface IBaseService<TEntity>
    {
        ServiceResult GetById(Guid id);

        ServiceResult GetAll();

        ServiceResult Add(TEntity entity);

        ServiceResult Update(TEntity entity, Guid id);

        ServiceResult Delete(Guid id);
    }
}
