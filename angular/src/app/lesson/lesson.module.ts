import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { LessonRoutingModule } from './lesson-routing.module';
import { LessonComponent } from './lesson.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [LessonComponent],
  imports: [SharedModule, LessonRoutingModule, NgbDatepickerModule],
})
export class LessonModule { }
