import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';

@Injectable({
  providedIn: 'root'
})
export class ContactServiceService {

  constructor(private commonService:CommonServiceService) { }
  public getContacts(): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/contact`;
    return this.commonService.returnHttpClient(url);
    }
}
