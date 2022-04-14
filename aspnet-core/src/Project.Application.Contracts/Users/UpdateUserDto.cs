using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Project.NguoiDungs
{
    public class UpdateUserDto : ExtensibleObject
    {
        public UpdateUserDto() { }

        [DynamicStringLength(typeof(IdentityUserConsts), "MaxUserNameLength", null)]
        public string UserName { get; set; }
        [DynamicStringLength(typeof(IdentityUserConsts), "MaxEmailLength", null)]
        public string Email { get; set; }
        [DynamicStringLength(typeof(IdentityUserConsts), "MaxNameLength", null)]
        public string Name { get; set; }
        [DynamicStringLength(typeof(IdentityUserConsts), "MaxSurnameLength", null)]
        public string Surname { get; set; }
        [DynamicStringLength(typeof(IdentityUserConsts), "MaxPhoneNumberLength", null)]
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
    }
}
