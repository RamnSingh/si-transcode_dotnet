using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Transcode.Models;

namespace Transcode.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TranscodeContext context;
        private Dictionary<String, Object> repositories = null;
        public UnitOfWork()
        {
            context = new TranscodeContext();
        }

        public IRepository<TEntity> GetRepository<TEntity>() 
            where TEntity : class, IEntity, new()
        {
            if (repositories == null)
                repositories = new Dictionary<string, object>();

            if(!repositories.ContainsKey(typeof(TEntity).Name))
            {
                repositories.Add(typeof(TEntity).Name, new Repository<TEntity>(context));

            }
            return (IRepository<TEntity>)(repositories[typeof(TEntity).Name]);


        }

        public async virtual Task<bool> Save()
        {
            if(context.ChangeTracker.HasChanges())
            {
                try
                {
                    return await context.SaveChangesAsync() > 0;
                }
                catch (Exception){}
            }
            return true;
        }
    }
}