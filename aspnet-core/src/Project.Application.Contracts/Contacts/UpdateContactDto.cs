using System.ComponentModel.DataAnnotations;

namespace Project.Contact
{
    public class UpdateContactDto
    {
        [Required]
        public string DescriptionPrimary { get; set; }

        [Required]
        public string EmailPrimary { get; set; }

        [Required]
        public string PhonePrimary { get; set; }

        [Required]
        public string AddressPrimary { get; set; }

        public string DescriptionSub { get; set; }
        public string EmailSub { get; set; }
        public string PhoneSub { get; set; }
        public string AddressSub { get; set; }
        public string Url { get; set; }
    }
}