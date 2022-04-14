import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { CommentRoutingModule } from './comment-routing.module';
import { CommentComponent } from './comment.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [CommentComponent],
  imports: [
    CommentRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ]
})
export class CommentModule { }

