using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project.PhanQuyens
{
    public interface IRoleAppService :
        ICrudAppService< //Defines CRUD methods
            RoleDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateRoleDto> //Used to create/update a book
    {

    }
}
