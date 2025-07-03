using BaseHW.Models;
using BaseHW.Models.Domain;
using BaseHW.Models.DTO;
using BaseHW.Repositories.Abstract;
using Humanizer;

namespace BaseHW.Repositories.Implementation
{
    public class SikuService : ISikuService
    {
        private readonly ApplicationDbContext ctx;
        public SikuService(ApplicationDbContext ctx) 
        {
        
            this.ctx = ctx;
        }
        public bool Add(Siku model)
        {
            try
            {

                ctx.Siku.Add(model);
                ctx.SaveChanges();
                foreach (int sikuyearId in model.Sikuyears)
                {
                    var sikuSikuyear = new SikuSikuyear
                    {
                        SikuId = model.Id,
                        SikuyearId = sikuyearId
                    };
                    ctx.SikuSikuyear.Add(sikuSikuyear);
                }
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
                var sikuSikuyears = ctx.SikuSikuyear.Where(a => a.SikuId == data.Id);
                    foreach(var sikuSikuyear in sikuSikuyears)
                {
                    ctx.SikuSikuyear.Remove(sikuSikuyear);
                }
                ctx.Siku.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Siku GetById(int id)
        {
            return ctx.Siku.Find(id);
        }

        public SikuListVm List(string term="", bool paging = false, int currentPage=0)
        {
            var data = new SikuListVm();
      

            var list = ctx.Siku.ToList();
      
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().Contains(term)).ToList();

            }
         
  

            if (paging)
            {
                int pageSize = 10;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach (var siku in list)
            {
                var sikuyears = (from sikuyear in ctx.Sikuyear
                              join mg in ctx.SikuSikuyear
                              on sikuyear.Id equals mg.SikuyearId
                                 where mg.SikuId == siku.Id
                              select sikuyear.SikuyearName
                              ).ToList();
                var sikuyearNames = string.Join(',', sikuyears);
                siku.SikuyearNames = sikuyearNames;
            }
     
            data.SikuList = list.AsQueryable();
            return data;
        }

        public bool Update(Siku model)
        {
            try
            {
                var sikuyearsToDeleted = ctx.SikuSikuyear.Where(a=> a.SikuId == model.Id && !model.Sikuyears.Contains(a.SikuyearId)).ToList();
                foreach(var mSikuyear in sikuyearsToDeleted)
                {
                    ctx.SikuSikuyear.Remove(mSikuyear);
                }
                foreach (int genId in model.Sikuyears)
                {
                    var sikuSikuyear = ctx.SikuSikuyear.FirstOrDefault(a => a.SikuId == model.Id && a.SikuyearId == genId);
                    if (sikuSikuyear == null)
                    {
                        sikuSikuyear = new SikuSikuyear { SikuyearId = genId, SikuId = model.Id };
                        ctx.SikuSikuyear.Add(sikuSikuyear);
                    }
                }
                ctx.Siku.Update(model);
                
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public List<int> GetSikuyearBySikuId(int sikuId)
        {
            var sikuyearIds = ctx.SikuSikuyear.Where(a => a.SikuId == sikuId).Select(a => a.SikuyearId).ToList();
            return sikuyearIds;
        }

    }



}
