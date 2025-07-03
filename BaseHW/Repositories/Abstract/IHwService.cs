using BaseHW.Models.Domain;
using BaseHW.Models.DTO;

namespace BaseHW.Repositories.Abstract
{
    public interface IHwService
    {

        bool Add(Hw model);

        bool Update(Hw model);
        Hw GetById(int id);

        bool Delete(int id);

        HwListVm List(string term = "", bool paging = false, int currentPage = 0);

        List<int> GetHwyearByHwId(int HwId);

    }
}
