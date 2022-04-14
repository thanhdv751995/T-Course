import { CommentServiceService } from './../ChildService/Comment/comment-service.service';
import { RateServiceService } from './../ChildService/Rate/rate-service.service';
import { ServerHttpService } from './../Services/server-http.service';
import { RoleService } from './../ChildService/Role/role.service';
import { CourseServiceService } from './../ChildService/Course/course-service.service';

import { AuthService } from '@abp/ng.core';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { localStor } from '../Services/local-storage.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls:['./home.component.scss']
})
  
export class HomeComponent implements OnInit, AfterViewInit {
  public courses: any;
  public contacts:any;
  public userCount:any;
  public countTeacherRole:any;
  public countStudentRole:any;
  public coursesShow:any;
  public defaultLoad=8;
  public loadPage=this.defaultLoad;
  public load="Xem Thêm";
  public RatesCount:any;
  public CommentsCount:any;
  public dataCountJoinCourse:number[]=[];
  public dataCourseName:string[]=[];
  public countForTrending=this.serverHttpService.countForTrending;
  
  

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(private oAuthService: OAuthService,
    private authService: AuthService,
    private serverHttpService:ServerHttpService,

    private courseService:CourseServiceService,
    private rateService:RateServiceService,
    private commentService:CommentServiceService
  ) { }
  ngOnInit(): void {
    
    this.courseService.getCourses(4).subscribe((data) => {
      this.courses = data;
    });

    this.courseService.getCourses(this.defaultLoad).subscribe((data) => {
      this.coursesShow = data;
    });
    this.rateService.getCountRate().subscribe((data)=>{
      this.RatesCount=data;
    })
    this.rateService.getUserCount().subscribe((data)=>{
      this.userCount=data??0;
    })
    this.commentService.getCountComment().subscribe((data)=>{
      this.CommentsCount=data;
    })

  }
  getFullCourse(){
      this.courseService.getFullCourses().subscribe(data=>{
      if(data){
      data.items.forEach(element => {
        this.dataCountJoinCourse.push(element.countStudent);
        this.dataCourseName.push(element.name);
      });
    }
    })
  }
  loadMore()
  {
      this.courseService.getCourses(this.loadPage+this.defaultLoad).subscribe((data) => {
      this.loadPage+=this.defaultLoad;
      this.coursesShow = data;
      if(this.loadPage>data.totalCount)
      {
        this.load="Nothing To Load";
      }
      });
  }
  ngAfterViewInit() {
    this.getFullCourse();
  }

  login() {
    this.authService.initLogin();
  }

}
