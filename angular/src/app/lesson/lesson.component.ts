import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { LessonDto, LessonService } from '../proxy/lessons';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { CourseLookupDto } from '../proxy/rates';


@Component({
  selector: 'app-lesson',
  templateUrl: './lesson.component.html',
  styleUrls: ['./lesson.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class LessonComponent implements OnInit {
  lesson = { items: [], totalCount: 0 } as PagedResultDto<LessonDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedLesson = {} as LessonDto;
  course$: Observable<CourseLookupDto[]>;
  constructor(
    public readonly list: ListService,
    private lessonService: LessonService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService) {
    this.course$ = lessonService.getCourseLookup().pipe(map((r) => r.items));
  }

  ngOnInit(): void {
    const lessonStreamCreator = (query) => this.lessonService.getList(query);

    this.list.hookToQuery(lessonStreamCreator).subscribe((response) => {
      this.lesson = response;
    });
  }
  createLesson() {
    this.selectedLesson = {} as LessonDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editLesson(id: string) {
    this.lessonService.get(id).subscribe((lesson) => {
      this.selectedLesson = lesson;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedLesson.name || '', Validators.required],
      description: [this.selectedLesson.description || '', Validators.required],
      idCourse: [this.selectedLesson.idCourse || '', Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedLesson.id) {
      this.lessonService
        .update(this.selectedLesson.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.lessonService.create(this.form.value).subscribe(() => {
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
          this.lessonService.delete(id).subscribe(() => this.list.get());
        }
      });
  }
}
