using Project.Contact;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Entities;
using Project.Attachments;

namespace Project.Contacts
{
    public class ContactAppService : ProjectAppService, IContactAppService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ContactManager _contactManager;
        private readonly AttachmentManager _attManager;
        private readonly IRepository<Attachment, Guid> _attachmentRepository;

        //private readonly IroleRepository _roleRepository;

        public ContactAppService(
            IContactRepository contactRepository,
            ContactManager contactManager,
            IAttachmentRepository attachmentRepository,
            AttachmentManager attManager
            //IroleRepository roleRepository,
            )
        {
            _contactRepository = contactRepository;
            _contactManager = contactManager;
            _attachmentRepository = attachmentRepository;
            _attManager = attManager;
        }

        public async Task<ContactDto> CreateAsync(CreateContactDto input)
        {
            var contact = await _contactManager.CreateAsync(
                input.DescriptionPrimary,
                input.DescriptionSub,
                input.EmailPrimary,
                input.EmailSub,
                input.PhonePrimary,
                input.PhoneSub,
                input.AddressPrimary,
                input.AddressSub

            );

            await _contactRepository.InsertAsync(contact);
            var att = await _attManager.CreateAsync(
                 input.URL,
                 contact.Id
    ); ;

            await _attachmentRepository.InsertAsync(att);
            return ObjectMapper.Map<Contact, ContactDto>(contact);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _contactRepository.DeleteAsync(id);
            var att = await _attachmentRepository.FindAsync(x => x.IDTable == id);
            await _attachmentRepository.DeleteAsync(att.Id);
        }

        public async Task<ContactDto> GetAsync(Guid id)
        {
            //var contact = await _contactRepository.GetAsync(id);
            //return ObjectMapper.Map<Contact, ContactDto>(contact);
            var queryable = await _contactRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from attachment in _attachmentRepository
                        join contact in queryable on attachment.IDTable equals contact.Id
                        where contact.Id == id
                        select new { contact, attachment };

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Contact), id);
            }

            var contactDto = ObjectMapper.Map<Contact, ContactDto>(queryResult.contact);
            contactDto.URL = queryResult.attachment.URL;
            return contactDto;
        }

        public async Task<PagedResultDto<ContactDto>> GetListAsync(GetContactListDto input)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await _contactRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from contact in queryable
                        join attachment in _attachmentRepository on contact.Id equals attachment.IDTable
                        orderby input.Sorting
                        select new { contact, attachment };
            //Paging
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var contactDtos = queryResult.Select(x =>
            {
                var contactDto = ObjectMapper.Map<Contact, ContactDto>(x.contact);
                contactDto.DescriptionPrimary = x.contact.DescriptionPrimary;
                contactDto.URL = x.attachment.URL;
                contactDto.EmailPrimary = x.contact.EmailPrimary;
                contactDto.PhonePrimary = x.contact.PhonePrimary;
                contactDto.AddressPrimary = x.contact.AddressPrimary;
                contactDto.DescriptionSub = x.contact.DescriptionSub;
                contactDto.EmailSub = x.contact.EmailSub;
                contactDto.PhoneSub = x.contact.PhoneSub;
                contactDto.AddressSub = x.contact.AddressSub;
                return contactDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _contactRepository.GetCountAsync();

            return new PagedResultDto<ContactDto>(
                totalCount,
                contactDtos
            );
        }

        public async Task UpdateAsync(Guid id, UpdateContactDto input)
        {
            var contact = await _contactRepository.GetAsync(id);
            var att = await _attachmentRepository.FindAsync(x => x.IDTable == id);

            contact.DescriptionPrimary = input.DescriptionPrimary;
            contact.DescriptionPrimary = input.DescriptionPrimary;
            contact.EmailPrimary = input.EmailPrimary;
            contact.EmailSub = input.EmailSub;
            contact.PhonePrimary = input.PhonePrimary;
            contact.PhoneSub = input.PhoneSub;
            contact.AddressPrimary = input.AddressPrimary;
            contact.AddressSub = input.AddressSub;
            att.URL = input.Url;

            await _contactRepository.UpdateAsync(contact);
            await _attachmentRepository.UpdateAsync(att);
        }
    }
}