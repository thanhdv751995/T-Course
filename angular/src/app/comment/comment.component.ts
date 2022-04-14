import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { CommentService, CommentDto, LessonLookupDto} from '@proxy/comments';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { UserLookupDto } from '@proxy/khoa-hocs';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class CommentComponent implements OnInit {
  comment = { items: [], totalCount: 0 } as PagedResultDto<CommentDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedComment = {} as CommentDto;
  users$: Observable<UserLookupDto[]>;
  lessons$: Observable<LessonLookupDto[]>;
  constructor(
    public readonly list: ListService,
    private commentService: CommentService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService) {
    this.users$ = commentService.getUserLookup().pipe(map((r) => r.items));
    this.lessons$ = commentService.getLessonLookup().pipe(map((r) => r.items));
  }

  ngOnInit() {
    const commentStreamCreator = (query) => this.commentService.getList(query);

    this.list.hookToQuery(commentStreamCreator).subscribe((response) => {
      this.comment = response;
    });
  }
  createComment() {
    this.selectedComment = {} as CommentDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editComment(id: string) {
    this.commentService.get(id).subscribe((comment) => {
      this.selectedComment = comment;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  buildForm() {
    this.form = this.fb.group({
      content: [this.selectedComment.content || '', Validators.required],
      idLesson: [this.selectedComment.idLesson || '', Validators.required],
      idParent: [this.selectedComment.idParent || null],
      idUser: [this.selectedComment.idUser || '', Validators.required]
    });
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedComment.id) {
      this.commentService
        .update(this.selectedComment.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.commentService.create(this.form.value).subscribe(() => {
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
          this.commentService.delete(id).subscribe(() => this.list.get());
        }
      });
  }
}
