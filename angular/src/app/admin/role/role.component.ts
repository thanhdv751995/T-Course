import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { RoleDto, RoleService } from '@proxy/role';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class RoleComponent implements OnInit {
  role = { items: [], totalCount: 0 } as PagedResultDto<RoleDto>;
  public SkipCount = 1;
  public MaxResultCount = 10;
  searchText;
  RoleList: any;

  isModalOpen = false;
  isModalOpen1 = false;

  form: FormGroup;

  selectedRole = {} as RoleDto;

  constructor(
    public readonly list: ListService,
    private roleService: RoleService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
  ) { }

  ngOnInit(): void {
    const roleStreamCreator = (query) => this.roleService.getList(query);

    this.list.hookToQuery(roleStreamCreator).subscribe((response) => {
      this.role = response;
    });
  }
  createRole() {
    this.selectedRole = {} as RoleDto;
    this.buildForm1();
    this.isModalOpen1 = true;
  }
  editRole(id: string) {
    this.roleService.get(id).subscribe((role) => {
      this.selectedRole = role;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.roleService.delete(id).subscribe(() => this.list.get());
        }
      });
  }
  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedRole.name || '', Validators.required],
      concurrencyStamp: [this.selectedRole.concurrencyStamp || null],
    });
  }
  buildForm1() {
    this.form = this.fb.group({
      name: [this.selectedRole.name || '', Validators.required],
    });
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedRole.id) {
      this.roleService
        .update(this.selectedRole.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.roleService.create(this.form.value).subscribe(() => {
        this.isModalOpen1= false;
        this.form.reset();
        this.list.get();
      });
    }
  }
  key = 'id';
  reserve: boolean = false;
  sort(key) {
    this.key = key;
    this.reserve = !this.reserve;
  }
}
