using BaseHW.Models.Domain;
using BaseHW.Models.DTO;

namespace BaseHW.Repositories.Abstract
{
    public interface ISikuService
    {

        bool Add(Siku model);

        bool Update(Siku model);
        Siku GetById(int id);

        bool Delete(int id);

        SikuListVm List(string term = "", bool paging = false, int currentPage = 0);

        List<int> GetSikuyearBySikuId(int sikuId);

    }
}
