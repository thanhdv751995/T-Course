import { NgbDateAdapter, NgbDateNativeAdapter } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { CreateUserDto, UpdateUserDto, UserDto, UserService } from '@proxy/user';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { UserServiceService } from 'src/app/ChildService/User/user-service.service';
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class UserComponent implements OnInit {
  public admin:any="admin";
  public currentUserLogin=JSON.parse(localStorage.getItem('CurrentUser'));
  user = { items: [], totalCount: 0 } as PagedResultDto<UserDto>;
  public SkipCount = 1;
  public MaxResultCount = 10;
  searchText;
  UserList:any;
  public currentUserLoginApi;
  isModalOpen = false;
  isModalOpen1 = false;
  form: FormGroup;

  selectedUser = {} as UserDto;
  selectedUserCreate = {} as CreateUserDto;
  selectedUserUpdate:string = '';
  constructor(
    public readonly list: ListService,
    private userService: UserService,
    private userService1: UserServiceService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
  ) { }

  ngOnInit(): void {
    const userStreamCreator = (query) => this.userService.getList(query);

    this.list.hookToQuery(userStreamCreator).subscribe((response) => {
      this.user = response;
    });
    if(this.currentUserLogin)
{
  this.userService1.getCurrentUser().subscribe((data)=>{
  this.currentUserLoginApi=data;
  })
}
  }
  createUser() {
    this.selectedUserCreate = {} as CreateUserDto;
    this.buildFormCreate();
    this.isModalOpen = true;
  }
  editUser(id: string){
      this.buildForm();
      this.isModalOpen1 = true;
  }
  buildForm() {
    this.form = this.fb.group({
      roleName: [this.selectedUserUpdate||Validators.required],
    });
  }
  buildFormCreate() {
    this.form = this.fb.group({
      password: [this.selectedUserCreate.password || '', Validators.required],
      userName: [this.selectedUserCreate.userName || '', Validators.required],
      name: [this.selectedUserCreate.name || '', Validators.required],
      email: [this.selectedUserCreate.email || '', Validators.required],
      dateOfBirth: [this.selectedUserCreate.dateOfBirth || '', Validators.required],
      avatar:[this.selectedUserCreate.avatar || '', Validators.required],
    });
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedUser.id) {
      let params: UpdateUserDto ={
        roleName :[this.form.value]
      }
      this.userService
        .update(this.selectedUser.id,params )
        .subscribe(() => {
          this.isModalOpen1 = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.userService.create(this.form.value).subscribe(() => {
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
          this.userService.delete(id).subscribe(() => this.list.get());
        }
      });
  }
  key = 'id';
  reserve: boolean = false;
  sort(key) {
    this.key = key;
    this.reserve = !this.reserve;
  }
}
