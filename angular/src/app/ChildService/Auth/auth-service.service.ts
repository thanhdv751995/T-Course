import { UserServiceService } from './../User/user-service.service';
import { localStor } from './../../Services/local-storage.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import jwt_decode from "jwt-decode";
import { UserDto } from '@proxy/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private currentUserSubject: BehaviorSubject<UserDto>;
  public currentUserLogin;
  public Token: string;
  constructor(
    protected http: HttpClient,

    ) { }

  login(UserName: string, Password: string): Observable<any> {
    return this.http.post(`${environment.apis.default.url}/api/app/user/login`, {
      UserName,
      Password
    }).pipe(map((userLogin) => {
        if (userLogin){
          this.currentUserLogin=userLogin;
          var token = userLogin["data"][`access_token`];
          var decoded = jwt_decode(token);
          // var decodedHeader = jwt_decode(token, { header: true });
          localStorage.setItem('CurrentUser', JSON.stringify(decoded));
          localStor.writeAccessToken(userLogin["data"][`access_token`]);
          localStor.writeRefreshToken(userLogin["data"][`refresh_token`]);
          // TODO request user

        }
        else{
          console.log("error");
          alert('Sai mat khau')
          // this.logOut();
        }
    }));
  }
  public get currentUserValue(): UserDto {
    this.currentUserSubject = new BehaviorSubject<UserDto>(JSON.parse(localStorage.getItem('CurrentUser')));
    return this.currentUserSubject.value;
}
}
