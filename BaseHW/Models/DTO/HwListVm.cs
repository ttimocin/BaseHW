using BaseHW.Models.Domain;

namespace BaseHW.Models.DTO
{
    public class HwListVm
    {
        public IQueryable<Hw> HwList { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }

}

