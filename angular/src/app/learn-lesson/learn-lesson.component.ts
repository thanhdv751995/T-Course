import { JoinCourseServiceService } from './../ChildService/JoinCourse/join-course-service.service';
import { CommentService } from './../proxy/comments/comment.service';
import { CommentServiceService } from './../ChildService/Comment/comment-service.service';
import { AttachmentService } from './../ChildService/Attachment/attachment-service.service';
import { LessonServiceService } from './../ChildService/Lesson/lesson-service.service';
import { Component, OnDestroy, OnInit, AfterViewInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { ServerHttpService } from '../Services/server-http.service';
import { Time } from '@angular/common';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import {SpinnerService} from '../Services/spinner.service'

@Component({
  selector: 'app-learn-lesson',
  templateUrl: './learn-lesson.component.html',
  styleUrls: ['./learn-lesson.component.scss'],
})
export class LearnLessonComponent implements OnInit,AfterViewInit {
  public showModal: boolean;
  public modalImage: any;
  [x: string]: any;
  dataList: any;
  apiHost: string = environment.oAuthConfig.issuer;
  public comments: any;
  public currentUrl;
  public lessons: any;
  public lesson: any;
  public courses: any;
  public videoUrl: any;
  public trustURL: any;
  public flag = false;
  public show: boolean = false;
  public buttonName: any = 'Trả lời';
  public content: any;
  public ContentChild: any;
  public IdComment: any;
  public IdCommentChild: any;
  public create: boolean = true;
  public IdLesson: string;
  public child: boolean = false;
  public timeComment: Time;
  public currentUserLogin;
  public defaultLoad = 10;
  public loadCommentDone: boolean = false;
  flagDeleteAll = false;
  fileToUpload: File = null;
  public loadPage = this.defaultLoad;
  public load = 'Xem Thêm';
  public fileName = 'Choose File';
  loading = false;
  constructor(
    private router: ActivatedRoute,
    private rout: Router,
    private lessonService: LessonServiceService,
    private attachmentService: AttachmentService,
    private serverHttpService: ServerHttpService,
    private confirmation: ConfirmationService,
    private commentService: CommentService,
    private commentServiceApi: CommentServiceService,
    private http: HttpClient,
    private toastr: ToastrService,
    private joinCourseService: JoinCourseServiceService

  ) {}
  ngAfterViewInit(): void {
    this.getComment();
  }

  ngOnInit(): void {

    this.currentUserLogin = this.serverHttpService.currentUserLogin;
    this.currentUrl = this.router.snapshot.params;
    this.IdLesson = this.currentUrl.idLesson;
    this.CheckHasBeenBlock();

    this.lessonService.getLessonByID(this.currentUrl.idLesson).subscribe(data => {
      this.lesson = data;
      if (data) {
        this.trustURL = this.lesson.url;
        this.serverHttpService.convertVideoToSafe(this.trustURL);
        this.videoUrl = this.serverHttpService.videoUrl;
        this.flag = true;
        this.lessonService.getLessonByCourseID(this.currentUrl.idCourse).subscribe(data => {
          this.lessons = data;
        });

      }
    });
    this.serverHttpService.reloadRouter();
  }
  CheckHasBeenBlock() {
    if (this.currentUserLogin) {
      this.joinCourseService
        .CheckHasBeenBlock(this.currentUserLogin, this.currentUrl.idCourse)
        .subscribe(data => {
          if (data) {
            this.rout.navigate(['/PageNotFound']);
          }
        });
    } else this.rout.navigate(['/PageNotFound']);
  }
  showM(srcModal) {
    this.modalImage = srcModal;
    this.showModal = true; // Show-Hide Modal Check
  }
  //Bootstrap Modal Close event
  hide() {
    this.showModal = false;
  }

  TryCatchError() {
    this.toastr.error('An error has been created', 'Unknown Error');
  }
  selectedFileInput(files: FileList) {
    this.fileToUpload = <File>files.item(0);
    this.fileName = files.item(0).name;
  }
  clearFileInput() {
    this.fileToUpload = null;
    this.fileName = 'Choose File';
  }

  toggle(id: string) {
    this.show = !this.show;
    // CHANGE THE NAME OF THE BUTTON.
    if (this.show) this.buttonName = 'Đóng';
    else this.buttonName = 'Trả lời';
    this.IdComment = id;
    this.ContentChild = '';
    this.create = true;
  }

  getComment() {
    this.loading=true;
    this.commentServiceApi
      .getCommentByIDLesson(this.IdLesson, this.defaultLoad)
      .subscribe(data => {
        if (data) {
          this.comments = data;
          this.load = 'Xem Thêm';
          this.loading=false;
        }
      },()=>{
        this.loading=false; 
      });
  }
  loadMore() {
    this.loading=true;
    this.commentServiceApi
      .getCommentByIDLesson(this.IdLesson, this.loadPage + this.defaultLoad)
      .subscribe(data => {
        if (data) {
          this.loadPage += this.defaultLoad;
          this.comments = data;
          if (this.loadPage > data.totalCount) {
            this.load = 'Nothing To Load';
          }
          this.loading=false;
        }
      },()=>{
        this.loading=false;
      });
  }
  deleteComment(id) {

      this.commentService.get(id).subscribe(comment => {
        this.selectedComment = comment;
        if (comment.idParent == null) {
          this.commentServiceApi.getIDChildByIDParent(id).subscribe(data => {
            this.commentChilds = data;
            this.confirmation.warn('::Bạn chắc chắn xóa?', '::Chắc chắn xóa').subscribe(status => {
              if (status === Confirmation.Status.confirm) {
                this.commentService.delete(id).subscribe(() => this.getComment());
              }
            });
          });
        } else {
          this.confirmation.warn('::Bạn chắc chắn xóa?', '::Chắc chắn xóa').subscribe(status => {
            if (status === Confirmation.Status.confirm) {
              this.commentService.delete(id).subscribe(() => this.getComment());
            }
          });
        }
      });

    
  }
  postAttachmentFile(ID) {
    this.loading=true;
    const formData = new FormData();
    formData.append('file', this.fileToUpload);
    formData.append('idTable', ID);
    this.attachmentService.postAttachment(formData).subscribe(res => {
      if (res) {
        this.getComment();
        this.loading=false;
      }
    });
  }
  putAttachmentFile(Id) {
    const formData = new FormData();
    formData.append('file', this.fileToUpload);
    formData.append('idTable', Id);
    this.attachmentService.putAttachment(Id, formData).subscribe(() => {
      this.getComment();
    });
  }
  createComment(idParent, parent: boolean) {
    try {
      if (parent && !this.content) {
        this.toastr.warning('Input your comment!', 'Warning!');
      } else {
        if (this.create) {
          if (parent) {
            const dataComment = {
              content: this.content,
              idParent: idParent,
              idUser: this.currentUserLogin,
              idLesson: this.IdLesson,
            };
            this.commentServiceApi.postComment(dataComment).subscribe(data => {
              if (data && this.fileToUpload) {
                this.postAttachmentFile(data.id);
              } else this.getComment();
              this.show = false;
              this.clearFileInput();
            });
          } else {
            const dataComment = {
              content: this.ContentChild,
              idParent: idParent,
              idUser: this.currentUserLogin,
              idLesson: this.IdLesson,
            };
            this.commentServiceApi.postComment(dataComment).subscribe(
              data => {
                if (data && this.fileToUpload) {
                  this.postAttachmentFile(data.id);
                } else this.getComment();
                this.show = false;
                this.clearFileInput();
              },
              () => {
                this.toastr.warning('Input your comment!', 'Warning!');
              }
            );
          }

          this.ContentChild = '';
        } else {
          if (this.child) {
            const dataCommentChild = {
              content: this.ContentChild,
              idParent: idParent,
              idUser: this.currentUserLogin,
              idLesson: this.IdLesson,
            };
            this.commentServiceApi
              .putComment(this.IdCommentChild, dataCommentChild)
              .subscribe(() => {
                this.lessonService
                  .CheckExistAttachmentCommentByID(this.IdCommentChild)
                  .subscribe(data => {
                    if (data) {
                      this.putAttachmentFile(this.IdCommentChild);
                    } else {
                      this.postAttachmentFile(this.IdCommentChild);
                    }
                    this.getComment();
                    this.show = false;
                    this.clearFileInput();
                  });
              });
            this.child = false;
          } else {
            idParent = null;
            const dataCm = {
              content: this.content,
              idParent: idParent,
              idUser: this.currentUserLogin,
              idLesson: this.IdLesson,
            };
            this.commentServiceApi.putComment(this.IdComment, dataCm).subscribe(() => {
              this.lessonService.CheckExistAttachmentCommentByID(this.IdComment).subscribe(data => {
                if (data) {
                  this.putAttachmentFile(this.IdComment);
                } else {
                  this.postAttachmentFile(this.IdComment);
                }
                this.getComment();
                this.show = false;
                this.clearFileInput();
              });
            });
          }
        }
        this.show = !this.show;
        this.buttonName = 'Trả lời';
        this.content = '';
      }
    } catch {
      this.TryCatchError();
    }
    //this.isModalOpen = true;
  }
  editComment(id: string) {
    this.create = false;
    this.commentService.get(id).subscribe(comment => {
      this.selectedComment = comment;
      if (comment.idParent != null) {
        this.show = true;
        this.IdComment = comment.idParent;
        this.ContentChild = comment.content;
        this.IdCommentChild = comment.id;
        (this.child = true), (this.buttonName = 'Đóng');
      } else {
        this.show = false;
        this.content = comment.content;
        this.IdComment = comment.id;
      }
    });
  }
}
