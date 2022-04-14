import { ProfileComponent } from './../../profile/profile.component';
import { JoinCourseServiceService } from './../../ChildService/JoinCourse/join-course-service.service';
import { CommonServiceService } from './../../Services/common-service.service';
import { ServerHttpService } from './../../Services/server-http.service';

import { Component, OnInit, EventEmitter, Input } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService} from '../../ChildService/Auth/auth-service.service';
import { UserServiceService } from 'src/app/ChildService/User/user-service.service';
import { localStor } from 'src/app/Services/local-storage.service';
import { environment } from '../../../environments/environment';
import { ToastrService } from 'ngx-toastr';
@Component({
selector: 'app-header',
templateUrl: './header.component.html',
styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
validateForm!: FormGroup;
public currentUserLogin=JSON.parse(localStorage.getItem('CurrentUser'));
public loginFalse=false;
public registerFalse=false;
public errorDetails:any;
public currentUserLoginApi;
public userName;
public apisHost: string = environment.oAuthConfig.issuer
public Name;
public Email;
public dateOfBirth;
public password;
public show:boolean=false;
public admin:any='admin';
public avatar="/attachments/avatar.jpg";
public listCourseUserJoin;
public numberCourseUserJoin:number;
public showModal: boolean
public isLoading:boolean=false;
public testUser;
constructor(private fb: FormBuilder, private authService: AuthService,
  private userService:UserServiceService,
  private serverService:ServerHttpService, private toastr: ToastrService,
  private joinCourseService:JoinCourseServiceService,) { }
ngOnInit(): void {

// localStorage.removeItem('abpSession');
if(this.currentUserLogin)
{
  this.userService.getCurrentUser().subscribe((data)=>{
  this.currentUserLoginApi=data;
  this.getListCourseUserJoin();
  })
}

this.validateForm = this.fb.group({
UserName: [null, [Validators.required]],
Password: [null, [Validators.required]]
});
}
logOut(){
  localStorage.clear();
  window.location.reload();
}

toggle() {
  this.show=true;
  this.show = !this.show;
  // CHANGE THE NAME OF THE BUTTON.
  
}
showSuccess(content,title) {
  this.toastr.success(content, title);
}
showError(err,notification) {
  this.toastr.error(err, notification);
}
Register(){
  this.isLoading=true;
  const registerUser={userName:this.userName,name:this.Name,email:this.Email,
    dateOfBirth:this.dateOfBirth,password:this.password,avatar:this.avatar};
    this.userService.registerUser(registerUser).subscribe((data)=>{
    if(data.success)
    {
      this.logIn(this.userName,this.password)
    }
  },(err)=>{
    this.isLoading=false;
    if(err){
      try{
    err.data.errors.forEach(element => {
      this.showError(element.description,err.success);
    });
  }catch{
    this.showError(err?.error?.message,err?.error?.details);
  }}
  })
  
}
getCountUserJoinCourse(){
  this.joinCourseService.getListCourseUserJoin(this.serverService.currentUserLogin).subscribe(data=>{
    if(data){
      this.numberCourseUserJoin=data.totalCount|0;
      
    }
  })
}
setLanguage(lang){
  localStor.setLanguage(lang);
  window.location.reload();
}
logIn(username,password){
  this.isLoading=true;
  this.authService.login(username, password).subscribe(() => {
    if(this.authService.currentUserLogin)
    {
      this.showSuccess('Login Successfully!','Welcome '+this.userName)

      window.location.reload();
    }
    },(err)=>{
      this.isLoading=false;
      if(err){
        console.log(err);
        // this.loginFalse=true;
        this.showError(err.error.data.error_description,err.error.data.success);
      }
    })
}
getListCourseUserJoin(){
  this.joinCourseService.getListCourseUserJoin(this.currentUserLogin.sub).subscribe(data=>{
    this.listCourseUserJoin=data;
    console.log(data)
  })
}
submitForm(form): void {
// tslint:disable-next-line:forin
for (const i in this.validateForm.controls) {
this.validateForm.controls[i].markAsDirty();
this.validateForm.controls[i].updateValueAndValidity();
}
const UserName = form.value.UserName;
const Password = form.value.Password;
this.logIn(UserName,Password)
,
error => {
console.log('Server Error', error);
}
}
showM()
{
  this.showModal = true; // Show-Hide Modal Check
  
}
//Bootstrap Modal Close event
hide()
{
  this.showModal = false;
}

@Input() updateActiveChange:any;
}
