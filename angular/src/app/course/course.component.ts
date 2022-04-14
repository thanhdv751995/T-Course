import { JoinCourseServiceService } from './../ChildService/JoinCourse/join-course-service.service';
import { CourseServiceService } from './../ChildService/Course/course-service.service';
import { CategoryServiceService } from './../ChildService/Category/category-service.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { CourseService, CourseDto, CategoryLookupDto, UserLookupDto } from '../proxy/khoa-hocs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Observable } from 'rxjs';
import { ServerHttpService } from '../Services/server-http.service';
import { ActivatedRoute, Router } from '@angular/router';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class CourseComponent implements OnInit,OnDestroy {
  public showModal: boolean
  public currentQueryUrl;
  public listPageNumberTotal=[];
  public totalPageNumberOfCourse:any;
  public currentPage:any;
  loadSameRouter: any;
  public paramUrl:any;
  public totalCourse:any;
  public StudentJoinCourse:any;
  public coursesCategory:any;
  public category:any;
  courses = { items: [], totalCount: 0 } as PagedResultDto<CourseDto>;
  public coursesMaxResultCount: any;
  isModalOpen = false;
  public searchParams:any;
  public countForTrending=this.serverHttp.countForTrending;
  public currentUserLogin;
  public currentUserRole;
  public checkRoleAdmin:boolean=false;
  searchText;
  form: FormGroup;

  selectedCourse = {} as CourseDto;
  categories$: Observable<CategoryLookupDto[]>;
  users$: Observable<UserLookupDto[]>;
  constructor(
    public readonly list: ListService,
    private fb: FormBuilder,
    private courseService:CourseService,
    private confirmation: ConfirmationService,
    private serverHttp: ServerHttpService,
    private router:ActivatedRoute,
    private categoryService:CategoryServiceService,
    private courseServiceApi:CourseServiceService,
    private joinCourseService:JoinCourseServiceService,
    private toastr: ToastrService
    ) { 
  }

  ngOnInit(): void {
    this.currentQueryUrl=this.router.snapshot.queryParams;
    this.currentPage=parseInt(this.currentQueryUrl.Page);
    this.searchParams=this.router.snapshot.queryParams.Search;
    this.paramUrl=this.router.snapshot.params;
    this.currentUserLogin=this.serverHttp.currentUserLogin;
    this.currentUserRole=this.serverHttp.currentUserRole;
    const courseStreamCreator = (query) => this.courseService.getList(query);
    if(isNaN(this.currentPage)||this.currentPage<=1)
    {
      this.currentPage=1;
    }
    if(this.currentQueryUrl.Page)
    {
      this.courseServiceApi.getCoursesSkipCount((this.currentQueryUrl.Page-1)*6,6).subscribe((data) => {
      this.coursesMaxResultCount = data;
      if(this.coursesMaxResultCount)
      {
        this.serverHttp.Pagination(this.coursesMaxResultCount,this.listPageNumberTotal,6);
        this.totalPageNumberOfCourse=this.serverHttp.totalPageNumberOfCourse;
      }
      },err=>{
        this.showError(err?.error?.message,"Error");
      })
    }
    else if(this.paramUrl.idCategory !=  null)
    {
      this.list.hookToQuery(courseStreamCreator).subscribe((data) => {
      this.coursesCategory=data;
      this.courseServiceApi.getCourseByCategoryID(this.paramUrl.idCategory).subscribe((data) => {
      this.coursesMaxResultCount = data;
      this.listPageNumberTotal=[];
      },err=>{
        this.showError(err?.error?.message,"Error");
      });
    },err=>{
        this.showError(err?.error?.message,"Error");
      });
    }
    else if(this.paramUrl.nameType =='newest')
    {
      this.courseServiceApi.getCoursesMaxResultCount(6).subscribe((data) => {
      this.coursesMaxResultCount = data;
      this.listPageNumberTotal=[];
    },err=>{
        this.showError(err?.error?.message,"Error");
      });
    }
    else if(this.paramUrl.nameType=='relative'){
      if(this.currentUserLogin){
      this.joinCourseService.getJoinCourseEver(this.currentUserLogin).subscribe(data=>{
        if(data){
          this.courseServiceApi.Relative(this.currentUserLogin).subscribe(data=>{
            if(data){
            this.courseServiceApi.getCourseByCategoryID(data[0].id).subscribe((data) => {
              this.coursesMaxResultCount = data;
              
              this.listPageNumberTotal=[];
              },err=>{
        this.showError(err?.error?.message,"Error");
      });
            }
          },err=>{
        this.showError(err?.error?.message,"Error");
      })
        }
      },err=>{
        this.showError(err?.error?.message,"Error");
      })
    }
    }
    else if(this.searchParams)
    {
      this.courseServiceApi.getFullCourses().subscribe((data)=>{
        this.totalCourse=data.totalCount;
        this.courseServiceApi.getCourseByFilter(this.searchParams,this.totalCourse).subscribe((data) => {
          this.coursesMaxResultCount = data;
          },err=>{
        this.showError(err?.error?.message,"Error");
      });
      },err=>{
        this.showError(err?.error?.message,"Error");
      })
    }
    else
    {
      this.courseServiceApi.getCoursesMaxResultCount(6).subscribe((data) => {
        
      this.coursesMaxResultCount = data;
      if(this.coursesMaxResultCount)
      {
        this.serverHttp.Pagination(this.coursesMaxResultCount,this.listPageNumberTotal,6);
        this.totalPageNumberOfCourse=this.serverHttp.totalPageNumberOfCourse;
      }

    },err=>{
        this.showError(err?.error?.message,"Error");
      })
    }
    
    this.serverHttp.reloadRouter();

    this.categoryService.getCategory().subscribe((data) => {
      this.category = data;
      // console.log(data)
      // var x = document.cookie;
      // console.log(x)
    },err=>{
        this.showError(err?.error?.message,"Error");
      });
    
  }
  showError(err,notification) {
  this.toastr.error(err, notification);
  } 
  showM()
  {
    this.showModal = true; // Show-Hide Modal Check
    
  }
  //Bootstrap Modal Close event
  hide()
  {
    this.showModal = false;
  }
  SetHasBeenLock(id,idCourse){
    this.joinCourseService.setHasBeenLock(id,"").subscribe(()=>{
    this.GetStudentJoinCourse(idCourse);
    },err=>{
        this.showError(err?.error?.message,"Error");
      });

  }
  key = 'creationTime';
  reserve: boolean = false;
  sort(key) {
    this.key = key;
    this.reserve = !this.reserve;
  }
  GetStudentJoinCourse(idCourse){
    this.joinCourseService.GetJoinCourseStudent(idCourse).subscribe(data=>{
      if(data){
        this.StudentJoinCourse=data;
        this.checkRoleAdmin=this.currentUserRole==this.serverHttp.roleDefaultAdmin;
        console.log(this.StudentJoinCourse)
        console.log(this.currentUserLogin)
      }
    },err=>{
        this.showError(err?.error?.message,"Error");
      })
    this.showM();
  }
  createCourse() {
    this.selectedCourse = {} as CourseDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editCourse(id: string) {
    this.courseService.get(id).subscribe((course) => {
      this.selectedCourse = course;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedCourse.name || '', Validators.required],
      description: [this.selectedCourse.description || '', Validators.required],
      idCategory: [this.selectedCourse.idCategory || '', Validators.required],
      idUser: [this.selectedCourse.idUser || '', Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedCourse.id) {
      this.courseService
        .update(this.selectedCourse.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.courseService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.courseService.delete(id).subscribe(() => this.list.get());
        }
      });
  }

  ngOnDestroy() {
    if (this.loadSameRouter) {
      this.loadSameRouter.unsubscribe();
    }
  }
}
