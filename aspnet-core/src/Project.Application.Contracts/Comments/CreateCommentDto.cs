using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Comments
{
    public class CreateCommentDto
    {
        [Required]
        public string content { get; set; }
        public Guid? IDParent { get; set; }
        [Required]
        public Guid IDLesson { get; set; }
        [Required]
        public Guid IDUser { get; set; }
    }
}
