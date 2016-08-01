using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Transcode.Models;

namespace Transcode.DAL
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<TEntity> GetById(Object id);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null);
        void Insert(TEntity ent);
        void Update(TEntity ent);
        void Delete(Object id);
        void Delete(TEntity ent);

    }
}