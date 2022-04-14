import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Ng2OrderModule } from 'ng2-order-pipe';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { SharedModule } from '../shared/shared.module';

import { CourseRoutingModule } from './course-routing.module';
import { CourseComponent } from './course.component';


@NgModule({
  declarations: [CourseComponent],
  imports: [
    SharedModule,
    CourseRoutingModule, FormsModule,
    Ng2SearchPipeModule,Ng2OrderModule
  ]
})
export class CourseModule { }
