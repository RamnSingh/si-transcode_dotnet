using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Transcode.Models;

namespace Transcode.DAL
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IEntity, new();
        Task<bool> Save();
    }
}