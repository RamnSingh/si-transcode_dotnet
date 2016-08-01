using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Transcode.Models;

namespace Transcode.DAL
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly TranscodeContext context;
        private DbSet<TEntity> dbSet;
        public Repository(TranscodeContext ctx)
        {
            context = ctx;
            dbSet = ctx.Set<TEntity>();
        }

        public async virtual Task<TEntity> GetById(Object id) 
        {
            return await dbSet.FindAsync(id);
        }

        public async virtual Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();

        }

        public virtual void Insert(TEntity ent)
        {
            dbSet.Add(ent);
        }

        public virtual void Update(TEntity ent)
        {
            dbSet.Attach(ent);
            context.Entry(ent).State = EntityState.Modified;
        }

        public virtual void Delete(Object id)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity ent)
        {
            throw new NotImplementedException();
        }
    }
}