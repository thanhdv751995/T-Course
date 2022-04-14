using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Project.KhoaHocs
{
    public class Course : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Benefit { get; set; }
        public Guid IDCategory { get; set; }
        public Guid IDUser { get; set; }
        private Course()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Course(
            Guid id,
            [NotNull] string name,
            [NotNull] string description,
            [NotNull] string benefit,
            Guid idCategory,
            Guid idUser)
            : base(id)
        {
            SetName(name);
            Description = description;
            Benefit = benefit;
            IDCategory = idCategory;
            IDUser = idUser;
        }
        internal Course ChangeName([NotNull] string courceName)
        {
            SetName(courceName);
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
    //test
}
