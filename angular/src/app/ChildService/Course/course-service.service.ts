import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';

@Injectable({
  providedIn: 'root'
})
export class CourseServiceService {

  constructor(private commonService:CommonServiceService) { }
  public getFullCourses(): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/course`;
    return this.commonService.returnHttpClient(url);
  }
  public getCourses(MaxResultCount): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/course?MaxResultCount=${MaxResultCount}`;
    return this.commonService.returnHttpClient(url);
  }
  public getCoursesMaxResultCount(MaxResultCount): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/course?MaxResultCount=${MaxResultCount}`;
    return this.commonService.returnHttpClient(url);
  }
  public getCoursesSkipCount(ResultSkipCount,MaxResultCount): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/course?SkipCount=${ResultSkipCount}&MaxResultCount=${MaxResultCount}`;
    return this.commonService.returnHttpClient(url);
  }
  public getCourseByID(courseID): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/course/${courseID}`;
    return this.commonService.returnHttpClient(url);
  }
  public getCourseByFilter(filter,MaxResultCount): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/course?Filter=${filter}&MaxResultCount=${MaxResultCount}`;
    return this.commonService.returnHttpClient(url);
  }
  public getCourseByCategoryID(categoryID): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/course/course-by-category-iD?ID=${categoryID}`;
    return this.commonService.returnHttpClient(url);
  }
  public Relative(IdUser): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/course/relative?idUser=${IdUser}`;
    return this.commonService.returnHttpClient(url);
  }
  public CheckPhoneNumberExist(Phone,IdUser):Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/course/check-phone-number-exist?PhoneNumber=${Phone}&ID=${IdUser}`;
    return this.commonService.returnHttpClient(url);
  }
}
