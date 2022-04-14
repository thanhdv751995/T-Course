import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '@proxy/user';
import { ToastrService } from 'ngx-toastr';
import { UserServiceService } from '../ChildService/User/user-service.service';

@Component({
  selector: 'app-forgotpass',
  templateUrl: './forgotpass.component.html',
  styleUrls: ['./forgotpass.component.scss']
})
export class ForgotpassComponent implements OnInit {

  public email: any;
  public currentQueryUrl;
  public isLoading:boolean=false;
  constructor(
    private UserService: UserServiceService,
    private router:ActivatedRoute,
    private toastr: ToastrService,
    private rout: Router
  ) { }

  ngOnInit(): void {
    this.currentQueryUrl=this.router.snapshot.queryParams;

  }

  registerEmail(){
    this.isLoading=true;
    this.UserService.forgotPassword(this.email).subscribe(()=>{
       this.toastr.info('Vui lòng kiểm tra Mail của bạn','Thông Báo!');
       this.rout.navigate(['/']);
       this.isLoading=false;
    },err=>{
      this.toastr.error(err?.error.message,'Nhập lỗi');
      this.isLoading=false;
    });
  }
}
