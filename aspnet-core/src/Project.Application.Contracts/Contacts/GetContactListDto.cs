using Volo.Abp.Application.Dtos;

namespace Project.Contact
{
    public class GetContactListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}