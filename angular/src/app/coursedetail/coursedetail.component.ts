import { HttpClient } from '@angular/common/http';
import { JoinCourseServiceService } from './../ChildService/JoinCourse/join-course-service.service';
import { RateServiceService } from './../ChildService/Rate/rate-service.service';
import { LessonServiceService } from './../ChildService/Lesson/lesson-service.service';
import { CourseServiceService } from './../ChildService/Course/course-service.service';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import { ServerHttpService } from '../Services/server-http.service';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-coursedetail',
  templateUrl: './coursedetail.component.html',
  styleUrls: ['./coursedetail.component.scss']
})
export class CoursedetailComponent implements OnInit,AfterViewInit {
  public apisHost: string = environment.oAuthConfig.issuer
  public currentUrl;
  public lessons:any;
  public courses:any;
  public rates:any;
  public idRate:any;
  public listCourseByID:any;
  public star1=0;star2=0;star3=0;star4=0;star5=0;
  public totalRate=0;averageCount=0;
  public trustURL:any;
  public videoUrl:any;
  public flag=false;
  flagLesson=false;
  public isHaveLesson=false;
  public content:any;
  public countStartRate:number;
  public countForTrending=this.serverHttpService.countForTrending;
  public defaultLoad=10;
  public loadPage=this.defaultLoad;
  public currentUserLogin;
  public load="Xem Thêm";
  public create:boolean=true;
  stars: number[] = [1, 2, 3, 4, 5];
  selectedValue: any;
  fileToUpload: File = null;
  constructor(
    private router:ActivatedRoute,
    private rout:Router,
    private lessonService: LessonServiceService,
    private courseService:CourseServiceService,
    private rateService:RateServiceService,
    private serverHttpService:ServerHttpService,
    private joinCourseService:JoinCourseServiceService,
    private confirmation: ConfirmationService,
    private http:HttpClient,
    private toastr: ToastrService
  ) {
    
  }

  ngOnInit(): void {
    this.currentUrl=this.router.snapshot.params;
    this.currentUserLogin=this.serverHttpService.currentUserLogin;
    this.lessonService.getLessonByCourseID(this.currentUrl.id).subscribe((data)=>{
      this.lessons=data;
      if(data.totalCount>0)
      {
        this.trustURL=data.items[0].lessonURL;
        this.serverHttpService.convertVideoToSafe(this.trustURL);
        this.videoUrl=this.serverHttpService.videoUrl;
        this.isHaveLesson=true;
        this.flagLesson=true;
      }
      else{
        this.serverHttpService.convertVideoToSafe(undefined);
        this.videoUrl=this.serverHttpService.videoUrl;
        this.isHaveLesson=false;
        this.flagLesson=true;
      }
    })
    this.getCourseByID();
    this.getRate();
     

    this.serverHttpService.reloadRouter();
    }
    ngAfterViewInit():void{
      
    }
    countStar(star) {
     this.selectedValue = star;
     this.countStartRate=this.selectedValue;
    }

    createRate()
    {
      this.joinCourseService.CheckHasBeenBlock(this.currentUserLogin,this.currentUrl.id).subscribe((data)=>{
        if(!data){
          if(this.create){
            this.rateService.getExistRate(this.currentUrl.id,this.currentUserLogin).subscribe(data=>{
              if(!data){
                const rateData={ratePoint:this.countStartRate,content:this.content,idCourse:this.currentUrl.id,
                  idUser:this.currentUserLogin};
                  
                if(this.countStartRate>0&&this.content)
                {
                this.rateService.postRate(rateData).subscribe((data)=>
                {
                  this.reloadContent();
                })
              }
              else{
                this.toastr.warning('Input your rate?','Warning!');
              }
              }
              else this.toastr.error('Exist rate!','Error!');
            })
    
          }
        else{
          const rateData={
            ratePoint:this.countStartRate,
            content:this.content,
            idCourse:this.currentUrl.id,
            idUser:this.currentUserLogin
          };
          this.rateService.putRate(this.idRate,rateData).subscribe(()=>
          {
            this.reloadContent();
            this.create=true;
          })
        }
        }else this.toastr.error('You Has Been Banned To Access This Course','Error')!
      })
    }
    
    reloadContent(){
      this.content="";
      this.selectedValue=0;
      this.countStartRate=0;
      this.getCourseByID();
      this.getRate();
    }
    onDelete(idRate)
    {
      this.confirmation.warn('::Bạn chắc chắn xóa?', '::Chắc chắn xóa')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.rateService.deleteRate(idRate).subscribe(()=>{
            this.reloadContent();
          })
        }
      });
     
    }
    checkJoinCourse(){
      this.joinCourseService.CheckHasBeenBlock(this.currentUserLogin,this.currentUrl.id).subscribe((data)=>{
        if(!data){
          this.joinCourseService.getJoinCourse(this.currentUserLogin,this.currentUrl.id).subscribe((data)=>{
            if(!data){
              const joinCourseData={idUser:this.currentUserLogin,idCourse:this.currentUrl.id}
              this.joinCourseService.postJoinCourse(joinCourseData).subscribe();
            }
          })
          this.rout.navigate(['/learn/'+this.currentUrl.id+'/'+this.lessons.items[0].lessonID]);
        } else this.toastr.error('You Has Been Banned To Access This Course','Error')!
      })
    }
    // countStarMethod(item,number:Number,variable)
    // {
    //     if(item==number)
    //     {
    //       variable++;
    //     }
    // }
    getCourseByID()
    {
      this.courseService.getCourseByID(this.currentUrl.id).subscribe((data)=>{
        this.courses=data;
        this.flag=true;
         this.courseService.getCourseByCategoryID(this.courses?.idCategory).subscribe((data) => {
         this.listCourseByID = data;

         this.load="Xem Thêm";
      })
      })
    }
    loadMore()
  {
      this.rateService.getRateByCourseID(this.currentUrl.id,this.loadPage+this.defaultLoad).subscribe((data) => {
      this.loadPage+=this.defaultLoad;
      this.rates=data;
      if(this.loadPage>data.totalCount)
      {
        this.load="Nothing To Load";
      }
      });
  }
  EditRate(id: string){
   
      this.idRate=id;
    this.create=false;
    this.rateService.get(id).subscribe((data) => {
      this.selectedValue=data;
      this.content=data.content;
      this.countStartRate= data.ratePoint;
    });
  }
    getRate()
    {
     this.rateService.getRateByCourseID(this.currentUrl.id,this.defaultLoad).subscribe((data)=>{
       if(data){
       this.rates=data;
       
       this.star1=0;this.star2=0;this.star3=0;this.star4=0;this.star5=0;
       if(data.totalCount>0)
       {
         for(let i=0;i<data.totalCount;i++)
         {
           if(data.items[i]?.ratePoint==1)
           {
             this.star1++;
           }
           if(data.items[i]?.ratePoint==2)
           {
             this.star2++;
           }
           if(data.items[i]?.ratePoint==3)
           {
             this.star3++;
           }
           if(data.items[i]?.ratePoint==4)
           {
             this.star4++;
           }
           if(data.items[i]?.ratePoint==5)
           {
             this.star5++;
           }
         }
       }
     }
    })
    }
}



