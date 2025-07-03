using BaseHW.Models;
using BaseHW.Models.Domain;
using BaseHW.Repositories.Abstract;

namespace BaseHW.Repositories.Implementation
{
    public class SikuyearService : ISikuyearService
    {
        private readonly ApplicationDbContext ctx;
        public SikuyearService(ApplicationDbContext ctx) 
        {
        
            this.ctx = ctx;
        }
        public bool Add(Sikuyear model)
        {
            try
            {
                ctx.Sikuyear.Add(model);
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
                ctx.Sikuyear.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Sikuyear GetById(int id)
        {
            return ctx.Sikuyear.Find(id);
        }

        public IQueryable<Sikuyear> List()
        {
            var data= ctx.Sikuyear.AsQueryable();
            return data;
        }

        public bool Update(Sikuyear model)
        {
            try
            {
                ctx.Sikuyear.Update(model);
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
