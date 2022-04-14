import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';

@Injectable({
  providedIn: 'root',
})
export class LessonServiceService {
  constructor(private commonService: CommonServiceService) {}
  public CheckExistAttachmentCommentByID(ID): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/attachment/${ID}/check-attachment-exist`;
    return this.commonService.returnHttpClient(url);
  }

  public getLessonByCourseID(courseID): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/course/lesson-by-iDCourse?ID=${courseID}`;
    return this.commonService.returnHttpClient(url);
  }

  public getLessonByID(lessonID): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/lesson/${lessonID}`;
    return this.commonService.returnHttpClient(url);
  }
}
