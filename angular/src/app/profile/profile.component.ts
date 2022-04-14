import { Router } from '@angular/router';
import { CourseServiceService } from './../ChildService/Course/course-service.service';

import { UserServiceService } from './../ChildService/User/user-service.service';
import { ServerHttpService } from 'src/app/Services/server-http.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  public currentUserLogin;
  public editForm: boolean = true;
  public apisHost: string = environment.oAuthConfig.issuer;
  fileToUpload: File = null;
  public fileName = 'Choose File';
  constructor(private userService: UserServiceService, protected http: HttpClient,private toastr: ToastrService,
    private serverHttpService:ServerHttpService,private courseService:CourseServiceService,private router:Router) {}
  ngOnInit(): void {
    this.editForm = false;
    this.getCurrentUser();
    
  }
  getCurrentUser() {
    if(this.serverHttpService.currentUserLogin){
    this.userService.getCurrentUser().subscribe(data => {
      this.currentUserLogin = data;
    });
  } else this.router.navigate(['/']);
  }

  updateProfile() {
    this.courseService.CheckPhoneNumberExist(this.currentUserLogin.phoneNumber,this.serverHttpService.currentUserLogin).subscribe(data=>{
      if(data){
        this.toastr.error('Số Điện Thoại này đã được dùng !!','Error');
      }else{
        this.userService.updateUser(this.currentUserLogin).subscribe(data => {
      this.currentUserLogin = data;
      this.editForm = false;
      if(data){
        this.toastr.success('Change Profile Successfully','Success!')
        // this.serverHttpService.redirectTo('/Profile')
        window.location.reload();
      }
    },(err)=>{
      this.toastr.error(err?.error.message,'Error');
    });
      }
    })
    

  }
  onClickEdit() {
    this.editForm = this.editForm == true ? false : true;
  }

  selectedFileInput(files: FileList) {
    this.fileToUpload = <File>files.item(0);
    this.fileName = files.item(0).name;
    const formD = new FormData();
    formD.append('file', this.fileToUpload);
    this.userService.changeProfilePicture(formD).subscribe(res => {
      this.currentUserLogin.extraProperties.Avatar = res['data']['url'];
      window.location.reload();
    });
    
  }

  async changeProfilePicture() {
    var formD = new FormData();
    formD.append('file', this.fileToUpload);
    this.userService.changeProfilePicture(formD).subscribe();
  }
}
