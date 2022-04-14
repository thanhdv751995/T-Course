using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Project.DanhMucs
{
    public class Category : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? IDParent { get; set; }

        private Category()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Category(
            Guid id,
            [NotNull] string name,
            [NotNull] string description,
            Guid? idParent)
            : base(id)
        {
            SetName(name);
            Description = description;
            IDParent = idParent;
        }
        internal Category ChangeName([NotNull] string categoryName)
        {
            SetName(categoryName);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name)
            );
        }
    }
    
}
