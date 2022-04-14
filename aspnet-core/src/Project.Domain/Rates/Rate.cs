using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Project.DanhGias
{
    public class Rate : AuditedAggregateRoot<Guid>
    {
        public int RatePoint { get; set; }
        public string Content { get; set; }
        public Guid IDCourse { get; set; }
        public Guid IDUser { get; set; }

        private Rate()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Rate(
            Guid id,
            [NotNull] int ratepoint,
            [NotNull] string content,
            Guid courseid,
            Guid userid)
            : base(id)
        {
            RatePoint = ratepoint;
            Content = content;
            IDCourse = courseid;
            IDUser = userid;
        }
    }
}