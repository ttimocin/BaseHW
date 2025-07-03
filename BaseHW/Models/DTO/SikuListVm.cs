using BaseHW.Models.Domain;

namespace BaseHW.Models.DTO
{
    public class SikuListVm
    {
        public IQueryable<Siku>? SikuList { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }

}

