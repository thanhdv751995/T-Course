import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';
import { ServerHttpService } from 'src/app/Services/server-http.service';

@Injectable({
  providedIn: 'root'
})
export class CommentServiceService {

  constructor(
    private commonService:CommonServiceService,
    private serverHttp:ServerHttpService
  ) { }
  public getCommentByIDLesson(lessonID,MaxResultCount): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/comment/comment-by-iDlesson?MaxResultCount=${MaxResultCount}&ID=${lessonID}`;
    return this.commonService.returnHttpClient(url);
  }
  public postComment(data): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/comment`;
    return this.commonService.postHttpClient(url,data);
  }
  public getCountComment(): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/comment/count`;
    return this.commonService.returnHttpClient(url);
  }
  public putComment(commentID,data): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/comment/${commentID}`;
    return this.commonService.putHttpClient(url,data);
}
  public getIDChildByIDParent(CommentID): Observable<any>{
    const url = `${this.commonService.REST_API_SERVER}/api/app/comment/i-dChild-by-iDPrent?ID=${CommentID}`;
    return this.commonService.returnHttpClient(url);
  }
}