import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private commonService:CommonServiceService) { }
  public getRoleByName(filter:string): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/identity/roles?Filter=${filter}`;
    return this.commonService.returnHttpClient(url);
}
}