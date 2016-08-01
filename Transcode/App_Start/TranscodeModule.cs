using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transcode.DAL;
using Transcode.Models;

namespace Transcode
{
    public class TranscodeModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUnitOfWork>().To<UnitOfWork>();
            this.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
        }
    }
}