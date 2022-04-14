import { ServerHttpService } from './../../Services/server-http.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryServiceService {

  constructor(
    private commonService:CommonServiceService,
    private serverHttp:ServerHttpService
    ) { }
  public getCategory(): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}${this.serverHttp.lookupCategoryLinkApi}`;
    return this.commonService.returnHttpClient(url);
  }
  public getCats(): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}${this.serverHttp.getCategory}`;
    return this.commonService.returnHttpClient(url);
  }
}
