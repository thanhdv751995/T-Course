import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { ContactService } from '@proxy/contacts';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ContactDto} from '@proxy/contact';
@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class ContactComponent implements OnInit {
  public SkipCount = 1;
  public MaxResultCount = 10;
  searchText;

  contact = { items: [], totalCount: 0 } as PagedResultDto<ContactDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedContact = {} as ContactDto;

  constructor(
    public readonly list: ListService,
    private ContactService: ContactService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService) {
  }

  ngOnInit(): void {
    const contactStreamCreator = (query) => this.ContactService.getList(query);

    this.list.hookToQuery(contactStreamCreator).subscribe((response) => {
      this.contact = response;
      console.log(this.contact)
    });
  }
  createContact() {
    this.selectedContact = {} as ContactDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editContact(id: string) {
    this.ContactService.get(id).subscribe((contact) => {
      this.selectedContact = contact;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      descriptionPrimary: [this.selectedContact.descriptionPrimary || '', null],
      emailPrimary: [this.selectedContact.emailPrimary || '', null],
      phonePrimary: [this.selectedContact.phonePrimary || null],
      addressPrimary: [this.selectedContact.addressPrimary || null],
      descriptionSub: [this.selectedContact.descriptionSub || null],
      emailSub: [this.selectedContact.emailSub || null],
      phoneSub: [this.selectedContact.phoneSub || null],
      addressSub: [this.selectedContact.addressSub || null],
      url: [this.selectedContact.url || null],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedContact.id) {
      this.ContactService
        .update(this.selectedContact.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.ContactService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.ContactService.delete(id).subscribe(() => this.list.get());
        }
      });
  }

  //sorting
  key = 'id';
  reserve: boolean = false;
  sort(key) {
    this.key = key;
    this.reserve = !this.reserve;
  }

}
