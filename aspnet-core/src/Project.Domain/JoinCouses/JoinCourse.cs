using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Project.ThamGiaKhoaHocs
{
    public class JoinCourse : AuditedAggregateRoot<Guid>
    {
        public Guid IDUser { get; set; }
        public Guid IDCourse { get; set; }
        public bool HasBeenLock { get; set; }
        private JoinCourse()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal JoinCourse(
            Guid id,
            Guid idCourse,
            bool hasBeenLock,
            Guid idUser)
            : base(id)
        {
            IDCourse = idCourse;
            IDUser = idUser;
            HasBeenLock = hasBeenLock;
        }
    }
}
