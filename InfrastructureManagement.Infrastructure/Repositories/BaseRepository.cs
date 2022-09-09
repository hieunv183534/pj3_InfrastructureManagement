using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _databaseContext;
        protected readonly string _tableName;
        protected DbSet<TEntity> _entities;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _tableName = typeof(TEntity).Name;
            _entities = (DbSet<TEntity>)typeof(DatabaseContext).GetProperty($"{_tableName}s").GetValue(_databaseContext);
        }

        public int Add(TEntity entity)
        {
            _entities.Add(entity);
            int rows = _databaseContext.SaveChanges();
            return rows;
        }

        public int Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
            int rows = _databaseContext.SaveChanges();
            return rows;
        }

        public virtual List<TEntity> GetAll()
        {
            List<TEntity> listEntity = _entities.ToList();

            foreach (var entity in listEntity)
            {
                Console.WriteLine(entity.ToString());
            }
            return listEntity;
        }

        public TEntity GetById(Guid id)
        {
            var myEntity = (from entity in _entities where entity.Id == id select entity).FirstOrDefault();
            return myEntity;
        }

        public int Update(TEntity entity, Guid id)
        {

            TEntity myEntity = GetById(id);
            var props = myEntity.GetType().GetProperties();

            foreach (var prop in props)
            {
                var propValue = prop.GetValue(entity);
                if (prop.Name == "Id")
                    continue;
                prop.SetValue(myEntity, propValue);
            }

            int rows = _databaseContext.SaveChanges();
            return rows;
        }
    }
}
