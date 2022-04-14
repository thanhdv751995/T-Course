using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.Contact
{
    public class ContactDto : EntityDto<Guid>
    {
        public string DescriptionPrimary { get; set; }
        public string EmailPrimary { get; set; }
        public string PhonePrimary { get; set; }
        public string AddressPrimary { get; set; }
        public string DescriptionSub { get; set; }
        public string EmailSub { get; set; }
        public string PhoneSub { get; set; }
        public string AddressSub { get; set; }
        public string URL { get; set; }
    }
}