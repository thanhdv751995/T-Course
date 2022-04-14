using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Project.BaiHocs
{
    public class Lesson : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid IDCourse { get; set; }

        private Lesson()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Lesson(
            Guid id,
            [NotNull] string name,
            [NotNull] string description,
            Guid idCourse)
            : base(id)
        {
            SetName(name);
            Description = description;
            IDCourse = idCourse;
        }

        internal Lesson ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string courceName)
        {
            Name = Check.NotNullOrWhiteSpace(
                courceName,
                nameof(courceName)
            );
        }
    }
}
   
