using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Project.DuLieu
{
    public class Role : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

    }
}
