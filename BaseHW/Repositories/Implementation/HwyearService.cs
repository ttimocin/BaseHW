using BaseHW.Models;
using BaseHW.Models.Domain;
using BaseHW.Repositories.Abstract;

namespace BaseHW.Repositories.Implementation
{
    public class HwyearService : IHwyearService
    {
        private readonly ApplicationDbContext ctx;
        public HwyearService(ApplicationDbContext ctx) 
        {
        
            this.ctx = ctx;
        }
        public bool Add(Hwyear model)
        {
            try
            {
                ctx.Hwyear.Add(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            } 
        }

        public bool Delete(int id)
        {
            try
            {
               var data= this.GetById(id);
                if(data == null)
                    return false;
                ctx.Hwyear.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Hwyear GetById(int id)
        {
            return ctx.Hwyear.Find(id);
        }

        public IQueryable<Hwyear> List()
        {
            var data= ctx.Hwyear.AsQueryable();
            return data;
        }

        public bool Update(Hwyear model)
        {
            try
            {
                ctx.Hwyear.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
