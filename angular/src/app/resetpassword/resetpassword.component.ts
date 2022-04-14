import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserServiceService } from '../ChildService/User/user-service.service';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.scss']
})
export class ResetpasswordComponent implements OnInit {
  public token:any;
  public iduser:any;
  public newpass:any;
  public currentUrl;
  public checkpass:any;

  constructor(
    private router:ActivatedRoute,
    private toastr: ToastrService,
    private UserService: UserServiceService,
    private rout: Router
  ) { }

  ngOnInit(): void {
    this.currentUrl=this.router.snapshot.params;
    this.iduser=this.currentUrl.userid;
    this.token=this.currentUrl.token;
  }
  
  resetpassword(){
    if(this.newpass===this.checkpass){
      const datareset={
        userId:this.iduser,
        token:this.token,
        newPassword:this.newpass
      }

      this.UserService.resetPassword(datareset).subscribe(()=>{
        
        this.toastr.info('Thay đổi mật khẩu thành công','Thông Báo!');
        this.rout.navigate(['/']);
      },err=>{
        console.log(err)
        if(err){
          this.toastr.error('Lỗi Khi thay Đổi Mật Khẩu',err.error.message);
        }
     });
    }
   else{
     this.toastr.error('Nhập mật khẩu không khớp');
   }
  }

}
