using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project.Comments
{
    public class CommentAppService_Tests : ProjectApplicationTestBase
    {
        private readonly ICommentAppService _commentAppService;
        public CommentAppService_Tests()
        {
            _commentAppService = GetRequiredService<ICommentAppService>();
        }
        [Fact]
        public async Task Should_Get_All_Authors_Without_Any_Filter()
        {
            var result = await _commentAppService.GetListAsync(new GetCommentListDto());

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(comment => comment.Content == "George Orwell");
            result.Items.ShouldContain(comment => comment.Content == "Douglas Adams");
        }
        [Fact]
        public async Task Should_Get_Filtered_Authors()
        {
            var result = await _commentAppService.GetListAsync(
                new GetCommentListDto { Filter = "George" });

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(comment => comment.Content == "George Orwell");
            result.Items.ShouldNotContain(comment => comment.Content == "Douglas Adams");
        }
        //[Fact]
        //public async Task Should_Create_A_New_Author()
        //{
        //    var authorDto = await _commentAppService.CreateAsync(
        //        new CreateCommentDto
        //        {
        //            content = "Edward Bellamy",
        //            IDLesson = ,
        //            Id = "Edward Bellamy was an American author..."
        //        }
        //    );

        //    authorDto.Id.ShouldNotBe(Guid.Empty);
        //    authorDto.Name.ShouldBe("Edward Bellamy");
        //}
    
    }
}
