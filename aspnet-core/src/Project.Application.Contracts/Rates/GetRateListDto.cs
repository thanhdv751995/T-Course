using Volo.Abp.Application.Dtos;

namespace Project.Rates
{
    public class GetRateListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}