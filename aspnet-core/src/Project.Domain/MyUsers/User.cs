using JetBrains.Annotations;
using Project.NguoiDung;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Project.NguoiDungs
{
    public class User : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid IDRole { get; set; }
        private User()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal User(
            Guid id,
            [NotNull] string name,
            DateTime birhday,
            string email,
            string phone,
            string username,
            string password,
            Guid idrole)
            : base(id)
        {
            SetName(name);
            Birthday = birhday;
            Email = email;
            Phone = phone;
            Username = username;
            Password = password;
            IDRole = idrole;
        }
        internal User ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: UserConst.MaxNameLength
            );
        }
    }
     
}
