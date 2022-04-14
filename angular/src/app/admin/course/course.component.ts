import { Component, EventEmitter, OnChanges, OnInit, Output, VERSION } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { CourseService, CourseDto, CategoryLookupDto, UserLookupDto } from '@proxy/khoa-hocs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient, HttpEventType, HttpResponse } from '@angular/common/http';
import { LessonDto, LessonService } from '@proxy/lessons';
@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class CourseComponent implements OnInit {
  
  //choosefile
  percentDone: number;
  uploadSuccess: boolean;
  [x: string]: any;
  public SkipCount = 1;
  public MaxResultCount = 10;
  searchText;
  courses = { items: [], totalCount: 0 } as PagedResultDto<CourseDto>;
  selectedValue = 'lucy';
  isModalOpen = false;
  isModalOpen2 = false;

  items = [
    {
      title: 'Item 1',
      id: 1
    },
    {
      title: 'Item 2',
      id: 2
    }
  ]

  form: FormGroup;

  selectedCourse = {} as CourseDto;
  selectedLesson = {} as LessonDto;
  categories$: Observable<CategoryLookupDto[]>;
  users$: Observable<UserLookupDto[]>;
  
  selectedOption: any;
  constructor(
    private http: HttpClient,
    public readonly list: ListService,
    private courseService: CourseService,
    private lessonService: LessonService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService) {
    this.categories$ = courseService.getCategoryLookup().pipe(map((r) => r.items));
    this.users$ = courseService.getUserLookup().pipe(map((r) => r.items));
    this.course$ = lessonService.getCourseLookup().pipe(map((r) => r.items));
  }
  ngOnInit(): void {
    const courseStreamCreator = (query) => this.courseService.getList(query);

    this.list.hookToQuery(courseStreamCreator).subscribe((response) => {
      this.courses = response;
    });
   
  }
  createCourse() {
    this.selectedCourse = {} as CourseDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editCourse(id: string) {
    this.courseService.get(id).subscribe((khoahoc) => {
      this.selectedCourse = khoahoc;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  //create lesson
  createLess(id: string) {
    this.selectedLesson = {} as LessonDto;
    this.courseService.get(id).subscribe((c) => {
      this.selectedLesson.idCourse = c.id;
      this.buildForm2();
      this.isModalOpen2 = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedCourse.name || '', Validators.required],
      description: [this.selectedCourse.description || '', Validators.required],
      idCategory: [this.selectedCourse.idCategory || '', Validators.required],
      idUser: [this.selectedCourse.idUser || '', Validators.required],
      benefit: [this.selectedCourse.benefit || '', Validators.required],
      url: [this.selectedCourse.url || '', null],
    });
  }
  buildForm2() {
    this.form = this.fb.group({
      name: [this.selectedLesson.name || '', Validators.required],
      description: [this.selectedLesson.description || '', Validators.required],
      idCourse: [this.selectedLesson.idCourse || '', Validators.required],
      url: [this.selectedLesson.url || '', null],
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
  save2() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedCourse.id) {
      
    } else {
      this.lessonService.create(this.form.value).subscribe(() => {
        this.isModalOpen2 = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  // delete(id: string) {
  //   this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
  //     .subscribe((status) => {
  //       if (status === Confirmation.Status.confirm) {
  //         this.courseService.delete(id).subscribe(() => this.list.get());
  //       }
  //     });
  // }
  key = 'id';
  reserve: boolean = false;
  sort(key) {
    this.key = key;
    this.reserve = !this.reserve;
  }
  delete (id: string)
  {
    this.courseService.getListLessonByIDCourseByID(id).subscribe(data=>{
      console.log(data)
      if(data.items.length > 0 ){
        this.confirmation.warn('::Bạn sẽ xóa hết bài học của khóa học này', '::Chắc chắn xóa')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.courseService.delete(id).subscribe(() => 
          this.list.get());
        }
      });
      }else{
        this.confirmation.warn('', '::Chắc chắn xóa')
        .subscribe((status) => {
          if (status === Confirmation.Status.confirm) {
              this.courseService.delete(id).subscribe(() => 
              this.list.get());
          }
        });
      }

    });  
  }

  
  upload(files: File[]){
    //pick from one of the 4 styles of file uploads below
    this.uploadAndProgress(files);
  }

  basicUpload(files: File[]){
    var formData = new FormData();
    Array.from(files).forEach(f => formData.append('file', f))
    this.http.post('https://file.io', formData)
      .subscribe(event => {  
        console.log('done')
      })
  }
  
  //this will fail since file.io dosen't accept this type of upload
  //but it is still possible to upload a file with this style
  basicUploadSingle(file: File){    
    this.http.post('https://file.io', file)
      .subscribe(event => {  
        console.log('done')
      })
  }
  
  uploadAndProgress(files: File[]){
    console.log(files)
    var formData = new FormData();
    Array.from(files).forEach(f => formData.append('file',f))
    
    this.http.post('https://file.io', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.percentDone = Math.round(100 * event.loaded / event.total);
        } else if (event instanceof HttpResponse) {
          this.uploadSuccess = true;
        }
    });
  }
  
  //this will fail since file.io dosen't accept this type of upload
  //but it is still possible to upload a file with this style
  uploadAndProgressSingle(file: File){    
    this.http.post('https://file.io', file, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.percentDone = Math.round(100 * event.loaded / event.total);
        } else if (event instanceof HttpResponse) {
          this.uploadSuccess = true;
        }
    });
  }
  selectcategory(): void{

    console.log("selectedOption",this.selectedOption);
    if(this.selectedOption==0){
      
      const courseStreamCreator = (query) => this.courseService.getList(query);

      this.list.hookToQuery(courseStreamCreator).subscribe((response) => {
        this.courses = response;
      });
    }
    else{
    this.courseService.getListCourseByCategoryIDByID(this.selectedOption.id).subscribe(res=>{
      this.courses = res;
    })
  }
  }
}
