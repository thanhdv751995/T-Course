using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Project.Contacts
{
    public class ContactManager : DomainService
    {
        private readonly IContactRepository _contactRepository;

        public ContactManager(IContactRepository contactReposittory)
        {
            _contactRepository = contactReposittory;
        }

        public async Task<Contact> CreateAsync(
            [NotNull] string descriptionPrimary,
            [AllowNull] string descriptionSub,

            [NotNull] string emailPrimary,
            [AllowNull] string emailSub,
            [NotNull] string phonePrimary,
            [AllowNull] string phoneSub,
            [NotNull] string addressPrimary,
            [AllowNull] string addressSub
            )
        {
            //var existingAuthor = await _authorRepository.FindByNameAsync(tenFile);
            return new Contact(
                GuidGenerator.Create(),
                descriptionPrimary,
                descriptionSub,
                emailPrimary,
                emailSub,
                phonePrimary,
                phoneSub,
                addressPrimary,
                addressSub
            );
        }
    }
}