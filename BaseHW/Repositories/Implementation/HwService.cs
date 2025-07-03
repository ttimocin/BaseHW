using BaseHW.Models;
using BaseHW.Models.Domain;
using BaseHW.Models.DTO;
using BaseHW.Repositories.Abstract;

namespace BaseHW.Repositories.Implementation
{
    public class HwService : IHwService
    {
        private readonly ApplicationDbContext ctx;
        public HwService(ApplicationDbContext ctx) 
        {
        
            this.ctx = ctx;
        }
        public bool Add(Hw model)
        {
            try
            {

                ctx.Hw.Add(model);
                ctx.SaveChanges();
                foreach (int hwyearId in model.Hwyears)
                {
                    var hwHwyear = new HwHwyear
                    {
                        HwId = model.Id,
                        HwyearId = hwyearId
                    };
                    ctx.HwHwyear.Add(hwHwyear);
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
                var hwHwyears = ctx.HwHwyear.Where(a => a.HwId == data.Id);
                    foreach(var hwHwyear in hwHwyears)
                {
                    ctx.HwHwyear.Remove(hwHwyear);
                }
                ctx.Hw.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Hw GetById(int id)
        {
            return ctx.Hw.Find(id);
        }

        public HwListVm List(string term="", bool paging = false, int currentPage=0)
        {
            var data = new HwListVm();
      

            var list = ctx.Hw.ToList();
      
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
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

            foreach (var hw in list)
            {
                var hwyears = (from hwyear in ctx.Hwyear
                                 join mg in ctx.HwHwyear
                              on hwyear.Id equals mg.HwyearId
                               where mg.HwId == hw.Id
                              select hwyear.HwyearName
                              ).ToList();
                var hwyearNames = string.Join(',', hwyears);
                hw.HwyearNames = hwyearNames;
            }
     
            data.HwList = list.AsQueryable();
            return data;
        }

        public bool Update(Hw model)
        {
            try
            {
                var hwyearsToDeleted = ctx.HwHwyear.Where(a=> a.HwId == model.Id && !model.Hwyears.Contains(a.HwyearId)).ToList();
                foreach(var mHwyear in hwyearsToDeleted)
                {
                    ctx.HwHwyear.Remove(mHwyear);
                }
                foreach (int genId in model.Hwyears)
                {
                    var hwHwyear = ctx.HwHwyear.FirstOrDefault(a => a.HwId == model.Id && a.HwyearId == genId);
                    if (hwHwyear == null)
                    {
                        hwHwyear = new HwHwyear { HwyearId = genId, HwId = model.Id };
                        ctx.HwHwyear.Add(hwHwyear);
                    }
                }
                ctx.Hw.Update(model);
                
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public List<int> GetHwyearByHwId(int hwId)
        {
            var hwyearIds = ctx.HwHwyear.Where(a => a.HwId == hwId).Select(a => a.HwyearId).ToList();
            return hwyearIds;
        }

    }



}
