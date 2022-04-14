import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';

@Injectable({
  providedIn: 'root',
})
export class UserServiceService {
  constructor(private commonService: CommonServiceService) {}
  public getCurrentUser(): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/identity/my-profile`;
    return this.commonService.returnHttpClientAuthorize(url);
  }
  public registerUser(data): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/user/register`;
    return this.commonService.postHttpClientNotAuthorize(url, data);
  }
  public updateUser(data): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/identity/my-profile`;
    return this.commonService.putHttpClient(url, data);
  }
  public changeProfilePicture(data): Observable<any> {
    console.log(1);
    const url = `${this.commonService.REST_API_SERVER}/api/app/user/upload`;
    return this.commonService.putHttpClient(url, data);
  }
  public forgotPassword(email): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/account/reset-password-request?email=${email}`;
    return this.commonService.postHttpClientNotAuthorize(url,email);
  }
  public resetPassword(data): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/account/reset-password`;
    console.log(url,data)
    return this.commonService.postHttpClientNotAuthorize(url,data);
  }
}
