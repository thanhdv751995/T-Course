﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Project.Comments
{
    public interface ICommentRepository : IRepository<Comment, Guid>
    {
        Task<List<Comment>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string filter = null
        );
    }
}
