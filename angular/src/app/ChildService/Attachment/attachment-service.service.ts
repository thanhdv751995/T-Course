import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonServiceService } from 'src/app/Services/common-service.service';
import { ServerHttpService } from 'src/app/Services/server-http.service';

@Injectable({
  providedIn: 'root',
})
export class AttachmentService {
  constructor(private commonService: CommonServiceService, private serverHttp: ServerHttpService) {}
  public postAttachment(data): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/attachment`;
    return this.commonService.postHttpClient(url, data);
  }
  public putAttachment(Id, data): Observable<any> {
    const url = `${this.commonService.REST_API_SERVER}/api/app/attachment/${Id}`;
    return this.commonService.putHttpClient(url, data);
  }
}
