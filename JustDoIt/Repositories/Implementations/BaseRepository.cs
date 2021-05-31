using JustDoIt.Database;
using JustDoIt.Models.Base;
using JustDoIt.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustDoIt.Repositories.Implementations
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        private readonly ApplicationContext ctx;
        public BaseRepository(ApplicationContext ctx)
        {
            this.ctx = ctx;
        }

        public TDbModel Create(TDbModel model)
        {
            ctx.Set<TDbModel>().Add(model);
            ctx.SaveChanges();
            return model;
        }

        public void Delete(Guid id)
        {
            var toDelete = ctx.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            ctx.Set<TDbModel>().Remove(toDelete);
            ctx.SaveChanges();
        }

        public List<TDbModel> GetAll()
        {
            return ctx.Set<TDbModel>().ToList();
        }

        public TDbModel Update(TDbModel model)
        {
            var toUpdate = ctx.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpdate != null)
            {
                toUpdate = model;
            }
            ctx.Update(toUpdate);
            ctx.SaveChanges();
            return toUpdate;
        }

        public TDbModel Get(Guid id)
        {
            return ctx.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }
    }
}
