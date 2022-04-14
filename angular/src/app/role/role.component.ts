import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { RoleService, RoleDto } from '@proxy/phan-quyens';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss'],
  providers: [ListService],
})
export class RoleComponent implements OnInit {
  role = { items: [], totalCount: 0 } as PagedResultDto<RoleDto>;
  selectedRole = {} as RoleDto; // declare selectedBook
  form: FormGroup; // add this line
  isModalOpen = false;

  constructor(public readonly list: ListService, private RoleService: RoleService, private fb: FormBuilder
    , private confirmation: ConfirmationService) { }

  ngOnInit(): void {
    const roleStreamCreator = (query) => this.RoleService.getList(query);

    this.list.hookToQuery(roleStreamCreator).subscribe((response) => {
      this.role = response;
    });
  }
  createRole() {
    this.selectedRole = {} as RoleDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editRole(id: string) {
    this.RoleService.get(id).subscribe((role) => {
      this.selectedRole = role;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.RoleService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
  buildForm() {
    this.form = this.fb.group({
      name: ['', Validators.required],
    });
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedRole.id
      ? this.RoleService.update(this.selectedRole.id, this.form.value)
      : this.RoleService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
