import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';

@Injectable({
  providedIn: 'root'
})
export class JoinCourseServiceService {

  constructor(
    private commonService:CommonServiceService
  ) { }
  public getJoinCourse(idUser,idCourse): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/join-course/join-course?idUser=${idUser}&idCourse=${idCourse}`;
    return this.commonService.returnHttpClient(url);
    }
    public CheckHasBeenBlock(idUser,idCourse): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/join-course/check-has-been-lock-join-course?idUser=${idUser}&idCourse=${idCourse}`;
    return this.commonService.returnHttpClient(url);
    }
    public getJoinCourseEver(idUser): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/join-course/check-ever-join-course?idUser=${idUser}`;
    return this.commonService.returnHttpClient(url);
    }
  public deleteJoinCourse(id): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/join-course/${id}`;
    return this.commonService.deleteHttpClient(url);
    } 
    public getListCourseUserJoin(idUser): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/join-course/course-user-join?idUser=${idUser}`;
    return this.commonService.returnHttpClient(url);
    }
    public postJoinCourse(data): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/join-course`;
    return this.commonService.postHttpClient(url,data);
    }
    public setHasBeenLock(id,data): Observable<any> {
      const url = `${this.commonService.REST_API_SERVER}/api/app/join-course/${id}`;
      return this.commonService.putHttpClient(url,data);
      }
    public GetJoinCourseStudent(idCourse): Observable<any> {
      const url = `${this.commonService.REST_API_SERVER}/api/app/join-course/user-join-course?IDCourse=${idCourse}`;
      return this.commonService.returnHttpClient(url);
      }
}
