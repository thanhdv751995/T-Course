using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Attachments
{
    public class UpdateAttachmentDto
    {
        public IFormFile File { get; set; }
    }
}
