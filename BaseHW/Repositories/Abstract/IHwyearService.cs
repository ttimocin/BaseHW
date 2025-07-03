using BaseHW.Models.Domain;
using BaseHW.Models.DTO;

namespace BaseHW.Repositories.Abstract
{
    public interface IHwyearService
    {

        bool Add(Hwyear model);

        bool Update(Hwyear model);
        Hwyear GetById(int id);

        bool Delete(int id);

        IQueryable<Hwyear> List();


    }
}
