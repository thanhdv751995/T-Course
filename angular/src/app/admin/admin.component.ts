import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../ChildService/User/user-service.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  public currentUserLogin=JSON.parse(localStorage.getItem('CurrentUser'));
  public loginFalse;
  public currentUserLoginApi;
  public apisHost: string = environment.oAuthConfig.issuer

  constructor(private userService:UserServiceService,private router:Router) { }

  ngOnInit(): void {
    if(this.currentUserLogin)
{
  this.userService.getCurrentUser().subscribe((data)=>{
  this.currentUserLoginApi=data;
  })
}

  }
  logOut(){
    if(confirm("Are you sure to logout ")) {
      console.log("ok");
    localStorage.clear();
    this.router.navigate(['']);
  }
  }
}
