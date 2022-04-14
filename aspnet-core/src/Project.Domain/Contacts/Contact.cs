using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Project.Contacts
{
    public class Contact : AuditedAggregateRoot<Guid>
    {
        public string DescriptionPrimary { get; set; }
        public string DescriptionSub { get; set; }
        public string EmailPrimary { get; set; }
        public string EmailSub { get; set; }
        public string PhonePrimary { get; set; }
        public string PhoneSub { get; set; }
        public string AddressPrimary { get; set; }
        public string AddressSub { get; set; }
        private Contact()
        {
        }

        internal Contact(
            Guid id,
            [NotNull] string descriptionPrimary,
            [NotNull] string emailPrimary,
            [NotNull] string phonePrimary,
            [NotNull] string addressPrimary,
            [NotNull] string descriptionSub,
            [NotNull] string emailSub,
            [NotNull] string phoneSub,
            [NotNull] string addressSub
            ) : base(id)
        {
            DescriptionPrimary = descriptionPrimary;
            DescriptionSub = descriptionSub;
            EmailPrimary = emailPrimary;
            EmailSub = emailSub;
            PhonePrimary = phonePrimary;
            PhoneSub = phoneSub;
            AddressPrimary = addressPrimary;
            AddressSub = addressSub;
        }
    }
}