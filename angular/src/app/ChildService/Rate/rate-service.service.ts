import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';

@Injectable({
  providedIn: 'root'
})
export class RateServiceService {

  constructor(private commonService:CommonServiceService) { }
  
  public getRateByCourseID(courseID,MaxResultCount): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/rate/rate-by-iDCourse?ID=${courseID}&MaxResultCount=${MaxResultCount}`;
    return this.commonService.returnHttpClient(url);
  }
  public getExistRate(courseID,IdUser): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/rate/exist-rate?IdCourse=${courseID}&IdUser=${IdUser}`;
    return this.commonService.returnHttpClient(url);
  }
  public postRate(data): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/rate`;
    return this.commonService.postHttpClient(url,data);
  }
  public getCountRate():Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/rate/count`;
    return this.commonService.returnHttpClient(url);
  }
  public getUserCount():Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/rate/user-count`;
    return this.commonService.returnHttpClient(url);
  }
  public deleteRate(idRate): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/rate/${idRate}`;
    return this.commonService.deleteHttpClient(url);
  }
  public putRate(idRate,data): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/rate/${idRate}`;
    return this.commonService.putHttpClient(url,data);
  }
  public get(id): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/rate/${id}`;
    return this.commonService.returnHttpClient(url);
  }
}
