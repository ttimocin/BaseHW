using BaseHW.Models.Domain;
using BaseHW.Models.DTO;

namespace BaseHW.Repositories.Abstract
{
    public interface ISikuyearService
    {

        bool Add(Sikuyear model);

        bool Update(Sikuyear model);
        Sikuyear GetById(int id);

        bool Delete(int id);

        IQueryable<Sikuyear> List();


    }
}
