using Project.PhanQuyens;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace Project.PhanQuyen
{
    public class RoleAppService_test : ProjectApplicationTestBase
    {
        private readonly IRoleAppService _roleAppService;

        public RoleAppService_test()
        {
            _roleAppService = GetRequiredService<IRoleAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Roles()
        {
            //Act
            var result = await _roleAppService.GetListAsync(
                new PagedAndSortedResultRequestDto()
            );

            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "AdminRole");
        }
        [Fact]
        public async Task Should_Create_A_Valid_Role()
        {
            //Act
            var result = await _roleAppService.CreateAsync(
                new CreateUpdateRoleDto
                {
                    Name = "New test",
                }
            );

            //Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("New test");
        }
        [Fact]
        public async Task Should_Not_Create_A_Role_Without_Name()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _roleAppService.CreateAsync(
                    new CreateUpdateRoleDto
                    {
                        Name = "",
                    }
                );
            });

            exception.ValidationErrors
                .ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
        }
    }
}
